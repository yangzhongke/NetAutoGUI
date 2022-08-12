using NetAutoGUI.Internals;
using System;
using System.Threading;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
    internal class WindowsMouseController : AbstractMouseController
    {
        private static void CheckXandY(int? x = null, int? y = null)
        {
            if ((x == null && y != null) || (x != null && y == null))
            {
                throw new ArgumentException("Either x or y are all null or none of them are null");
            }
        }
        public override void Click(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left, int clicks = 1, double interval = 0)
        {
            CheckXandY(x, y);

            if(clicks<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(clicks), "clicks should be positive.");
            }
            for (int i = 0; i < clicks;i++)
            {
                if (x == null && y == null)
                {
                    (x, y) = Position();
                }
                User32.MOUSEEVENTF btnTypeDown, btnTypeUp;
                ToMouseEvent(button, out btnTypeDown, out btnTypeUp);
                User32.mouse_event(btnTypeDown, 0, 0, 0, IntPtr.Zero);
                User32.mouse_event(btnTypeUp, 0, 0, 0, IntPtr.Zero);
                Thread.Sleep((int)(interval * 1000));
            }
        }

        private static void ToMouseEvent(MouseButtonType button, out User32.MOUSEEVENTF btnTypeDown, out User32.MOUSEEVENTF btnTypeUp)
        {
            if (button == MouseButtonType.Left)
            {
                btnTypeDown = User32.MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN;
                btnTypeUp = User32.MOUSEEVENTF.MOUSEEVENTF_LEFTUP;
            }
            else if (button == MouseButtonType.Right)
            {
                btnTypeDown = User32.MOUSEEVENTF.MOUSEEVENTF_RIGHTDOWN;
                btnTypeUp = User32.MOUSEEVENTF.MOUSEEVENTF_RIGHTUP;
            }
            else if (button == MouseButtonType.Middle)
            {
                btnTypeDown = User32.MOUSEEVENTF.MOUSEEVENTF_MIDDLEDOWN;
                btnTypeUp = User32.MOUSEEVENTF.MOUSEEVENTF_MIDDLEUP;
            }
            else
            {
                throw new InvalidOperationException("unknow button:" + button);
            }
        }

        public override void MouseDown(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left)
        {
            CheckXandY(x, y);
            User32.MOUSEEVENTF btnTypeDown;
            ToMouseEvent(button, out btnTypeDown, out _);
            User32.mouse_event(btnTypeDown, 0, 0, 0, IntPtr.Zero);
        }

        public override void MouseUp(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left)
        {
            CheckXandY(x, y);
            User32.MOUSEEVENTF btnTypeUp;
            ToMouseEvent(button, out _, out btnTypeUp);
            User32.mouse_event(btnTypeUp, 0, 0, 0, IntPtr.Zero);
        }

        public override void MoveTo(int x, int y)
        {
            User32.SetCursorPos(x, y)
                .CheckReturn(nameof(User32.SetCursorPos));
        }

        public override Location Position()
        {
            User32.GetCursorPos(out POINT point)
                .CheckReturn(nameof(User32.GetCursorPos));
            return new Location(point.X, point.Y);
        }

        public override void Scroll(int value)
        {
            User32.mouse_event(User32.MOUSEEVENTF.MOUSEEVENTF_WHEEL, 0,0,value,IntPtr.Zero);
        }

        public override Size Size()
        {
            int w = User32.GetSystemMetrics(User32.SystemMetric.SM_CXSCREEN);
            int h = User32.GetSystemMetrics(User32.SystemMetric.SM_CYSCREEN);
            return new Size(w, h);
        }
    }
}
