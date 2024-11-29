using System;
using System.Diagnostics;
using System.Linq;

namespace NetAutoGUI
{
	public static class ScreenshotExtensions
	{
		public static Rectangle? LocateOnScreen(this IScreenshotController ctl, string imgFileToBeFound, double confidence = 0.99)
		{
			var items = LocateAllOnScreen(ctl, imgFileToBeFound, confidence);
			if (items.Length <= 0)
			{
				return null;
			}
			else
			{
				return items[0];
			}
		}

		public static Rectangle WaitOnScreen(this IScreenshotController ctl, string imgFileToBeFound, double confidence = 0.99, double timeoutSeconds = 5)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			Rectangle? rect = null;
			while (sw.ElapsedMilliseconds < timeoutSeconds * 1000 && rect == null)
			{
				rect = LocateAllOnScreen(ctl, imgFileToBeFound, confidence).FirstOrDefault();
			}
			if (rect == null)
			{
				throw new InvalidOperationException($"image {imgFileToBeFound} not found on the screen");
			}
			else
			{
				return rect;
			}
		}

		public static Rectangle[] LocateAllOnScreen(this IScreenshotController ctrl, string imgFileToBeFound, double confidence = 0.99)
		{
			var bitmapScreen = ctrl.Screenshot();
			return ctrl.LocateAll(bitmapScreen, imgFileToBeFound, confidence);
		}

		public static void Highlight(this IScreenshotController ctl, string imgFileToBeFound, double confidence = 0.99, double waitSeconds = 0.5)
		{
			var rects = LocateAllOnScreen(ctl, imgFileToBeFound, confidence);
			ctl.Highlight(waitSeconds, rects);
		}
	}
}
