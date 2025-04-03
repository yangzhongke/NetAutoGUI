using System;

namespace NetAutoGUI
{
    /// <summary>
    /// A window on desktop
    /// </summary>
    /// <param name="Id">the id/handler of the window</param>
    public class Window
    {
        public long Id { get; private set; }

        public Window(long id)
        {
            Id = id;
        }

        public string Title => GUI.Window.GetTitle(this);
        public Rectangle Boundary => GUI.Window.GetBoundary(this);

        public void PressKey(VirtualKeyCode keyCode)
        {
            GUI.Window.PressKey(this, keyCode);
        }

        public void Close()
        {
            GUI.Window.Close(this);
        }
    }
}
