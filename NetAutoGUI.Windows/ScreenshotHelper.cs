using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Vanara.PInvoke;
using static Vanara.PInvoke.Gdi32;

namespace NetAutoGUI.Windows
{
	internal static class ScreenshotHelper
	{
		public static System.Drawing.Rectangle ToSysRect(NetAutoGUI.Rectangle rect)
		{
			return new(rect.X, rect.Y, rect.Width, rect.Height);
		}

		public static NetAutoGUI.Rectangle ToAutoRect(System.Drawing.Rectangle rect)
		{
			return new(rect.X, rect.Y, rect.Width, rect.Height);
		}

		public static byte[] CaptureWindow(HWND hWnd, int width, int height)
		{
			//https://blog.walterlv.com/post/win32-and-system-drawing-capture-window-to-bitmap.html

			var wdc = User32.GetWindowDC(hWnd);
			var cdc = Gdi32.CreateCompatibleDC(wdc);
			var hBitmap = Gdi32.CreateCompatibleBitmap(wdc, width, height);
			// 关联兼容位图和兼容内存，不这么做，下面的像素位块（bit_block）转换不会生效到 hBitmap。
			var oldHBitmap = Gdi32.SelectObject(cdc, hBitmap);
			// 注：使用 GDI+ 截取“使用硬件加速过的”应用时，截取到的部分是全黑的。
			var result = Gdi32.BitBlt(cdc, 0, 0, width, height, wdc, 0, 0, RasterOperationMode.SRCCOPY);
			Win32Error.ThrowLastErrorIfFalse(result);
			try
			{
				using var bmp = Image.FromHbitmap(hBitmap.DangerousGetHandle());
				using var ms = new MemoryStream();
				bmp.Save(ms, ImageFormat.Bmp);
				ms.Seek(0, SeekOrigin.Begin);
				var data = ms.ToArray();
				return data;
			}
			finally
			{
				Gdi32.SelectObject(cdc, oldHBitmap);
				Gdi32.DeleteObject(hBitmap);
				Gdi32.DeleteDC(cdc);
				User32.ReleaseDC(hWnd, wdc);
			}
		}

        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        public static Bitmap CaptureVirtualScreen()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);//Crucial for multi screen and multi scale

            var screens = Screen.AllScreens;

            Rectangle virtualScreen = new Rectangle(
                SystemInformation.VirtualScreen.Left,
                SystemInformation.VirtualScreen.Top,
                SystemInformation.VirtualScreen.Width,
                SystemInformation.VirtualScreen.Height
            );

            Bitmap finalImage = new Bitmap(virtualScreen.Width, virtualScreen.Height);
            using (Graphics graphics = Graphics.FromImage(finalImage))
            {
                graphics.FillRectangle(Brushes.Black, 0, 0, finalImage.Width, finalImage.Height);

                foreach (Screen screen in screens)
                {
                    float scaleX = GetScreenScaleFactor(screen, out float scaleY);

                    using Bitmap screenCapture = CaptureScreen(screen, scaleX, scaleY);

                    int x = screen.Bounds.Left - virtualScreen.X;
                    int y = screen.Bounds.Top - virtualScreen.Y;

                    graphics.DrawImage(screenCapture, x, y, screenCapture.Width, screenCapture.Height);
                    screenCapture.Dispose();
                }
            }
            return finalImage;
        }

        static float GetScreenScaleFactor(Screen screen, out float scaleY)
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(typeof(DEVMODE));
            EnumDisplaySettings(screen.DeviceName, -1, ref dm);

            var scalingFactorX = Math.Round(Decimal.Divide(dm.dmPelsWidth, screen.Bounds.Width), 2);
            var scalingFactorY = Math.Round(Decimal.Divide(dm.dmPelsHeight, screen.Bounds.Height), 2);
            scaleY = (float)scalingFactorY;
            return (float)scalingFactorX;
        }

        static Bitmap CaptureScreen(Screen screen, float scaleX, float scaleY)
        {
            int width = (int)(screen.Bounds.Width * scaleX);
            int height = (int)(screen.Bounds.Height * scaleY);

            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(screen.Bounds.Left, screen.Bounds.Top, 0, 0, new System.Drawing.Size(width, height));
            }
            return bitmap;
        }
    }
}
