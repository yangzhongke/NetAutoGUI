using OpenCvSharp;

namespace NetAutoGUI.Internals
{
    public abstract class AbstractScreenshotController : IScreenshotController
    {
        public Rectangle? LocateOnScreen(string imgFileToBeFound, double confidence = 0.5)
        {
            BitmapData bitmapToBeFound = LoadImageFromFile(imgFileToBeFound);
            BitmapData bitmapScreen = Screenshot();
            using Mat matToBeFound = bitmapToBeFound.ToMat();
            using Mat matScreen = bitmapScreen.ToMat();
            using Mat resultArray = new Mat();

            //https://pythontechworld.com/article/detail/wIQ6jh0GzaYu
            Cv2.MatchTemplate(matScreen, matToBeFound, resultArray, TemplateMatchModes.CCoeffNormed);
            Cv2.MinMaxLoc(resultArray, out _, out double maxValue, out _, out OpenCvSharp.Point point);
            if (maxValue >= confidence)
            {
                return new Rectangle(point.X, point.Y,bitmapToBeFound.Width,bitmapToBeFound.Height);
            }
            else
            {
                return null;
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
    }
}
