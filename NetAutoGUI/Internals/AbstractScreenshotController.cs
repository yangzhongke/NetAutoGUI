using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetAutoGUI.Internals
{
    public abstract class AbstractScreenshotController : IScreenshotController
    {
        public Rectangle? LocateOnScreen(string imgFileToBeFound, double confidence = 0.99)
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

        public Location? LocateCenterOnScreen(string imgFileToBeFound, double confidence = 0.99)
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

        public Location[] LocateAllCentersOnScreen(string imgFileToBeFound, double confidence = 0.99, int maxCount = 10)
        {
            List<Location> list = new List<Location>();
            foreach (var rect in LocateAllOnScreen(imgFileToBeFound,confidence,maxCount))
            {
                list.Add(rect.Center);
            }
            return list.ToArray();
        }


        public Rectangle[] LocateAllOnScreen(string imgFileToBeFound, double confidence = 0.99, int maxCount = 5)
        {
            var bitmapScreen = Screenshot();
            var bitmapToBeFound = this.LoadImageFromFile(imgFileToBeFound);
            using Mat matToBeFound = bitmapToBeFound.ToMat();
            using Mat matScreen = bitmapScreen.ToMat();
            var rectangles = new List<(Rectangle Rect, double Confidence)>();
            using (var result = matScreen.MatchTemplate(matToBeFound, TemplateMatchModes.CCoeffNormed))
            {
                var indexer = result.GetGenericIndexer<float>();
                int width = bitmapToBeFound.Width;
                int height = bitmapToBeFound.Height;
                for (int r = 0; r < result.Rows; r++)
                {
                    for (int c = 0; c < result.Cols; c++)
                    {
                        float similarity = indexer[r, c];
                        if (similarity > confidence)
                        {
                            rectangles.Add(new(new(c, r, width, height), similarity));
                            if (rectangles.Count >= maxCount) break;
                        }
                    }
                }
            }
            return rectangles.OrderByDescending(e => e.Confidence).Select(e => e.Rect).ToArray();
        }

        protected abstract void Click(int x, int y);

        public void ClickOnScreen(string imgFileToBeFound, double confidence = 0.99)
        {
            var ptr = LocateCenterOnScreen(imgFileToBeFound, confidence);
            if(ptr==null)
            {
                throw new InvalidOperationException($"image {imgFileToBeFound} not found on the screen");
            }
            else
            {
                (int x, int y) = ptr;
                Click(x, y);
            }
        }

        public abstract void Highlight(params Rectangle[] rectangles);

        public void Highlight(string imgFileToBeFound, double confidence = 0.99, int maxCount = 5)
        {
            var rects = LocateAllOnScreen(imgFileToBeFound, confidence, maxCount);
            Highlight(rects);
        }
    }
}
