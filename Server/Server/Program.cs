using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Server
{
    class Program
    {
        const int LISTENING_PORT = 10001;
        const string IMAGE_DIR = @"C:\virus\receive\";
        const int BUFFER_SIZE = 10240;

        static void Main(string[] args)
        {
            string dir = IMAGE_DIR;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var listener = new TcpListener(IPAddress.Any, LISTENING_PORT);
            listener.Start();

            Console.WriteLine($"Listening on port {LISTENING_PORT}...");
            while (true)
            {
                var client = listener.AcceptTcpClient();
                Console.Write($"[{client.Client.RemoteEndPoint.ToString().Remove(12)}]");
                ThreadPool.QueueUserWorkItem(cb => ClientThread(client));
            }
        }
        static void ClientThread(TcpClient client)
        {
            try
            {
                using (var stream = client.GetStream())
                {
                    string subdir = IMAGE_DIR+client.Client.RemoteEndPoint.ToString().Remove(12)+"\\";
                    if (!Directory.Exists(subdir))
                    {
                        Directory.CreateDirectory(subdir);
                    }
                    // Read filename length
                    int fNameLen = stream.ReadByte();
                    byte[] fNameBytes = new byte[fNameLen];
                    // Read filename
                    stream.Read(fNameBytes, 0, fNameLen);
                    string fName = Encoding.Unicode.GetString(fNameBytes);
                    Console.WriteLine($" {fName}");
                    using (var fs = File.OpenWrite(subdir + fName))
                    {
                        byte[] buffer = new byte[BUFFER_SIZE];
                        while (true)
                        {
                            int r = stream.Read(buffer, 0, BUFFER_SIZE);
                            if (r == 0)
                                break;
                            fs.Write(buffer, 0, r);
                        }
                    }
                }
            }
            finally
            {
                client.Close();
            }
        }
    }
}
