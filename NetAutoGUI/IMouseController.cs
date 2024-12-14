namespace NetAutoGUI
{
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
        ///  simulates a single, left-button mouse click at the mouse’s current position. 
        /// </summary>
        /// <param name="x">move mouse to (x,y), then click the button</param>
        /// <param name="y"></param>
        public void Click(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left, int clicks = 1, double interval = 0);

        public void DoubleClick(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left, double interval = 0);

        public void MouseDown(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left);
        public void MouseUp(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left);

        /// <summary>
        ///  scroll the mouse  wheel
        /// </summary>
        /// <param name="value">positive value is for scrolling up, negative is value for scrolling down</param>
        public void Scroll(int value);
    }
}
