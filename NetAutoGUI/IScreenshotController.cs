namespace NetAutoGUI
{
	public interface IScreenshotController
	{
		public BitmapData Screenshot(Rectangle? region = null, uint screenIndex = 0);

		public Rectangle[] LocateAll(BitmapData basePicture, string imgFileToBeFound, double confidence = 0.99);

        public RectangleWithConfidence[] LocateAllWithConfidence(BitmapData basePicture, string imgFileToBeFound, double confidence = 0.99);

        public void Highlight(double waitSeconds = 0.5, params Rectangle[] rectangles);
		public BitmapData ScreenshotAllScreens();
	}
}
