namespace NetAutoGUI
{
    public record Rectangle(int X, int Y, int Width, int Height)
    {
        public void Deconstruct(out int x, out int y, out int width, out int height)
        {
            x = X;
            y = Y;
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Center point of the Rectangle
        /// </summary>
        public Location Center
        {
            get
            {
                int centerX = X + Width / 2;
                int centerY = Y + Height / 2;
                return new Location(centerX, centerY);
            }
        }
    }
}
