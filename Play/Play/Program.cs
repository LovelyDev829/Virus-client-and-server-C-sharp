using System;
using Topshelf;
using System.Runtime.InteropServices;

namespace Play
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            // Hide
            // ShowWindow(handle, SW_HIDE);

            var exitCode = HostFactory.Run(x =>
            {
                x.Service<Heartbeat>(s =>
                {
                    s.ConstructUsing(heartbeat => new Heartbeat());
                    s.WhenStarted(heartbeat => heartbeat.Start());
                    s.WhenStopped(heartbeat => heartbeat.Stop());
                });
                x.RunAsLocalSystem();

                // x.SetServiceName("IPservice");
                // x.SetDisplayName("IPservice");
                x.SetDescription("This is a simple display testing program with C#");
            });
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
