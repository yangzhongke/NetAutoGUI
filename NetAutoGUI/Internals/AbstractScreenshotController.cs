using OpenCvSharp;
using System.Collections.Generic;
using System.Linq;

namespace NetAutoGUI.Internals
{
	public abstract class AbstractScreenshotController : IScreenshotController
	{
		protected abstract BitmapData LoadImageFromFile(string imageFile);

		public abstract BitmapData Screenshot(Rectangle? region = null, uint screenIndex = 0);

		public Rectangle[] LocateAll(BitmapData basePicture, string imgFileToBeFound, double confidence = 0.99)
		{
            var rectangles = LocateAllWithConfidence(basePicture, imgFileToBeFound, confidence);
            return rectangles.OrderBy(e => e.Rectangle.Y).Select(e => e.Rectangle).ToArray();
		}

		public abstract void Highlight(double waitSeconds = 0.5, params Rectangle[] rectangles);

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
                        rectangles.Add(new(new(c, r, width, height), similarity));
                    }
                }
            }
            return rectangles.ToArray();
        }

        public abstract BitmapData ScreenshotAllScreens();
    }
}
