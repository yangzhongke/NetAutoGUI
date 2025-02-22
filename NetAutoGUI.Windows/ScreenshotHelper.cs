using SkiaSharp;
using System.Drawing;
using System.Windows.Forms;
using Vanara.PInvoke;
using static Vanara.PInvoke.Gdi32;

namespace NetAutoGUI.Windows
{
    internal static class ScreenshotHelper
    {
        public static BitmapData CaptureWindow(HWND hWnd, int width, int height)
        {
            GUIWindows.CheckIsSetHighDpiModeInvoked();
            //https://blog.walterlv.com/post/win32-and-system-drawing-capture-window-to-bitmap.html
            var windowDC = User32.GetWindowDC(hWnd);
            var compatibleDC = Gdi32.CreateCompatibleDC(windowDC);
            var hBitmap = Gdi32.CreateCompatibleBitmap(windowDC, width, height);
            // Associate a compatible bitmap with compatible memory. If you don't do this, the following pixel bit_block conversion will not take effect on hBitmap.
            var oldHBitmap = Gdi32.SelectObject(compatibleDC, hBitmap);
            var result = Gdi32.BitBlt(compatibleDC, 0, 0, width, height, windowDC, 0, 0, RasterOperationMode.SRCCOPY);
            Win32Error.ThrowLastErrorIfFalse(result);
            try
            {
                using var bmp = Image.FromHbitmap(hBitmap.DangerousGetHandle());
                return bmp.ToBitmapData();
            }
            finally
            {
                Gdi32.SelectObject(compatibleDC, oldHBitmap);
                Gdi32.DeleteObject(hBitmap);
                Gdi32.DeleteDC(compatibleDC);
                User32.ReleaseDC(hWnd, windowDC);
            }
        }

        public static BitmapData CaptureVirtualScreen()
        {
            GUIWindows.CheckIsSetHighDpiModeInvoked();
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

                foreach (Screen screen in Screen.AllScreens)
                {
                    using Bitmap screenCapture = CaptureScreen(screen);
                    int x = screen.Bounds.Left - virtualScreen.X;
                    int y = screen.Bounds.Top - virtualScreen.Y;
                    graphics.DrawImage(screenCapture, x, y);
                }
            }
            return finalImage.ToBitmapData();
        }

        /// <summary>
        /// Invoker should dispose the returned Bitmap
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        static Bitmap CaptureScreen(Screen screen)
        {
            Bitmap bitmap = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(screen.Bounds.Left, screen.Bounds.Top, 0, 0, screen.Bounds.Size);
            }
            return bitmap;
        }
    }
}
