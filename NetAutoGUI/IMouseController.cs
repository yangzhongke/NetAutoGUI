using System.Threading;
using System.Threading.Tasks;

namespace NetAutoGUI
{
    /// <summary>
    /// Mouse controller, used for simulating mouse events
    /// </summary>
    public interface IMouseController
    {
        /// <summary>
        /// Get current location of the mouse cursor 
        /// </summary>
        /// <returns></returns>
        public Location Position();

        /// <summary>
        /// Move the mouse cursor to the specific location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveTo(int x, int y);

        /// <summary>
        /// move the mouse cursor over a few pixels relative to its current position
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public void Move(int offsetX, int offsetY);

        /// <summary>
        ///  Simulate a single mouse click. 
        /// </summary>
        /// <param name="x">move mouse to (x,y), then click the button</param>
        /// <param name="y">move mouse to (x,y), then click the button</param>
        /// <param name="button">which mouse button to click</param>
        /// <param name="clicks">click count</param>
        /// <param name="intervalInSeconds">interval in seconds between clicks</param>
        public void Click(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left, int clicks = 1,
            double intervalInSeconds = 0);

        /// <summary>
        ///  Simulate a single mouse click. 
        /// </summary>
        /// <param name="x">move mouse to (x,y), then click the button</param>
        /// <param name="y">move mouse to (x,y), then click the button</param>
        /// <param name="button">which mouse button to click</param>
        /// <param name="clicks">click count</param>
        /// <param name="intervalInSeconds">interval in seconds between clicks</param>
        /// <param name="cancellationToken">cancellationToken</param>
        public Task ClickAsync(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left,
            int clicks = 1,
            double intervalInSeconds = 0, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Simulate a double mouse click. 
        /// </summary>
        /// <param name="x">move mouse to (x,y), then click the button</param>
        /// <param name="y">move mouse to (x,y), then click the button</param>
        /// <param name="button">which mouse button to click</param>
        /// <param name="intervalInSeconds">interval in seconds</param>
        public void DoubleClick(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left,
            double intervalInSeconds = 0);

        /// <summary>
        ///  Simulate a double mouse click. 
        /// </summary>
        /// <param name="x">move mouse to (x,y), then click the button</param>
        /// <param name="y">move mouse to (x,y), then click the button</param>
        /// <param name="button">which mouse button to click</param>
        /// <param name="intervalInSeconds">interval in seconds</param>
        public Task DoubleClickSync(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left,
            double intervalInSeconds = 0);

        /// <summary>
        /// Simulate a mouse down
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="button">which button</param>
        public void MouseDown(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left);

        /// <summary>
        /// Simulate a mouse up
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="button">which button</param>
        public void MouseUp(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left);

        /// <summary>
        ///  Scroll the mouse  wheel
        /// </summary>
        /// <param name="value">positive value is for scrolling up, negative is value for scrolling down</param>
        public void Scroll(int value);
    }
}
