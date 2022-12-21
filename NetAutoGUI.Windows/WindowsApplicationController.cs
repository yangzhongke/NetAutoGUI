using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using Vanara.PInvoke;
using WildcardMatch;

namespace NetAutoGUI.Windows
{
    [SupportedOSPlatform("windows")]
    internal class WindowsApplicationController : IApplicationController
    {
        private string GetWindowText(HWND hwnd)
        {
            StringBuilder sb = new StringBuilder(1024);
            User32.GetWindowText(hwnd,sb, sb.Capacity);
            return sb.ToString();
        }

        private Rectangle GetWindowRect(HWND hwnd)
        {
            User32.GetWindowRect(hwnd, out RECT rect);
            return new Rectangle(rect.X,rect.Y,rect.Width,rect.Height);
        }

        private Window GetWindowDetail(HWND hwnd)
        {
            string title = GetWindowText(hwnd);
            Rectangle rect = GetWindowRect(hwnd);
            IntPtr intPtr = (IntPtr)hwnd;
            Window window = new Window(title, intPtr.ToInt64(), rect);
            return window;
        }

        public Window? ActivateWindowByTitle(string title)
        {
            return ActivateWindowByTitle(t => t == title);
        }

        public Window? ActivateWindowByTitle(Func<string, bool> predict)
        {
            bool found = false;
            Window? window = null;
            User32.EnumWindows((hwnd, _) => {
                string text = GetWindowText(hwnd);
                if (predict(text))
                {
                    //https://stackoverflow.com/questions/2636721/bring-another-processes-window-to-foreground-when-it-has-showintaskbar-false
                    if (User32.IsIconic(hwnd))
                    {
                        User32.ShowWindow(hwnd, ShowWindowCommand.SW_RESTORE);
                    }
                    User32.SetForegroundWindow(hwnd);
                    found = true;
                    window = GetWindowDetail(hwnd);
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
            return window;
        }

        public Window? ActivateWindowLikeTitle(string wildcard)
        {
            return ActivateWindowByTitle(f => wildcard.WildcardMatch(f, true));
        }

        public bool IsApplicationRunning(string processName)
        {
            return Process.GetProcesses().Any(p=>p.ProcessName==processName);
        }

        public void LaunchApplication(string appPath, string? arguments=null)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = appPath,
                Arguments = arguments,
                UseShellExecute = true,//这样就可以用"chrome.exe"这个相对路径启动，而不用全路径
            };
            Process.Start(startInfo);
        }

        public Window? FindWindowByTitle(string title)
        {
            return FindWindowByTitle(t => title == t);
        }

        public Window? FindWindowByTitle(Func<string, bool> predict)
        {
            Window? window = null;
            User32.EnumWindows((hwnd, _) => {
                string text = GetWindowText(hwnd);
                if (predict(text))
                {
                    window = GetWindowDetail(hwnd);
                    return false;//stop the enumeration
                }
                else
                {
                    return true;// continue the enumeration
                }
            }, IntPtr.Zero);
            return window;
        }

        public Window? FindWindowLikeTitle(string wildcard)
        {
            return FindWindowByTitle(f=>wildcard.WildcardMatch(f,true));
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

        public Window WaitForWindowByTitle(string title, double timeoutSeconds = 2)
        {
            return WaitForWindowByTitle(t => title == t, timeoutSeconds);
        }

        public Window WaitForWindowByTitle(Func<string, bool> predict, double timeoutSeconds = 2)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Window? window = null;
            while ((window = FindWindowByTitle(predict))==null)
            {
                if (stopwatch.ElapsedMilliseconds > timeoutSeconds * 1000)
                {
                    throw new TimeoutException("wait for Window timeout");
                }
                Thread.Sleep(50);
            }
            return window;
        }

        public Window WaitForWindowLikeTitle(string wildcard, double timeoutSeconds = 2)
        {
            return WaitForWindowByTitle(f => wildcard.WildcardMatch(f, true), timeoutSeconds);
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
