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
                return bmp.ToBitmapData();
            }
            finally
            {
                Gdi32.SelectObject(cdc, oldHBitmap);
                Gdi32.DeleteObject(hBitmap);
                Gdi32.DeleteDC(cdc);
                User32.ReleaseDC(hWnd, wdc);
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
