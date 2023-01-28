using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
	}
}
