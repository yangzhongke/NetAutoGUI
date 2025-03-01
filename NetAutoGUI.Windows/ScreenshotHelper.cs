using System;
using System.Drawing;
using System.Windows.Forms;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
    internal static class ScreenshotHelper
    {
        public static BitmapData CaptureWindow(HWND hWnd)
        {
            // DWM is only available on Windows Vista and later;
            // On some server systems, DWM is not available either.
            // In this case, we use PrintWindow API instead.
            // CaptureWindowUsingDwm can capture the GPU-accelarated window,
            // while CaptureWindowUsingPrintWindow can only capture as a black picture from the GPU-accelarated window.
            if (IsDwmEnabled())
            {
                return CaptureWindowUsingDwm(hWnd);
            }
            else
            {
                return CaptureWindowUsingPrintWindow(hWnd);
            }
        }

        private static bool IsDwmEnabled()
        {
            try
            {
                DwmApi.DwmIsCompositionEnabled(out bool enabled);
                return enabled;
            }
            catch
            {
                return false;
            }
        }

        private static BitmapData CaptureWindowUsingDwm(HWND hWnd)
        {
            DwmApi.DwmGetWindowAttribute<RECT>(hWnd, DwmApi.DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS, out RECT rect); // DWMWA_EXTENDED_FRAME_BOUNDS

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            using Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(rect.Left, rect.Top, 0, 0, new System.Drawing.Size(width, height), CopyPixelOperation.SourceCopy);
            }
            return bmp.ToBitmapData();
        }

        public static BitmapData CaptureWindowUsingPrintWindow(HWND hWnd)
        {
            if (!User32.GetWindowRect(hWnd, out RECT rect))
                throw new Exception("Failed to get window rectangle.");

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            using Bitmap bmp = new(width, height);
            using Graphics g = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = g.GetHdc();
            bool success = User32.PrintWindow(hWnd, hdcBitmap, 0);
            g.ReleaseHdc(hdcBitmap);

            if (!success)
                throw new Exception("PrintWindow failed!");

            return bmp.ToBitmapData();
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
        /// Invoker should dispose the returned Bitmap.
        /// It uses Dwm, so it can capture the GPU-accelarated window.
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
