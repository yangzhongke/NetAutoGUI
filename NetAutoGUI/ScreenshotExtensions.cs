using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public static Task<Rectangle> WaitOnScreenAsync(this IScreenshotController ctl,
            BitmapData imgFileToBeFound,
            double confidence = 0.99, double timeoutSeconds = 5, CancellationToken cancellationToken = default)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Rectangle? rect = null;
            while (sw.ElapsedMilliseconds < timeoutSeconds * 1000 && rect == null)
            {
                rect = LocateAllOnScreen(ctl, imgFileToBeFound, confidence).FirstOrDefault();
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }
            }

            if (rect == null)
            {
                throw new InvalidOperationException($"image {imgFileToBeFound} not found on the screen");
            }
            else
            {
                return Task.FromResult(rect);
            }
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
