using OpenCvSharp;
using System.Collections.Generic;
using System.Linq;

namespace NetAutoGUI.Internals
{
	public abstract class AbstractScreenshotController : IScreenshotController
	{
		protected abstract BitmapData LoadImageFromFile(string imageFile);

		public abstract BitmapData Screenshot();

        public abstract BitmapData Screenshot(Window window);

        public Rectangle[] LocateAll(BitmapData basePicture, string imgFileToBeFound, double confidence = 0.99)
		{
            var rectangles = LocateAllWithConfidence(basePicture, imgFileToBeFound, confidence);
            return rectangles.OrderBy(e => e.Rectangle.Y).Select(e => e.Rectangle).ToArray();
		}

		public abstract void Highlight(double waitSeconds = 0.5, params Rectangle[] rectangles);

        /// <summary>
        /// Convert the location of the screenshot to the relative location to the primary screen.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public abstract (int x,int y) ScreenshotLocationToRelativeLocation(int x, int y);

        public RectangleWithConfidence[] LocateAllWithConfidence(BitmapData basePicture, string imgFileToBeFound, double confidence = 0.99)
        {
            var bitmapToBeFound = this.LoadImageFromFile(imgFileToBeFound);
            using Mat matToBeFound = bitmapToBeFound.ToMat();
            using Mat matBasePicture = basePicture.ToMat();
            var rectangles = new List<RectangleWithConfidence>();
            using var result = matBasePicture.MatchTemplate(matToBeFound, TemplateMatchModes.CCoeffNormed);
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
                        (int relativeX, int relativeY) = ScreenshotLocationToRelativeLocation(c, r);
                        Rectangle rectangle = new(relativeX, relativeY, width, height);
                        rectangles.Add(new(rectangle, similarity));
                    }
                }
            }
            return rectangles.ToArray();
        }        
    }
}
