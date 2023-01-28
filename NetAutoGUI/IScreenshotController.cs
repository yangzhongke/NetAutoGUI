namespace NetAutoGUI
{
	public interface IScreenshotController
	{
		public BitmapData Screenshot(Rectangle? region = null);

		public Rectangle[] LocateAll(BitmapData basePicture, string imgFileToBeFound, double confidence = 0.99);

		public void Highlight(double waitSeconds = 0.5, params Rectangle[] rectangles);
	}
}
