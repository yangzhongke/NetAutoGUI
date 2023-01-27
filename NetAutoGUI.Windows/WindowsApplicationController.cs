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
                bool visible = User32.IsWindowVisible(hwnd);
                if (!visible) return true;
                var currentWin = GetWindowDetail(hwnd);
                if (predict(currentWin))
                {
                    window = currentWin;
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

		public Window FindWindowById(long id)
		{
			return GetWindowDetail(new HWND(new IntPtr(id)));
		}
	}
}
