using NetAutoGUI.Internals;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
			using Bitmap bitmap = ScreenshotHelper.CaptureVirtualScreen();
			return ToBitmapData(bitmap);
		}

        public override BitmapData Screenshot(Window window)
        {
			var windowHandler = window.Id;
			int width = window.Rectangle.Width;
            int height = window.Rectangle.Height;
            var bytes = ScreenshotHelper.CaptureWindow(new HWND((IntPtr)windowHandler), width, height);
            return new BitmapData(bytes, width, height);
        }

        private static BitmapData ToBitmapData(Bitmap bitmap)
		{
			using MemoryStream memSteam = new MemoryStream();
			bitmap.Save(memSteam, ImageFormat.Bmp);
			memSteam.Position = 0;
			byte[] data = memSteam.ToArray();
			return new BitmapData(data, bitmap.Width, bitmap.Height);
		}

		protected override BitmapData LoadImageFromFile(string imageFile)
		{
			using var image = Image.FromFile(imageFile);
			using Bitmap bitmap = new Bitmap(image);
			return ToBitmapData(bitmap);
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

        
    }
}
