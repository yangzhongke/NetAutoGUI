using OpenCvSharp;
using System.Collections.Generic;
using System.Linq;
using Point = OpenCvSharp.Point;

namespace NetAutoGUI.Internals
{
    public abstract class AbstractScreenshotController : IScreenshotController
    {
        public Rectangle? LocateOnScreen(string imgFileToBeFound, double confidence = 0.5)
        {
            var items = LocateAllOnScreen(imgFileToBeFound, confidence, maxCount:1);
            if(items.Length<=0)
            {
                return null;
            }
            else
            {
                return items[0];
            }
        }
        protected abstract BitmapData LoadImageFromFile(string imageFile);

        public abstract BitmapData Screenshot(Rectangle? region = null);

        public abstract void Screenshot(string filename,Rectangle? region = null);

        public Location? LocateCenterOnScreen(string imgFileToBeFound, double confidence = 0.5)
        {
            var rect = LocateOnScreen(imgFileToBeFound, confidence);
            if(rect==null)
            {
                return null;
            }
            else
            {
                return new(rect.X,rect.Y);
            }
        }

        public Rectangle[] LocateAllOnScreen(string imgFileToBeFound, double confidence = 0.5, int maxCount = 10)
        {
            BitmapData bitmapToBeFound = LoadImageFromFile(imgFileToBeFound);
            BitmapData bitmapScreen = Screenshot();
            using Mat matToBeFound = bitmapToBeFound.ToMat();
            using Mat matScreen = bitmapScreen.ToMat();
            var rectangles = new List<Rectangle>();
            while (true)
            {
                using (var result = matScreen.MatchTemplate(matToBeFound, TemplateMatchModes.CCoeffNormed))
                {
                    result.MinMaxLoc(out _, out double maxValues, out _, out Point maxLocations);
                    if (maxValues >= confidence)
                    {
                        Rectangle match = new(maxLocations.X, maxLocations.Y, bitmapToBeFound.Width, bitmapToBeFound.Height);
                        rectangles.Add(match);
                        if(rectangles.Count>=maxCount)
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
            return rectangles.Distinct().ToArray();
        }

        public Location[] LocateAllCentersOnScreen(string imgFileToBeFound, double confidence = 0.5, int maxCount = 10)
        {
            List<Location> list = new List<Location>();
            foreach (var rect in LocateAllOnScreen(imgFileToBeFound,confidence,maxCount))
            {
                list.Add(rect.Center);
            }
            return list.ToArray();
        }
    }
}
