namespace NetAutoGUI
{
    /// <summary>
    /// A rectangle
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="Width"></param>
    /// <param name="Height"></param>
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

        /// <summary>
        /// If the give location <paramref name="loc"/> is within the rectangle.
        /// </summary>
        /// <param name="loc">location</param>
        /// <returns>If it's within or not.</returns>
        public bool Contains(Location loc)
        {
            return loc.X >= X && loc.X <= X + Width && loc.Y >= Y && loc.Y <= Y + Height;
        }

        /// <summary>
        /// Area of the rectangle
        /// </summary>
        public int Area => Width * Height;

        public override string ToString()
        {
            return $"X:{X},Y:{Y},Width:{Width},Height:{Height}";
        }
    }
}
