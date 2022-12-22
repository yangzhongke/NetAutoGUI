namespace NetAutoGUI.Windows
{
    internal static class ScreenshotHelper
    {
        public static System.Drawing.Rectangle ToSysRect(NetAutoGUI.Rectangle rect)
        {
            return new(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static NetAutoGUI.Rectangle ToAutoRect(System.Drawing.Rectangle rect)
        {
            return new(rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}
