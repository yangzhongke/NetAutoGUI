using NetAutoGUI.Internals;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace NetAutoGUI.Windows
{
    internal class WindowsScreenshotController : AbstractScreenshotController
    {
        public override BitmapData Screenshot(Rectangle? region = null)
        {
            using Bitmap bitmap = TakeScreenShot(region);
            using MemoryStream memSteam = new MemoryStream();
            bitmap.Save(memSteam, ImageFormat.Bmp);
            memSteam.Position = 0;
            byte[] data = memSteam.ToArray();
            return new BitmapData(data);
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
            using Image image = Image.FromFile(imageFile);
            using Bitmap bitmap = new Bitmap(image);
            using MemoryStream memStream = new MemoryStream();
            bitmap.Save(memStream, ImageFormat.Bmp);
            memStream.Position = 0;
            byte[] data = memStream.ToArray();
            return new BitmapData(data);
        }
    }
}
