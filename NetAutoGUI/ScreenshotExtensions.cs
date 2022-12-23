using System;
using System.Collections.Generic;
using System.Text;

namespace NetAutoGUI
{
    public static class ScreenshotExtensions
    {
        public static BitmapData Screenshot(this IScreenshotController ctl, Window window)
        {
            var rect = window.Rectangle;
            return ctl.Screenshot(rect);
            //todo: enable the screenshot of invisible window by IGraphicsCaptureItemInterop 
            //https://blogs.windows.com/windowsdeveloper/2019/09/16/new-ways-to-do-screen-capture/
        }

        /*
        public static Rectangle? LocateOnWindow(this IScreenshotController ctl, Window window, string imgFileToBeFound, double confidence = 0.99)
        {

        }
        public static void ClickOnWindow(this IScreenshotController ctl, Window window, string imgFileToBeFound, double confidence = 0.99)
        {

        }
        public static void WaitAndClickOnWindow(this IScreenshotController ctl, Window window, string imgFileToBeFound, double confidence = 0.99, double timeoutSeconds = 5)
        {

        }

        public static Location? LocateCenterOnWindow(this IScreenshotController ctl, Window window, string imgFileToBeFound, double confidence = 0.99)
        {

        }

        public static Rectangle[] LocateAllOnWindow(this IScreenshotController ctl, Window window, string imgFileToBeFound, double confidence = 0.99, int maxCount = 5)
        {

        }

        public static Location[] LocateAllCentersOnWindow(this IScreenshotController ctl, Window window, string imgFileToBeFound, double confidence = 0.99, int maxCount = 5)
        {

        }*/
    }
}
