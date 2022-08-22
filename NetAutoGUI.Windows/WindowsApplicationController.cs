using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
    [SupportedOSPlatform("windows")]
    internal class WindowsApplicationController : IApplicationController
    {
        private string GetWindowText(IntPtr hwnd)
        {
            StringBuilder sb = new StringBuilder(1024);
            User32.GetWindowText(hwnd,sb, sb.Capacity);
            return sb.ToString();
        }
        public void ActivateWindowByTitle(string title)
        {
            ActivateWindowByTitle(t => t == title);
        }

        public void ActivateWindowByTitle(Func<string, bool> predict)
        {
            bool found = false;
            User32.EnumWindows((hwnd, _) => {
                string text = GetWindowText((IntPtr)hwnd);
                if (predict(text))
                {
                    //https://stackoverflow.com/questions/2636721/bring-another-processes-window-to-foreground-when-it-has-showintaskbar-false
                    if (User32.IsIconic(hwnd))
                    {
                        User32.ShowWindow(hwnd, ShowWindowCommand.SW_RESTORE);
                    }
                    User32.SetForegroundWindow(hwnd);
                    found = true;
                    return false;//stop the enumeration
                }
                else
                {
                    return true;// continue the enumeration
                }
            }, IntPtr.Zero);
            if(!found)
            {
                throw new InvalidOperationException("cannot find the window");
            }
        }

        public bool IsApplicationRunning(string processName)
        {
            return Process.GetProcesses().Any(p=>p.ProcessName==processName);
        }

        public void LaunchApplication(string appPath, params string[] arguments)
        {
            Process.Start(appPath,arguments);
        }

        public bool WindowExistsByTitle(string title)
        {
            return WindowExistsByTitle(t => title == t);
        }

        public bool WindowExistsByTitle(Func<string, bool> predict)
        {
            bool found = false;
            User32.EnumWindows((hwnd, _) => {
                string text = GetWindowText((IntPtr)hwnd);
                if (predict(text))
                {
                    found = true;
                    return false;//stop the enumeration
                }
                else
                {
                    return true;// continue the enumeration
                }
            }, IntPtr.Zero);
            return found;
        }

        public void WaitForApplication(string processName, double timeoutSeconds = 2)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while(!Process.GetProcesses().Any(p => p.ProcessName == processName))
            {
                if(stopwatch.ElapsedMilliseconds>timeoutSeconds*1000)
                {
                    throw new TimeoutException("wait for application timeout:"+processName);
                }
                Thread.Sleep(50);
            }
        }

        public void WaitForWindowByTitle(string title, double timeoutSeconds = 2)
        {
            WaitForWindowByTitle(t => title == t, timeoutSeconds);
        }

        public void WaitForWindowByTitle(Func<string, bool> predict, double timeoutSeconds = 2)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (!WindowExistsByTitle(predict))
            {
                if (stopwatch.ElapsedMilliseconds > timeoutSeconds * 1000)
                {
                    throw new TimeoutException("wait for Window timeout");
                }
                Thread.Sleep(50);
            }
        }

        public void KillProcesses(string processName)
        {
            var items = Process.GetProcesses().Where(p => p.ProcessName == processName);
            foreach(var p in items)
            {
                p.Kill();
            }
        }
    }
}
