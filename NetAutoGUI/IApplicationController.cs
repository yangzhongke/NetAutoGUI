using System;
using System.Diagnostics;

namespace NetAutoGUI
{
    public interface IApplicationController
    {
        public Process LaunchApplication(string appPath, string? arguments=null);
        public bool IsApplicationRunning(string processName);
        public void KillProcesses(string processName);
        public Window[] GetAllWindows();
        public Window? FindWindowByTitle(string title);
        public Window? FindWindow(Func<Window, bool> predict);
        public Window? FindWindowLikeTitle(string wildcard);
        public void WaitForApplication(string processName, double timeoutSeconds=2);

        public Window WaitForWindowByTitle(string title, double timeoutSeconds = 2);
        public Window WaitForWindow(Func<Window, bool> predict, double timeoutSeconds = 2);
        public Window WaitForWindowLikeTitle(string wildcard, double timeoutSeconds = 2);
    }
}
