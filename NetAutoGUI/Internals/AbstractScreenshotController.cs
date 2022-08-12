using OpenCvSharp;

namespace NetAutoGUI.Internals
{
    public abstract class AbstractScreenshotController : IScreenshotController
    {
        public Rectangle? LocateOnScreen(string imgFileToBeFound)
        {
            BitmapData bitmapToBeFound = LoadImageFromFile(imgFileToBeFound);
            BitmapData bitmapScreen = Screenshot();
            using Mat matToBeFound = bitmapToBeFound.ToMat();
            using Mat matScreen = bitmapScreen.ToMat();
            using Mat resultArray = new Mat();
            Cv2.MatchTemplate(matScreen, matToBeFound, resultArray, TemplateMatchModes.CCoeff);
            Cv2.MinMaxLoc(resultArray, out Point minLoc, out Point maxLoc);
            Point matchLoc = maxLoc;
            return new Rectangle(matchLoc.X,matchLoc.Y,matToBeFound.Width,matToBeFound.Height);
        }

        protected abstract BitmapData LoadImageFromFile(string imageFile);

        public abstract BitmapData Screenshot(Rectangle? region = null);

        public abstract void Screenshot(string filename,Rectangle? region = null);
    }
}
