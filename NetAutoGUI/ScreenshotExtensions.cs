using System;
using System.Diagnostics;
using System.Linq;

namespace NetAutoGUI
{
	public static class ScreenshotExtensions
	{
		public static BitmapData Screenshot(this IScreenshotController ctl, Window window, uint screenIndex = 0)
		{
			var rect = window.Rectangle;
			return ctl.Screenshot(rect, screenIndex);
			//todo: enable the screenshot of invisible window by IGraphicsCaptureItemInterop 
			//https://blogs.windows.com/windowsdeveloper/2019/09/16/new-ways-to-do-screen-capture/
		}

		public static Rectangle? LocateOnScreen(this IScreenshotController ctl, string imgFileToBeFound, double confidence = 0.99, uint screenIndex = 0)
		{
			var items = LocateAllOnScreen(ctl, imgFileToBeFound, confidence, screenIndex: screenIndex);
			if (items.Length <= 0)
			{
				return null;
			}
			else
			{
				return items[0];
			}
		}

		public static Rectangle WaitOnScreen(this IScreenshotController ctl, string imgFileToBeFound, double confidence = 0.99, double timeoutSeconds = 5, uint screenIndex = 0)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			Rectangle? rect = null;
			while (sw.ElapsedMilliseconds < timeoutSeconds * 1000 && rect == null)
			{
				rect = LocateAllOnScreen(ctl, imgFileToBeFound, confidence, screenIndex: screenIndex).FirstOrDefault();
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

		public static Rectangle[] LocateAllOnScreen(this IScreenshotController ctrl, string imgFileToBeFound, double confidence = 0.99, uint screenIndex = 0)
		{
			var bitmapScreen = ctrl.Screenshot(screenIndex: screenIndex);
			return ctrl.LocateAll(bitmapScreen, imgFileToBeFound, confidence);
		}

		public static void Highlight(this IScreenshotController ctl, string imgFileToBeFound, double confidence = 0.99, double waitSeconds = 0.5, uint screenIndex = 0)
		{
			var rects = LocateAllOnScreen(ctl, imgFileToBeFound, confidence, screenIndex: screenIndex);
			ctl.Highlight(waitSeconds, rects);
		}
	}
}
