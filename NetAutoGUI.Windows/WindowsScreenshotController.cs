using NetAutoGUI.Internals;
using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Threading;
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
			int width = window.Rectangle.Width;
            int height = window.Rectangle.Height;
			HWND hWND = new HWND((IntPtr)windowHandler);
            return ScreenshotHelper.CaptureWindow(hWND, width, height);
        }        

		protected override BitmapData LoadImageFromFile(string imageFile)
		{
			using var image = Image.FromFile(imageFile);
			using Bitmap bitmap = new Bitmap(image);
			return ScreenshotHelper.ToBitmapData(bitmap);
        }

		private static PRECT ToPRECT(Rectangle r)
		{
			return new PRECT(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
		}

		public override void Highlight(double waitSeconds = 0.5, params Rectangle[] rectangles)
		{
			HDC hDC_Desktop = User32.GetDC(HWND.NULL);
			foreach (var rect in rectangles)
			{
				HBRUSH blueBrush = User32.GetSysColorBrush(SystemColorIndex.COLOR_ACTIVEBORDER);
				User32.FillRect(hDC_Desktop, new RECT(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height), blueBrush);
			}
			Thread.Sleep((int)(waitSeconds * 1000));
			foreach (var rect in rectangles)
			{
				User32.InvalidateRect(HWND.NULL, ToPRECT(rect), true);
			}
		}

        public override (int x, int y) ScreenshotLocationToRelativeLocation(int x, int y)
        {
            return (x + SystemInformation.VirtualScreen.Left, y + SystemInformation.VirtualScreen.Top);
        }
    }
}
