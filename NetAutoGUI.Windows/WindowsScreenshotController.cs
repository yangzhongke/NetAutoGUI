using InputSimulatorStandard;
using NetAutoGUI.Internals;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
    internal class WindowsScreenshotController : AbstractScreenshotController
    {
        private IInputSimulator inputSimulator = new InputSimulator();
        public override BitmapData Screenshot(Rectangle? region = null)
        {
            using Bitmap bitmap = TakeScreenShot(region);
            using MemoryStream memSteam = new MemoryStream();
            bitmap.Save(memSteam, ImageFormat.Bmp);
            memSteam.Position = 0;
            byte[] data = memSteam.ToArray();
            return new BitmapData(data,bitmap.Width,bitmap.Height);
        }

        private Bitmap TakeScreenShot(Rectangle? region = null)
        {
            Screen screen = Screen.PrimaryScreen;
            if (region == null)
            {
                region = ScreenshotHelper.ToAutoRect(screen.Bounds);
            }
            Bitmap bitmap = new Bitmap(region.Width, region.Height);
            using Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(region.X, region.Y, 0, 0, new(region.Width, region.Height));
            return bitmap;
        }

        public override void Screenshot(string filename, Rectangle? region = null)
        {
            using Bitmap bitmap = TakeScreenShot(region);
            ImageFormat format = ScreenshotHelper.ParseImageFormat(filename);
            bitmap.Save(filename, format);
        }

        protected override BitmapData LoadImageFromFile(string imageFile)
        {
            using var image = Image.FromFile(imageFile);
            using Bitmap bitmap = new Bitmap(image);
            using MemoryStream memStream = new MemoryStream();
            bitmap.Save(memStream, ImageFormat.Bmp);
            memStream.Position = 0;
            byte[] data = memStream.ToArray();
            return new BitmapData(data, bitmap.Width, bitmap.Height);
        }

        protected override void Click(int x, int y)
        {
            /*
             inputSimulator.Mouse.MoveMouseTo(x, y);
              inputSimulator.Mouse.LeftButtonClick();*/
            
            User32.SetCursorPos(x, y)
                 .CheckReturn(nameof(User32.SetCursorPos));
            User32.mouse_event(User32.MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN, x, y, 0, IntPtr.Zero);
            User32.mouse_event(User32.MOUSEEVENTF.MOUSEEVENTF_LEFTUP, x, y, 0, IntPtr.Zero);
        }

        private static PRECT ToPRECT(Rectangle r)
        {
            return new PRECT(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }
        public override void Highlight(params Rectangle[] rectangles)
        {
            HDC hDC_Desktop = User32.GetDC(HWND.NULL);
            foreach(var rect in rectangles)
            {   
                HBRUSH blueBrush = User32.GetSysColorBrush(SystemColorIndex.COLOR_ACTIVEBORDER);
                User32.FillRect(hDC_Desktop, new RECT(rect.X,rect.Y,rect.X+rect.Width, rect.Y+rect.Height), blueBrush);
            }
            
            Thread.Sleep(1000);
            foreach (var rect in rectangles)
            {
                User32.InvalidateRect(HWND.NULL, ToPRECT(rect), true);
            }
            Thread.Sleep(1000);
        }
    }
}
