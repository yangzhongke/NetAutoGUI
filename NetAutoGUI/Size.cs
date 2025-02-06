namespace NetAutoGUI
{
    /// <summary>
    /// Size
    /// </summary>
    /// <param name="Width">Width</param>
    /// <param name="Height">Height</param>
    public record Size(int Width, int Height)
    {
        public void Deconstruct(out int width, out int height)
        {
            width = Width;
            height = Height;
        }
    }
}
