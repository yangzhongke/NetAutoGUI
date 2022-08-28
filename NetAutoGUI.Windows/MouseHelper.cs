using NetAutoGUI.Internals;
using System;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
    internal class MouseHelper
    {
        public static void MoveTo(int x,int y)
        {
            User32.SetCursorPos(x, y)
                .CheckReturn(nameof(User32.SetCursorPos));
        }

        public static void LeftButtonDown()
        {
            User32.mouse_event(User32.MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero);
        }

        public static void LeftButtonUp()
        {
            User32.mouse_event(User32.MOUSEEVENTF.MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void LeftButtonClick()
        {
            LeftButtonDown();
            LeftButtonUp();
        }

        public static void RightButtonDown()
        {
            User32.mouse_event(User32.MOUSEEVENTF.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero);
        }

        public static void RightButtonUp()
        {
            User32.mouse_event(User32.MOUSEEVENTF.MOUSEEVENTF_RIGHTUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void RightButtonClick()
        {
            RightButtonDown();
            RightButtonUp();
        }

        public static void MiddleButtonDown()
        {
            User32.mouse_event(User32.MOUSEEVENTF.MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, IntPtr.Zero);
        }

        public static void MiddleButtonUp()
        {
            User32.mouse_event(User32.MOUSEEVENTF.MOUSEEVENTF_MIDDLEUP, 0, 0, 0, IntPtr.Zero);
        }

        public static void MiddleButtonClick()
        {
            MiddleButtonDown();
            MiddleButtonUp();
        }

        public static void Scroll(int delta)
        {
            User32.mouse_event(User32.MOUSEEVENTF.MOUSEEVENTF_WHEEL, 0, 0,delta, IntPtr.Zero);
        }
    }
}
