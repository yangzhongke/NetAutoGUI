using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetAutoGUI
{
    /// <summary>
    /// Controller for screenshot
    /// </summary>
    public interface IScreenshotController
    {
        /// <summary>
        /// Take a screenshot. If there are multiple monitors, they will be displayed into a single image with system's multiple displays' arrangement.
        /// On Windows, please invoke GUIWindows.Initialize() at the beginning of application's entry, for example Main() or Program.cs
        /// </summary>
        /// <returns></returns>
        public BitmapData Screenshot();

        /// <summary>
        /// Take a screenshot of a window.
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public BitmapData Screenshot(Window window);

        /// <summary>
        /// Locates all occurrences of a given bitmap within a base image with a specified confidence level.
        /// </summary>
        /// <param name="basePicture">The base image where the search is performed</param>
        /// <param name="bitmapToBeFound">The image to locate within the base image</param>
        /// <param name="confidence">The confidence level required for a match, ranging from 0.0 to 1.0.
        /// A value closer to 1.0 ensures higher accuracy but may result in fewer matches.</param>
        /// <returns>An array of <see cref="RectangleWithConfidence"/> objects, each representing a located instance of 
        /// <paramref name="bitmapToBeFound"/> within <paramref name="basePicture"/>, along with the confidence score.</returns>
        public RectangleWithConfidence[] LocateAllWithConfidence(BitmapData basePicture, BitmapData bitmapToBeFound, double confidence = 0.99);

        /// <summary>
        /// Locates all occurrences of a given bitmap within a base image with a specified confidence level.
        /// </summary>
        /// <param name="basePicture">The base image where the search is performed</param>
        /// <param name="bitmapToBeFound">The image to locate within the base image</param>
        /// <param name="confidence">The confidence level required for a match, ranging from 0.0 to 1.0.
        /// A value closer to 1.0 ensures higher accuracy but may result in fewer matches.</param>
        /// <returns>An array of <see cref="Rectangle"/> objects, each representing a located instance of 
        /// <paramref name="bitmapToBeFound"/> within <paramref name="basePicture"/>.</returns>
        public Rectangle[] LocateAll(BitmapData basePicture, BitmapData bitmapToBeFound, double confidence = 0.99)
        {
            var rectangles = LocateAllWithConfidence(basePicture, bitmapToBeFound, confidence);
            return rectangles.OrderBy(e => e.Rectangle.Y).Select(e => e.Rectangle).ToArray();
        }

        /// <summary>
        /// Highlight several areas 
        /// </summary>
        /// <param name="waitSeconds">display for the given seconds before it disappear</param>
        /// <param name="rectangles">multiple areas to highlight</param>
        public void Highlight(double waitSeconds = 0.5, params Rectangle[] rectangles);

        /// <summary>
        /// Highlight several areas 
        /// </summary>
        /// <param name="waitSeconds">display for the given seconds before it disappear</param>
        /// <param name="rectangles">multiple areas to highlight</param>
        /// <param name="cancellationToken"></param>
        public Task HighlightAsync(double waitSeconds, Rectangle[] rectangles,
            CancellationToken cancellationToken = default);
    }
}
