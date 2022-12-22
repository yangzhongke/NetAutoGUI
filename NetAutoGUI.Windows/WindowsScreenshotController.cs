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
        public override BitmapData Screenshot(Rectangle? region = null)
        {
            Screen screen = Screen.PrimaryScreen;
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

        public override BitmapData Screenshot(Window window)
        {
            var rect = window.Rectangle;
            return Screenshot(rect);
            //todo: enable the screenshot of invisible window by IGraphicsCaptureItemInterop 
            //https://blogs.windows.com/windowsdeveloper/2019/09/16/new-ways-to-do-screen-capture/
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
            MouseHelper.MoveTo(x, y);
            MouseHelper.LeftButtonClick();
        }

        private static PRECT ToPRECT(Rectangle r)
        {
            return new PRECT(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }
        public override void Highlight(double waitSeconds=0.5,params Rectangle[] rectangles)
        {
            HDC hDC_Desktop = User32.GetDC(HWND.NULL);
            foreach(var rect in rectangles)
            {   
                HBRUSH blueBrush = User32.GetSysColorBrush(SystemColorIndex.COLOR_ACTIVEBORDER);
                User32.FillRect(hDC_Desktop, new RECT(rect.X,rect.Y,rect.X+rect.Width, rect.Y+rect.Height), blueBrush);
            }
            Thread.Sleep((int)(waitSeconds * 1000));
            foreach (var rect in rectangles)
            {
                User32.InvalidateRect(HWND.NULL, ToPRECT(rect), true);
            }            
        }
    }
}
