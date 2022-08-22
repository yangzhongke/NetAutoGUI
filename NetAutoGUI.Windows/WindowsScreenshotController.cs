using NetAutoGUI.Internals;
using OpenCvSharp;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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

        private Bitmap ToBitmap(BitmapData data)
        {
            using MemoryStream ms = new MemoryStream(data.Data);
            Bitmap bitmap = new Bitmap(ms);
            return bitmap;
        }

        public override Rectangle[] LocateAllOnScreen(string imgFileToBeFound, double confidence = 0.5, int maxCount = 10)
        {
            var bitmapScreen = Screenshot();
            var bitmapToBeFound = this.LoadImageFromFile(imgFileToBeFound);
            using Mat matToBeFound = bitmapToBeFound.ToMat();
            using Mat matScreen = bitmapScreen.ToMat();
            var rectangles = new List<(Rectangle Rect,double Confidence)>();

            while (true)
            {
                using (var result = matScreen.MatchTemplate(matToBeFound, TemplateMatchModes.CCoeffNormed))
                {
                    result.MinMaxLoc(out _, out double maxValue, out _, out OpenCvSharp.Point maxLocations);
                    if (maxValue >= confidence)
                    {
                        Rectangle match = new(maxLocations.X, maxLocations.Y, bitmapToBeFound.Width, bitmapToBeFound.Height);
                        rectangles.Add(new (match, maxValue));
                        if (rectangles.Count >= maxCount)
                        {
                            break;
                        }
                        matScreen.Rectangle(new Rect(match.X, match.Y, match.Width, match.Height), Scalar.Blue, -1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return rectangles.OrderByDescending(e=>e.Confidence).Select(e=>e.Rect).ToArray();
        }
    }
}
