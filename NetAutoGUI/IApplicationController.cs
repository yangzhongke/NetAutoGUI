using System;

namespace NetAutoGUI
{
    public interface IApplicationController
    {
        public void LaunchApplication(string appPath, params string[] arguments);
        public bool IsApplicationRunning(string appPath);
        /*public void ActivateApplication(string appPath, bool throwExceptionWhenMulti=true);*/
        public void WindowExistsByTitle(string title, bool throwExceptionWhenMulti);
        public void WindowExistsByTitle(Func<string,bool> predict, bool throwExceptionWhenMulti = true);
        public void ActivateWindowByTitle(string title, bool throwExceptionWhenMulti = true);
        public void ActivateWindowByTitle(Func<string, bool> predict, bool throwExceptionWhenMulti = true);
    }
}
