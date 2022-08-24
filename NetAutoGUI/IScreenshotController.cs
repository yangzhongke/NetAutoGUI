namespace NetAutoGUI
{
    public interface IScreenshotController
    {
        public BitmapData Screenshot(Rectangle? region=null);
        public void Screenshot(string filename,Rectangle? region = null);
        public Rectangle? LocateOnScreen(string imgFileToBeFound, double confidence= 0.99);
        public void ClickOnScreen(string imgFileToBeFound, double confidence = 0.99);
        public Location? LocateCenterOnScreen(string imgFileToBeFound, double confidence = 0.99);
        public Rectangle[] LocateAllOnScreen(string imgFileToBeFound, double confidence = 0.99, int maxCount=5);
        public Location[] LocateAllCentersOnScreen(string imgFileToBeFound, double confidence = 0.99, int maxCount = 5);
    }
}
