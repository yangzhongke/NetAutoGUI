namespace NetAutoGUI
{
    public interface IScreenshotController
    {
        public BitmapData Screenshot(Rectangle? region=null);
        public void Screenshot(string filename,Rectangle? region = null);
        public Rectangle? LocateOnScreen(string imgFileToBeFound, double confidence= 0.5);
        public Location? LocateCenterOnScreen(string imgFileToBeFound, double confidence = 0.5);
    }
}
