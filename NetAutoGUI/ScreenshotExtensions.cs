using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetAutoGUI.Internals;

namespace NetAutoGUI
{
    public static class ScreenshotExtensions
    {
        public static Rectangle? LocateOnScreen(this IScreenshotController ctl, BitmapData imgFileToBeFound, double confidence = 0.99)
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

        public static Rectangle WaitOnScreen(this IScreenshotController ctl, BitmapData imgFileToBeFound, double confidence = 0.99, double timeoutSeconds = 5)
        {
            return TimeBoundWaiter.WaitForNotNull(
                () => LocateAllOnScreen(ctl, imgFileToBeFound, confidence).FirstOrDefault(), timeoutSeconds,
                "Cannot find an area within the given time");
        }

        public static Rectangle WaitOnScreen(this IScreenshotController ctl, string imgFileToBeFound,
            double confidence = 0.99, double timeoutSeconds = 5)
        {
            return WaitOnScreen(ctl, BitmapData.FromFile(imgFileToBeFound), confidence, timeoutSeconds);
        }

        public static void ClickOnScreen(this IScreenshotController ctl, string imgFileToBeFound,
            double confidence = 0.99, double timeoutSeconds = 5)
        {
            BitmapData bitmapData = BitmapData.FromFile(imgFileToBeFound);
            var center = WaitOnScreen(ctl, bitmapData, confidence, timeoutSeconds).Center;
            GUI.Mouse.Click(center.X, center.Y);
        }
        
        public static async Task<Rectangle> WaitOnScreenAsync(this IScreenshotController ctl,
            BitmapData imgFileToBeFound,
            double confidence = 0.99, double timeoutSeconds = 5, CancellationToken cancellationToken = default)
        {
            return await TimeBoundWaiter.WaitForNotNullAsync(
                () => LocateAllOnScreen(ctl, imgFileToBeFound, confidence).FirstOrDefault(), timeoutSeconds,
                "Cannot find an area within the given time",
                cancellationToken);
        }

        public static Rectangle[] LocateAllOnScreen(this IScreenshotController ctrl, BitmapData imgFileToBeFound, double confidence = 0.99)
        {
            var bitmapScreen = ctrl.Screenshot();
            return ctrl.LocateAll(bitmapScreen, imgFileToBeFound, confidence);
        }

        public static void Highlight(this IScreenshotController ctl, BitmapData imgFileToBeFound, double confidence = 0.99, double waitSeconds = 0.5)
        {
            var rects = LocateAllOnScreen(ctl, imgFileToBeFound, confidence);
            ctl.Highlight(waitSeconds, rects);
        }
    }
}
