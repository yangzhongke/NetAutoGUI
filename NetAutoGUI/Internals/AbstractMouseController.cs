﻿namespace NetAutoGUI.Internals
{
    public abstract class AbstractMouseController : IMouseController
    {
        public abstract void Click(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left,
            int clicks = 1, double intervalInSeconds = 0);

        public void DoubleClick(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left,
            double intervalInSeconds = 0)
        {
            Click(x, y, button, 2, intervalInSeconds);
        }

        public abstract void MouseDown(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left);

        public abstract void MouseUp(int? x = null, int? y = null, MouseButtonType button = MouseButtonType.Left);

        public void Move(int offsetX, int offsetY)
        {
            (int x, int y) = Position();
            MoveTo(x + offsetX, y + offsetY);
        }

        public abstract void MoveTo(int x, int y);

        public abstract Location Position();

        public abstract void Scroll(int value);
    }
}
