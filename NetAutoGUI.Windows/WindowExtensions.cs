using NetAutoGUI.Windows;
using System;
using System.Threading;
using Vanara.PInvoke;

namespace NetAutoGUI
{
    public static class WindowExtensions
    {
        public static void Activate(this Window window)
        {
            ActiveWindow(window.Id);
            Thread.Sleep(100);
        }

        public static void ActiveWindow(long windowId)
        {
            //https://stackoverflow.com/questions/2636721/bring-another-processes-window-to-foreground-when-it-has-showintaskbar-false
            HWND hwnd = windowId.ToHWND();
            if (User32.IsIconic(hwnd))
            {
                User32.ShowWindow(hwnd, ShowWindowCommand.SW_RESTORE);
            }
            User32.SetForegroundWindow(hwnd);
        }

        public static void Maximize(this Window window)
        {
            HWND hwnd = window.Id.ToHWND();
            User32.ShowWindow(hwnd, ShowWindowCommand.SW_MAXIMIZE);
            Thread.Sleep(100);
        }

        public static dynamic GetMainMenu(this Window window)
        {
            HWND hwnd = window.Id.ToHWND();
            return new DynamicMainMenu(hwnd);
        }

        public static UIElement GetRoot(this Window window)
        {
            return new UIElement(window.Id);
        }
    }
}