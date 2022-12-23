namespace NetAutoGUI
{
    public interface IScreenshotController
    {
        public BitmapData Screenshot(Rectangle? region=null);

        public Rectangle? LocateOnScreen(string imgFileToBeFound, double confidence= 0.99);
        public void ClickOnScreen(string imgFileToBeFound, double confidence = 0.99);
        public void WaitAndClickOnScreen(string imgFileToBeFound, double confidence = 0.99, double timeoutSeconds = 5);
        public Location? LocateCenterOnScreen(string imgFileToBeFound, double confidence = 0.99);
        public Rectangle[] LocateAllOnScreen(string imgFileToBeFound, double confidence = 0.99);
        public Location[] LocateAllCentersOnScreen(string imgFileToBeFound, double confidence = 0.99);

        public void Highlight(double waitSeconds = 0.5,params Rectangle[] rectangles);
        public void Highlight(string imgFileToBeFound, double confidence = 0.99, double seconds=0.5);
    }
}
