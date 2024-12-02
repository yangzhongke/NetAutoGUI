using System;

namespace NetAutoGUI
{
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
