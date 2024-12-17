using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public bool IsApplicationRunning(string processName)
        {
            //The ProcessName property does not include the .exe extension
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(processName);
            return Process.GetProcesses().Any(p => nameWithoutExtension.Equals(p.ProcessName, StringComparison.OrdinalIgnoreCase));
        }

        public Process LaunchApplication(string appPath, string? arguments = null)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = appPath,
                Arguments = arguments,
                UseShellExecute = true,//This way you can start it with the relative path "chrome.exe" instead of the full path
            };
            var process = Process.Start(startInfo);
            if(process == null)
            {
                throw new InvalidOperationException("Failed to start the application");
            }
            return process;
        }

        public Window? FindWindowByTitle(string title)
        {
            return FindWindow(t => title == t.Title);
        }

        public Window? FindWindow(Func<Window, bool> predict)
        {
            Window? window = null;
            User32.EnumWindows((hwnd, _) =>
            {
                bool visible = User32.IsWindowVisible(hwnd);
                if (!visible) return true;
                var currentWin = WindowFactory.CreateWindow(hwnd);
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
            return FindWindow(f => wildcard.WildcardMatch(f.Title, true));
        }

        public void WaitForApplication(string processName, double timeoutSeconds = 2)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //The ProcessName property does not include the .exe extension
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(processName);
            while (!Process.GetProcesses().Any(p => p.ProcessName == nameWithoutExtension))
            {
                if (stopwatch.ElapsedMilliseconds > timeoutSeconds * 1000)
                {
                    throw new TimeoutException("wait for application timeout:" + processName);
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
            while ((window = GetAllWindows().FirstOrDefault(predict)) == null)
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
            //The ProcessName property does not include the .exe extension
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(processName);
            var items = Process.GetProcesses().Where(p => nameWithoutExtension.Equals(p.ProcessName, StringComparison.OrdinalIgnoreCase));
            foreach (var p in items)
            {
                p.Kill();
            }
        }

        public Window[] GetAllWindows()
        {
            List<Window> list = new List<Window>();
            User32.EnumWindows((hwnd, _) =>
            {
                var window = WindowFactory.CreateWindow(hwnd);
                list.Add(window);
                return true;// continue the enumeration
            }, IntPtr.Zero);
            return list.ToArray();
        }

        public Window FindWindowById(long id)
        {
            return WindowFactory.CreateWindow(id.ToHWND());
        }

        public void OpenFileWithDefaultApp(string filePath)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
    }
}
