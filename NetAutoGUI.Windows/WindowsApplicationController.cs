using System;
using System.Collections.Generic;
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
            return ActivateWindow(t => t.Title == title);
        }

        public Window? ActivateWindow(Func<Window, bool> predict)
        {
            bool found = false;
            Window? window = null;
            User32.EnumWindows((hwnd, _) => {
                //skip invisible windows
                if (!User32.IsWindowVisible(hwnd)) return true;
                var currentWin = GetWindowDetail(hwnd);
                if (predict(currentWin))
                {
                    ActiveWindow(((IntPtr)hwnd).ToInt64());
                    Thread.Sleep(100);
                    found = true;
                    window = currentWin;
                    return false;//stop the enumeration
                }
                else
                {
                    return true;// continue the enumeration
                }
            },IntPtr.Zero);
            if(!found)
            {
                throw new InvalidOperationException("cannot find the window");
            }
            return window;
        }

        public Window? ActivateWindowLikeTitle(string wildcard)
        {
            return ActivateWindow(f => wildcard.WildcardMatch(f.Title, true));
        }

        public bool IsApplicationRunning(string processName)
        {
            return Process.GetProcesses().Any(p=>p.ProcessName==processName);
        }

        public Process LaunchApplication(string appPath, string? arguments=null)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = appPath,
                Arguments = arguments,
                UseShellExecute = true,//这样就可以用"chrome.exe"这个相对路径启动，而不用全路径
            };
            return Process.Start(startInfo);
        }

        public Window? FindWindowByTitle(string title)
        {
            return FindWindow(t => title == t.Title);
        }

        public Window? FindWindow(Func<Window, bool> predict)
        {
            Window? window = null;
            User32.EnumWindows((hwnd, _) => {
                var window = GetWindowDetail(hwnd);
                if (predict(window))
                {                    
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
            return FindWindow(f=>wildcard.WildcardMatch(f.Title,true));
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
            return WaitForWindow(t => title == t.Title, timeoutSeconds);
        }

        public Window WaitForWindow(Func<Window, bool> predict, double timeoutSeconds = 2)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Window? window = null;
            while ((window = GetAllWindows().FirstOrDefault(predict))==null)
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
            return WaitForWindow(f => wildcard.WildcardMatch(f.Title, true), timeoutSeconds);
        }

        public void KillProcesses(string processName)
        {
            var items = Process.GetProcesses().Where(p => p.ProcessName == processName);
            foreach(var p in items)
            {
                p.Kill();
            }
        }

        public Window[] GetAllWindows()
        {
            List<Window> list = new List<Window>(); 
            User32.EnumWindows((hwnd, _) => {
                var window = GetWindowDetail(hwnd);
                list.Add(window);
                return true;// continue the enumeration
            }, IntPtr.Zero);
            return list.ToArray();
        }

        public void ActiveWindow(long windowId)
        {
            //https://stackoverflow.com/questions/2636721/bring-another-processes-window-to-foreground-when-it-has-showintaskbar-false
            HWND hwnd = new HWND((IntPtr)windowId);
            if (User32.IsIconic(hwnd))
            {
                User32.ShowWindow(hwnd, ShowWindowCommand.SW_RESTORE);
            }
            User32.SetForegroundWindow(hwnd);
        }
    }
}
