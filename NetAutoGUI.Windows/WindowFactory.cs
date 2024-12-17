using System.Text;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
    public static class WindowFactory
    {
        private static string GetWindowText(long hwnd)
        {
            StringBuilder sb = new StringBuilder(1024);
            User32.GetWindowText(hwnd.ToHWND(), sb, sb.Capacity);
            return sb.ToString();
        }

        private static Rectangle GetWindowRect(long hwnd)
        {
            User32.GetWindowRect(hwnd.ToHWND(), out RECT rect);
            return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static Window CreateWindow(HWND hwnd)
        {
            Window window = new Window(hwnd.ToInt64(), GetWindowRect, GetWindowText);
            return window;
        }
    }
}
