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

        public Location[] LocateAllCentersOnScreen(string imgFileToBeFound, double confidence = 0.5, int maxCount = 10)
        {
            List<Location> list = new List<Location>();
            foreach (var rect in LocateAllOnScreen(imgFileToBeFound,confidence,maxCount))
            {
                list.Add(rect.Center);
            }
            return list.ToArray();
        }

        public abstract Rectangle[] LocateAllOnScreen(string imgFileToBeFound, double confidence = 0.5, int maxCount = 10);
    }
}
