using System;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows;

public static class Win32Helpers
{
    public static void ActiveWindow(HWND hWnd)
    {
        if (Win32Helpers.GetRootWindow(hWnd) != hWnd)
        {
            throw new ArgumentException(nameof(hWnd),
                "Only handle to root Window is supported.");
        }

        User32.ShowWindow(hWnd, ShowWindowCommand.SW_RESTORE);

        //In some scenarios, there is a classic "window just blinks in the taskbar" issue;
        //This happens because Windows prevents background applications from stealing focus for security and usability reasons.

        uint targetThreadId = User32.GetWindowThreadProcessId(hWnd, out _);
        uint currentThreadId = Kernel32.GetCurrentThreadId();

        // Attach input threads to set focus properly
        User32.AttachThreadInput(currentThreadId, targetThreadId, true);

        //In some cases, even AttachThreadInput, BringWindowToTop, and SetFocus etc
        //have been used, the window still just blinks in the task bar rather than being actived,
        //This tricks Windows into thinking the user initiated the focus:
        //This works more reliably because SetForegroundWindow will allow the change if the user “pressed” ALT.

        User32.keybd_event((byte)User32.VK.VK_MENU, 0, 0); // ALT down
        User32.SetForegroundWindow(hWnd);
        User32.keybd_event((byte)User32.VK.VK_MENU, 0, User32.KEYEVENTF.KEYEVENTF_KEYUP); // ALT up

        User32.BringWindowToTop(hWnd);
        User32.SetFocus(hWnd);

        User32.AttachThreadInput(currentThreadId, targetThreadId, false);
    }

    public static HWND GetRootWindow(HWND hWnd)
    {
        var hWndWindow = User32.GetAncestor(hWnd, User32.GetAncestorFlag.GA_ROOT);
        Win32Error.ThrowLastError();
        return hWndWindow;
    }
}