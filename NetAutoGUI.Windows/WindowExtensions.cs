using NetAutoGUI.Windows;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NetAutoGUI.Internals;
using Vanara.PInvoke;
using WildcardMatch;

namespace NetAutoGUI
{
    public static class WindowExtensions
    {
        public static void Activate(this Window window)
        {
            ActiveWindow(window.Id);
        }

        public static void ActiveWindow(long windowId)
        {
            HWND hWnd = windowId.ToHWND();
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

        public static void Maximize(this Window window)
        {
            HWND hwnd = window.Id.ToHWND();
            User32.ShowWindow(hwnd, ShowWindowCommand.SW_MAXIMIZE);
        }

        public static dynamic GetMainMenu(this Window window)
        {
            HWND hwnd = window.Id.ToHWND();
            return new DynamicMainMenu(hwnd);
        }

        public static Win32UIElement GetRoot(this Window window)
        {
            return new Win32UIElement(window.Id);
        }

        public static Win32UIElement WaitFor(this Window window, Func<Win32UIElement, bool> predicate,
            string errorMessageWhenTimeout,
            double timeoutSeconds = Constants.DefaultWaitSeconds)
        {
            return TimeBoundWaiter.WaitForNotNull(
                () => window.GetRoot().Descendents.FirstOrDefault(predicate),
                timeoutSeconds, errorMessageWhenTimeout);
        }

        static string RemoveAmpChars(this string str)
        {
            return str.Replace("&", "");
        }

        public static Win32UIElement WaitForText(this Window window, string text,
            double timeoutSeconds = Constants.DefaultWaitSeconds)
        {
            return WaitFor(window, w => text == w.Text.RemoveAmpChars(),
                $"Cannot find a control with text={text}", timeoutSeconds);
        }

        public static Win32UIElement WaitForTextLike(this Window window, string wildcardText,
            double timeoutSeconds = Constants.DefaultWaitSeconds)
        {
            return WaitFor(window, w => wildcardText.WildcardMatch(w.Text.RemoveAmpChars()),
                $"Cannot find a control with text like {wildcardText}", timeoutSeconds);
        }
    }
}