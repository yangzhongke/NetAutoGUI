using System;

namespace NetAutoGUI
{
    /// <summary>
    /// A window on desktop
    /// </summary>
    /// <param name="Id">the id/handler of the window</param>
    /// <param name="getRectFunc">a function used for getting the window's rectangle</param>
    /// <param name="getTitleFunc">a function used for getting the window's title</param>
    public record Window(long Id, Func<long, Rectangle> getRectFunc,
        Func<long, string> getTitleFunc)
    {
        public string Title
        {
            get
            {
                return getTitleFunc(Id);
            }
        }
        public Rectangle Rectangle
        {
            get
            {
                return getRectFunc(Id);
            }
        }
    }
}
