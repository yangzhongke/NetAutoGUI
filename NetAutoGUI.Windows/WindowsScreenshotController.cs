using NetAutoGUI.Internals;
using System.Runtime.Versioning;
using System.Windows.Forms;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
    [SupportedOSPlatform("windows")]
    internal class WindowsScreenshotController : AbstractScreenshotController
    {
        public override BitmapData Screenshot()
        {
            return ScreenshotHelper.CaptureVirtualScreen();
        }

        public override BitmapData Screenshot(Window window)
        {
            var windowHandler = window.Id;
            return ScreenshotHelper.CaptureWindow(windowHandler.ToHWND());
        }


        public override void Highlight(params Rectangle[] rectangles)
        {
            HDC hDcDesktop = User32.GetDC(HWND.NULL);
            foreach (var rect in rectangles)
            {
                HBRUSH blueBrush = User32.GetSysColorBrush(SystemColorIndex.COLOR_ACTIVEBORDER);
                User32.FillRect(hDcDesktop, new RECT(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height),
                    blueBrush);
            }

            GUI.Pause(0.5);
            foreach (var rect in rectangles)
            {
                User32.InvalidateRect(HWND.NULL, ToPRECT(rect), true);
            }

            PRECT ToPRECT(Rectangle r)
            {
                return new PRECT(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
            }
        }
        
        public override (int x, int y) ScreenshotLocationToRelativeLocation(int x, int y)
        {
            return (x + SystemInformation.VirtualScreen.Left, y + SystemInformation.VirtualScreen.Top);
        }
    }
}
