using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using NetAutoGUI.Internals;
using Vanara.PInvoke;
using WildcardMatch;

namespace NetAutoGUI.Windows
{
    [SupportedOSPlatform("windows")]
    internal class WindowsApplicationController : IApplicationController
    {
        public bool IsApplicationRunning(string processName, string? arguments = null)
        {
            return Process.GetProcesses().Any(p => processName.Equals(p.ProcessName, StringComparison.OrdinalIgnoreCase)
                                                   && (arguments == null || (arguments == p.StartInfo.Arguments)));
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
            return FindWindow(w => title == w.Title);
        }

        public Window? FindWindow(Func<Window, bool> predict)
        {
            Window? window = null;
            User32.EnumWindows((hwnd, _) =>
            {
                bool visible = User32.IsWindowVisible(hwnd);
                if (!visible) return true;
                var currentWin = new Window(hwnd.ToInt64());
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
            return FindWindow(w => wildcard.WildcardMatch(w.Title, true));
        }

        public Process WaitForApplication(string processName, double timeoutSeconds = Constants.DefaultWaitSeconds)
        {
            return TimeBoundWaiter.WaitForNotNull(() =>
                    Process.GetProcesses().FirstOrDefault(p =>
                        processName.Equals(p.ProcessName, StringComparison.OrdinalIgnoreCase)), timeoutSeconds,
                $"Cannot find application with processName={processName}");
        }

        public async Task<Process> WaitForApplicationAsync(string processName,
            double timeoutSeconds = Constants.DefaultWaitSeconds,
            CancellationToken cancellationToken = default)
        {
            return await TimeBoundWaiter.WaitForNotNullAsync(
                () => Process.GetProcesses().FirstOrDefault(p => p.ProcessName == processName), timeoutSeconds,
                $"Cannot find application with processName={processName}",
                cancellationToken);
        }

        public Window WaitForWindowByTitle(string title, double timeoutSeconds = Constants.DefaultWaitSeconds)
        {
            return WaitForWindow(w => title == w.Title, $"Cannot find a window whose title={title}",
                timeoutSeconds);
        }

        public async Task<Window> WaitForWindowByTitleAsync(string title,
            double timeoutSeconds = Constants.DefaultWaitSeconds,
            CancellationToken cancellationToken = default)
        {
            return await WaitForWindowAsync(w => title == w.Title,
                $"Cannot find a window whose title={title}",
                timeoutSeconds, cancellationToken);
        }

        public Window WaitForWindow(Func<Window, bool> predict, string errorMessageWhenTimeout,
            double timeoutSeconds = Constants.DefaultWaitSeconds)
        {
            return TimeBoundWaiter.WaitForNotNull(() => GetAllWindows().FirstOrDefault(predict), timeoutSeconds,
                errorMessageWhenTimeout);
        }

        public async Task<Window> WaitForWindowAsync(Func<Window, bool> predict,
            string errorMessageWhenTimeout,
            double timeoutSeconds = Constants.DefaultWaitSeconds,
            CancellationToken cancellationToken = default)
        {
            return await TimeBoundWaiter.WaitForNotNullAsync(() => GetAllWindows().FirstOrDefault(predict),
                timeoutSeconds, errorMessageWhenTimeout, cancellationToken);
        }

        public Window WaitForWindowLikeTitle(string wildcard, double timeoutSeconds = Constants.DefaultWaitSeconds)
        {
            return WaitForWindow(w => wildcard.WildcardMatch(w.Title, true),
                $"Cannot find a window with text like '{wildcard}'", timeoutSeconds);
        }

        public async Task<Window> WaitForWindowLikeTitleAsync(string wildcard,
            double timeoutSeconds = Constants.DefaultWaitSeconds,
            CancellationToken cancellationToken = default)
        {
            return await WaitForWindowAsync(w => wildcard.WildcardMatch(w.Title, true),
                $"Cannot find a window with text like '{wildcard}'", timeoutSeconds,
                cancellationToken);
        }

        public void KillProcesses(string processName)
        {
            var items = Process.GetProcesses()
                .Where(p => processName.Equals(p.ProcessName, StringComparison.OrdinalIgnoreCase));
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
                var window = new Window(hwnd.ToInt64());
                if (User32.IsWindowVisible(hwnd))
                {
                    list.Add(window);
                }
                return true;// continue the enumeration
            }, IntPtr.Zero);
            return list.ToArray();
        }

        public Window? FindWindowById(long id)
        {
            HWND hwnd = id.ToHWND();
            if (!User32.IsWindow(hwnd))
            {
                return null;
            }

            return new Window(hwnd.ToInt64());
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
