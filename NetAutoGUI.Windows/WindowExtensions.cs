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
            Win32Helpers.ActiveWindow(window.Id.ToHWND());
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