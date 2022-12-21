using System;

namespace NetAutoGUI
{
    public interface IApplicationController
    {
        public void LaunchApplication(string appPath, string? arguments=null);
        public bool IsApplicationRunning(string processName);
        public void KillProcesses(string processName);
        public bool WindowExistsByTitle(string title);
        public bool WindowExistsByTitle(Func<string,bool> predict);
        public bool WindowExistsLikeTitle(string wildcard);
        public void ActivateWindowByTitle(string title);
        public void ActivateWindowByTitle(Func<string, bool> predict);
        public void ActivateWindowLikeTitle(string wildcard);
        public void WaitForApplication(string processName, double timeoutSeconds=2);
        public void WaitForWindowByTitle(string title, double timeoutSeconds = 2);
        public void WaitForWindowByTitle(Func<string, bool> predict, double timeoutSeconds = 2);
        public void WaitForWindowLikeTitle(string wildcard, double timeoutSeconds = 2);
    }
}
