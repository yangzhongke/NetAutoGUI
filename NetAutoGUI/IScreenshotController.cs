using System.Linq;

namespace NetAutoGUI
{
    public interface IScreenshotController
    {
        public BitmapData Screenshot();

        public BitmapData Screenshot(Window window);



        public RectangleWithConfidence[] LocateAllWithConfidence(BitmapData basePicture, BitmapData bitmapToBeFound, double confidence = 0.99);

        public Rectangle[] LocateAll(BitmapData basePicture, BitmapData bitmapToBeFound, double confidence = 0.99)
        {
            var rectangles = LocateAllWithConfidence(basePicture, bitmapToBeFound, confidence);
            return rectangles.OrderBy(e => e.Rectangle.Y).Select(e => e.Rectangle).ToArray();
        }

        public void Highlight(double waitSeconds = 0.5, params Rectangle[] rectangles);
    }
}
