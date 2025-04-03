using System;
using System.Text;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows;

public class WindowsWindowController : IWindowController
{
    public Rectangle GetBoundary(Window window)
    {
        User32.GetWindowRect(window.Id.ToHWND(), out RECT rect);
        return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
    }

    public string GetTitle(Window window)
    {
        StringBuilder sb = new StringBuilder(1024);
        User32.GetWindowText(window.Id.ToHWND(), sb, sb.Capacity);
        return sb.ToString();
    }

    public void PressKey(Window window, VirtualKeyCode keyCode)
    {
        HWND hwnd = window.Id.ToHWND();
        User32.SendMessage(hwnd, (uint)User32.WindowMessage.WM_KEYDOWN, (IntPtr)User32.VK.VK_SPACE, IntPtr.Zero);
        User32.SendMessage(hwnd, (uint)User32.WindowMessage.WM_KEYUP, (IntPtr)User32.VK.VK_SPACE, IntPtr.Zero);
    }

    public void Close(Window window)
    {
        HWND hwnd = window.Id.ToHWND();
        User32.SendMessage(hwnd, (uint)User32.WindowMessage.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
    }
}