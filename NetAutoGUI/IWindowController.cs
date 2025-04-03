namespace NetAutoGUI;

public interface IWindowController
{
    public Rectangle GetBoundary(Window window);
    public string GetTitle(Window window);
    public void PressKey(Window window, VirtualKeyCode keyCode);
    public void Close(Window window);
}