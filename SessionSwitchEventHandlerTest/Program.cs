using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SessionSwitchEventHandlerTest
{
    public class Program
    {
        public static SessionSwitchEventHandler sessionSwitchEventHandler;

        [STAThread]
        public static void Main(string[] args)
        {
            ThreadStart systemSessionMonitorThreadStart = () => systemSessionMonitorConfigure();
            Thread systemSessionMonitorThread = new Thread(systemSessionMonitorThreadStart);
            systemSessionMonitorThread.Start();

            Console.ReadLine();
        }

        private static void systemSessionMonitorConfigure()
        {
            sessionSwitchEventHandler = new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            SystemEvents.SessionSwitch += sessionSwitchEventHandler;
        }

        public static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            //if (e.Reason == SessionSwitchReason.ConsoleDisconnect || e.Reason == SessionSwitchReason.RemoteDisconnect) 
            //{
            //    File.WriteAllText("Log.txt", "Exiting!");
            //}
            //Console.WriteLine(e.Reason.ToString());
            //Environment.Exit(0);

            File.AppendAllLines("Log.txt", new string[] { e.Reason.ToString() });
        }
    }
}
