using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetAutoGUI.Internals;

namespace NetAutoGUI
{
    public static class WindowExtensions
    {
        /// <summary>
        /// Translate the relative position to position on screen
        /// </summary>
        /// <param name="winX">x to the window</param>
        /// <param name="winY">y to the window</param>
        /// <param name="window">window</param>
        /// <returns>position relative to the whole screen</returns>
        private static (int x, int y) WindowPosToScreen(this Window window, int winX, int winY)
        {
            var rect = GUI.Window.GetBoundary(window);
            return (winX + rect.X, winY + rect.Y);
        }

        private static (int x, int y) ScreenPosToWindow(this Window window, int screenX, int screenY)
        {
            var rect = GUI.Window.GetBoundary(window);
            return (screenX + rect.X, screenY + rect.Y);
        }

        private static Rectangle WindowRectToScreen(Window window, Rectangle relativeRect)
        {
            var winRect = GUI.Window.GetBoundary(window);
            return new Rectangle(winRect.X + relativeRect.X, winRect.Y + relativeRect.Y, relativeRect.Width, relativeRect.Height);
        }

        private static Rectangle ScreenRectToWindow(Window window, Rectangle screenRect)
        {
            var winRect = GUI.Window.GetBoundary(window);
            return new Rectangle(screenRect.X - winRect.X, screenRect.Y - winRect.Y, screenRect.Width, screenRect.Height);
        }

        /// <summary>
        /// Move the mouse cursor to the specific location
        /// </summary>
        /// <param name="window">window</param>
        /// <param name="winX"></param>
        /// <param name="winY"></param>
        public static void MoveMouseTo(this Window window, int winX, int winY)
        {
            (int x, int y) = WindowPosToScreen(window, winX, winY);
            GUI.Mouse.MoveTo(x, y);
        }

        /// <summary>
        ///  Simulate a single mouse click at the given position relative to the given window<paramref name="window"/>. 
        /// </summary>
        /// <param name="window">window</param>
        /// <param name="winX">mouse x to window origin. The default value is current mouse x.</param>
        /// <param name="winY">mouse y to window origin. The default value is current mouse y. </param>
        /// <param name="button">which mouse button</param>
        /// <param name="clicks">click times</param>
        /// <param name="intervalInSeconds">interval in seconds between clicks</param>
        public static void Click(this Window window, int? winX = null, int? winY = null,
            MouseButtonType button = MouseButtonType.Left, int clicks = 1, double intervalInSeconds = 0)
        {
            if (winX != null && winY != null)
            {
                (int x, int y) = WindowPosToScreen(window, (int)winX, (int)winY);
                GUI.Mouse.Click(x: x, y: y, button: button, clicks: clicks, intervalInSeconds: intervalInSeconds);
            }
            else
            {
                GUI.Mouse.Click(button: button, clicks: clicks, intervalInSeconds: intervalInSeconds);
            }
        }

        /// <summary>
        ///  Simulate a double mouse click at the given position relative to the given window<paramref name="window"/>. 
        /// </summary>
        /// <param name="window">window</param>
        /// <param name="winX">mouse x to window origin. The default value is current mouse x.</param>
        /// <param name="winY">mouse y to window origin. The default value is current mouse y. </param>
        /// <param name="button">which mouse button</param>
        /// <param name="intervalInSeconds">interval in seconds between clicks</param>
        public static void DoubleClick(this Window window, int? winX = null, int? winY = null,
            MouseButtonType button = MouseButtonType.Left, double intervalInSeconds = 0)
        {
            if (winX != null && winY != null)
            {
                (int x, int y) = WindowPosToScreen(window, (int)winX, (int)winY);
                GUI.Mouse.DoubleClick(x: x, y: y, button: button, intervalInSeconds: intervalInSeconds);
            }
            else
            {
                GUI.Mouse.DoubleClick(button: button, intervalInSeconds: intervalInSeconds);
            }
        }

        /// <summary>
        /// Press down a mouse key down on a window
        /// </summary>
        /// <param name="window">window</param>
        /// <param name="winX">x to the window. Default value is the current mouse position.</param>
        /// <param name="winY">y to the window. Default value is the current mouse position.</param>
        /// <param name="button">which button</param>
        public static void MouseDown(this Window window, int? winX = null, int? winY = null, MouseButtonType button = MouseButtonType.Left)
        {
            if (winX != null && winY != null)
            {
                (int x, int y) = WindowPosToScreen(window, (int)winX, (int)winY);
                GUI.Mouse.MouseDown(x: x, y: y, button: button);
            }
            else
            {
                GUI.Mouse.MouseDown(button: button);
            }
        }

        /// <summary>
        /// Release a mouse key down on a window
        /// </summary>
        /// <param name="window">window</param>
        /// <param name="winX">x to the window. Default value is the current mouse position.</param>
        /// <param name="winY">y to the window. Default value is the current mouse position.</param>
        /// <param name="button">which button</param>
        public static void MouseUp(this Window window, int? winX = null, int? winY = null, MouseButtonType button = MouseButtonType.Left)
        {
            if (winX != null && winY != null)
            {
                (int x, int y) = WindowPosToScreen(window, (int)winX, (int)winY);
                GUI.Mouse.MouseUp(x: x, y: y, button: button);
            }
            else
            {
                GUI.Mouse.MouseUp(button: button);
            }
        }

        /// <summary>
        /// Locates all occurrences of a given bitmap within the window with a specified confidence level.
        /// </summary>
        /// <param name="window">The window where the search is performed</param>
        /// <param name="imgFileToBeFound">The image to locate within the window</param>
        /// <param name="confidence">The confidence level required for a match, ranging from 0.0 to 1.0.
        /// A value closer to 1.0 ensures higher accuracy but may result in fewer matches.</param>
        /// <returns>An array of <see cref="Rectangle"/> objects, each representing a located instance of 
        /// <paramref name="imgFileToBeFound"/> within <paramref name="window"/>.</returns>
        public static Rectangle[] LocateAll(this Window window, BitmapData imgFileToBeFound, double confidence = 0.99)
        {
            var winScreenshot = GUI.Screenshot.Screenshot(window);
            return GUI.Screenshot.LocateAll(winScreenshot, imgFileToBeFound, confidence);
        }


        /// <summary>
        /// Highlight several areas 
        /// </summary>
        /// <param name="window">window</param>
        /// <param name="waitSeconds">display for the given seconds before it disappear</param>
        /// <param name="relativeRects">multiple areas to highlight</param>
        public static void Highlight(this Window window, double waitSeconds = 0.5, params Rectangle[] relativeRects)
        {
            Rectangle[] rectsToScreen = relativeRects.Select(r => WindowRectToScreen(window, r)).ToArray();
            GUI.Screenshot.Highlight(waitSeconds, rectsToScreen);
        }

        /// <summary>
        /// Wait for the first matched area(matched with<paramref name="imgFileToBeFound"/>) and click the centre of the area
        /// </summary>
        /// <param name="window">window</param>
        /// <param name="imgFileToBeFound">The image to locate within the window</param>
        /// <param name="confidence">The confidence level required for a match, ranging from 0.0 to 1.0.
        /// A value closer to 1.0 ensures higher accuracy but may result in fewer matches.</param>
        /// <param name="timeoutSeconds">timeout in seconds</param>
        /// <exception cref="TimeoutException">not found after timeout</exception>
        public static void WaitAndClick(this Window window, BitmapData imgFileToBeFound,
            double confidence = 0.99, double timeoutSeconds = 5)
        {
            var rect = Wait(window, imgFileToBeFound, confidence, timeoutSeconds);
            Highlight(window, 2, rect);
            Click(window, rect.Center.X, rect.Center.Y);
        }

        /// <summary>
        /// Wait for the first matched area(matched with<paramref name="imgFileToBeFound"/>) and click the centre of the area
        /// </summary>
        /// <param name="window">window</param>
        /// <param name="imgFileToBeFound">The image to locate within the window</param>
        /// <param name="confidence">The confidence level required for a match, ranging from 0.0 to 1.0.
        /// A value closer to 1.0 ensures higher accuracy but may result in fewer matches.</param>
        /// <param name="timeoutSeconds">timeout in seconds</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <exception cref="TimeoutException">not found after timeout</exception>
        public static async Task WaitAndClickAsync(this Window window, BitmapData imgFileToBeFound,
            double confidence = 0.99, double timeoutSeconds = 5, CancellationToken cancellationToken = default)
        {
            var rect = await WaitAsync(window, imgFileToBeFound, confidence, timeoutSeconds, cancellationToken);
            Click(window, rect.Center.X, rect.Center.Y);
        }


        /// <summary>
        /// Wait for the first matched area(matched with<paramref name="imgFileToBeFound"/>) 
        /// </summary>
        /// <param name="window">window</param>
        /// <param name="imgFileToBeFound">The image to locate within the window</param>
        /// <param name="confidence">The confidence level required for a match, ranging from 0.0 to 1.0.
        /// A value closer to 1.0 ensures higher accuracy but may result in fewer matches.</param>
        /// <param name="timeoutSeconds">timeout in seconds</param>
        /// <returns>Rectangle of the first found area relative to <paramref name="window"/></returns>
        /// <exception cref="TimeoutException">not found after timeout</exception>
        public static Rectangle Wait(this Window window, BitmapData imgFileToBeFound, double confidence = 0.99, double timeoutSeconds = 5)
        {
            var winBitmap = GUI.Screenshot.Screenshot(window);
            return TimeBoundWaiter.WaitForNotNull(
                () => GUI.Screenshot.LocateAll(winBitmap, imgFileToBeFound, confidence).FirstOrDefault(),
                timeoutSeconds, "Cannot find an area within the given time");
        }

        /// <summary>
        /// Wait for the first matched area(matched with<paramref name="imgFileToBeFound"/>) 
        /// </summary>
        /// <param name="window">window</param>
        /// <param name="imgFileToBeFound">The image to locate within the window</param>
        /// <param name="confidence">The confidence level required for a match, ranging from 0.0 to 1.0.
        /// A value closer to 1.0 ensures higher accuracy but may result in fewer matches.</param>
        /// <param name="timeoutSeconds">timeout in seconds</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Rectangle of the first found area</returns>
        /// <exception cref="TimeoutException">not found after timeout</exception>
        public static async Task<Rectangle> WaitAsync(this Window window, BitmapData imgFileToBeFound,
            double confidence = 0.99,
            double timeoutSeconds = 5, CancellationToken cancellationToken = default)
        {
            var winBitmap = GUI.Screenshot.Screenshot(window);
            return await TimeBoundWaiter.WaitForNotNullAsync(
                () => GUI.Screenshot.LocateAll(winBitmap, imgFileToBeFound, confidence).FirstOrDefault(),
                timeoutSeconds, "Cannot find an area within the given time", cancellationToken);
        }
    }
}
