using NetAutoGUI.Internals;
using System;
using System.Runtime.Versioning;
using System.Threading;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
    [SupportedOSPlatform("windows")]
    internal class WindowsMouseController : AbstractMouseController
    {
        public override void Click(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left,
            int clicks = 1, double intervalInSeconds = 0)
        {
            if (clicks <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(clicks), "clicks should be positive.");
            }
            TryMoveTo(x, y);
            for (int i = 0; i < clicks; i++)
            {
                if (button == MouseButtonType.Left)
                {
                    MouseHelper.LeftButtonClick();
                }
                else if (button == MouseButtonType.Middle)
                {
                    MouseHelper.MiddleButtonClick();
                }
                else if (button == MouseButtonType.Right)
                {
                    MouseHelper.RightButtonClick();
                }

                Thread.Sleep((int)(intervalInSeconds * 1000));
            }
            Thread.Sleep(100);
        }

        public override void MouseDown(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left)
        {
            TryMoveTo(x, y);
            if (button == MouseButtonType.Left)
            {
                MouseHelper.LeftButtonDown();
            }
            else if (button == MouseButtonType.Middle)
            {
                MouseHelper.MiddleButtonDown();
            }
            else if (button == MouseButtonType.Right)
            {
                MouseHelper.RightButtonDown();
            }
        }

        public override void MouseUp(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left)
        {
            TryMoveTo(x, y);
            if (button == MouseButtonType.Left)
            {
                MouseHelper.LeftButtonUp();
            }
            else if (button == MouseButtonType.Middle)
            {
                MouseHelper.MiddleButtonUp();
            }
            else if (button == MouseButtonType.Right)
            {
                MouseHelper.RightButtonUp();
            }
        }

        private void TryMoveTo(int? x, int? y)
        {
            int destX, destY;
            if (x == null && y == null)
            {
                (destX, destY) = Position();
            }
            else if (x != null && y != null)
            {
                (destX, destY) = (x.Value, y.Value);
            }
            else
            {
                throw new ArgumentException("Either x or y are all null or none of them are null");
            }
            MoveTo(destX, destY);
        }

        public override void MoveTo(int x, int y)
        {
            MouseHelper.MoveTo(x, y);
        }

        public override Location Position()
        {
            User32.GetCursorPos(out POINT point);
            return new Location(point.X, point.Y);
        }

        public override void Scroll(int value)
        {
            MouseHelper.Scroll(value);
        }
    }
}
