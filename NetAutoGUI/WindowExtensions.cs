namespace NetAutoGUI
{
    public static class WindowExtensions
    {
        /// <summary>
        /// Translate the relative position to position on screen
        /// </summary>
        /// <param name="winX"></param>
        /// <param name="winY"></param>
        /// <param name="window"></param>
        /// <returns></returns>
        public static (int x, int y) WindowPosToScreen(this Window window, int winX, int winY)
        {
            return (winX + window.Rectangle.X, winY + window.Rectangle.Y);
        }
    }
}
