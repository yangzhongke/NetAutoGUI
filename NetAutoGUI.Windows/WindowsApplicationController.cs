using System;
using System.Text;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
    internal class WindowsApplicationController : IApplicationController
    {
        private string GetWindowText(IntPtr hwnd)
        {
            StringBuilder sb = new StringBuilder(1024);
            User32.GetWindowText(hwnd,sb, sb.Capacity);
            return sb.ToString();
        }
        public void ActivateWindowByTitle(string title, bool throwExceptionWhenMulti = true)
        {
            User32.EnumWindows((hwnd, _) => {
                string text = GetWindowText((IntPtr)hwnd);
                if(text==title)
                {
                    User32.BringWindowToTop(hwnd);
                    return false;//stop the enumeration
                }
                else
                {
                    return true;// continue the enumeration
                }
            },IntPtr.Zero);
        }

        public void ActivateWindowByTitle(Func<string, bool> predict, bool throwExceptionWhenMulti = true)
        {
            throw new NotImplementedException();
        }

        public bool IsApplicationRunning(string appPath)
        {
            throw new NotImplementedException();
        }

        public void LaunchApplication(string appPath, params string[] arguments)
        {
            throw new NotImplementedException();
        }

        public void WindowExistsByTitle(string title, bool throwExceptionWhenMulti)
        {
            throw new NotImplementedException();
        }

        public void WindowExistsByTitle(Func<string, bool> predict, bool throwExceptionWhenMulti = true)
        {
            throw new NotImplementedException();
        }
    }
}
