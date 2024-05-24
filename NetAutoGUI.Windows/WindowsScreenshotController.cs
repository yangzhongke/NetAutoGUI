using NetAutoGUI.Internals;
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
		public override BitmapData Screenshot(Rectangle? region = null, uint screenIndex = 0)
		{
			Screen screen = Screen.AllScreens[screenIndex];
			if (region == null)
			{
				region = ScreenshotHelper.ToAutoRect(screen.Bounds);
			}
			using Bitmap bitmap = new Bitmap(region.Width, region.Height);
			using Graphics g = Graphics.FromImage(bitmap);
			g.CopyFromScreen(region.X, region.Y, 0, 0, new(region.Width, region.Height));
			return ToBitmapData(bitmap);
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

		private static System.Drawing.Rectangle GetVirtualScreenBounds()
		{
            /*
			System.Drawing.Rectangle result = new System.Drawing.Rectangle();
			foreach (Screen screen in Screen.AllScreens)
			{
                var dm = new DEVMODE();
                dm.dmSize = (ushort)Marshal.SizeOf(typeof(DEVMODE));
                User32.EnumDisplaySettings(screen.DeviceName, User32.ENUM_CURRENT_SETTINGS, ref dm);
				System.Drawing.Rectangle realBounds = new System.Drawing.Rectangle(dm.dmPosition.x, dm.dmPosition.Y, (int)dm.dmPelsWidth, (int)dm.dmPelsHeight);
                result = System.Drawing.Rectangle.Union(result, realBounds);
            }
			return result;*/
            System.Drawing.Rectangle result = new System.Drawing.Rectangle();
            foreach (Screen screen in Screen.AllScreens)
            {               
                result = System.Drawing.Rectangle.Union(result, screen.Bounds);
            }
            return result;
        }

        public override BitmapData ScreenshotAllScreens()
        {
            var virtualScreenBounds = GetVirtualScreenBounds();
			using Bitmap bitmap = new(virtualScreenBounds.Width, virtualScreenBounds.Height);
			using Graphics g = Graphics.FromImage(bitmap);
			g.CopyFromScreen(virtualScreenBounds.Location, System.Drawing.Point.Empty, bitmap.Size);
			bitmap.Save("d:/1.jpg");
			return ToBitmapData(bitmap);
            /*
            var virtualScreenBounds = GetVirtualScreenBounds();
            int width = virtualScreenBounds.Width;
            int height = virtualScreenBounds.Height;

            using Bitmap bitmap = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(bitmap);

            g.CopyFromScreen(virtualScreenBounds.Location, Point.Empty, virtualScreenBounds.Size);

            // Adjust for screen DPI scaling
            foreach (Screen screen in Screen.AllScreens)
            {
                float scaleFactorX = g.DpiX / screen.Bounds.Width;
                float scaleFactorY = g.DpiY / screen.Bounds.Height;

                int offsetX = (int)Math.Round(screen.Bounds.X * scaleFactorX);
                int offsetY = (int)Math.Round(screen.Bounds.Y * scaleFactorY);
                int scaledWidth = (int)Math.Round(screen.Bounds.Width * scaleFactorX);
                int scaledHeight = (int)Math.Round(screen.Bounds.Height * scaleFactorY);

                var screenBounds = new System.Drawing.Rectangle(offsetX, offsetY, scaledWidth, scaledHeight);
                g.DrawImage(bitmap, screen.Bounds, screenBounds, GraphicsUnit.Pixel);
            }*/
        }
    }
}
