namespace NetAutoGUI
{
    public record Size(int Width, int Height)
    {
        public void Deconstruct(out int width,out int height)
        {
            width = Width;
            height = Height;
        }
    }
}
