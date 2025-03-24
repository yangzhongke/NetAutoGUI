using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

namespace NetAutoGUI.Windows;

public class Win32UIElement
{
    private HWND hwnd;

    public Win32UIElement(long hwnd)
    {
        this.hwnd = hwnd.ToHWND();
    }

    public Win32UIElement(HWND hwnd)
    {
        this.hwnd = hwnd;
    }

    public HWND Handle
    {
        get
        {
            return this.hwnd;
        }
    }

    public string ClassName
    {
        get
        {
            int len = 256;
            StringBuilder sbClass = new StringBuilder(len);
            User32.RealGetWindowClass(hwnd, sbClass, (uint)len);
            return sbClass.ToString();
        }
    }
    public string Text
    {
        get
        {
            int txtLen = User32.GetWindowTextLength(hwnd);
            StringBuilder sbText = new StringBuilder(txtLen);
            User32.GetWindowText(hwnd, sbText, txtLen + 1);
            return sbText.ToString();
        }
        set
        {
            User32.SetWindowText(hwnd, value);
        }
    }

    public Win32UIElement? Parent
    {
        get
        {
            var pHwnd = User32.GetParent(hwnd);
            if (pHwnd.IsNull)
            {
                return null;
            }

            return new Win32UIElement(pHwnd);
        }
        set
        {
            if (value == null)
            {
                User32.SetParent(this.hwnd, HWND.NULL);
            }
            else
            {
                User32.SetParent(this.hwnd, value.hwnd);
            }
        }
    }

    public IEnumerable<Win32UIElement> Children
    {
        get
        {
            HWND prevChild = HWND.NULL;
            HWND childHwnd;
            while (true)
            {
                childHwnd = User32.FindWindowEx(hwnd, prevChild, null, null);
                if (childHwnd.IsNull)
                {
                    break;
                }
                else
                {
                    prevChild = childHwnd;
                    if (User32.IsWindowVisible(childHwnd))
                    {
                        yield return new Win32UIElement(childHwnd);
                    }
                }
            }
        }
    }

    private static IEnumerable<HWND> GetAllDescendantWindows(HWND parentHwnd)
    {
        ISet<HWND> descendantWindows = new HashSet<HWND>();
        GetDescendantWindowsRecursive(parentHwnd, descendantWindows);
        return descendantWindows;
    }

    private static void GetDescendantWindowsRecursive(HWND parentHwnd, ISet<HWND> collectedWindows)
    {
        List<HWND> childWindows = new List<HWND>();

        EnumWindowsProc callback = (hWnd, lParam) =>
        {
            if (User32.IsWindowVisible(hWnd))
            {
                childWindows.Add(hWnd);
            }

            return true; // Continue enumeration
        };

        EnumChildWindows(parentHwnd, callback, IntPtr.Zero);

        foreach (var childHwnd in childWindows)
        {
            if (User32.IsWindowVisible(childHwnd))
            {
                collectedWindows.Add(childHwnd);
            }

            GetDescendantWindowsRecursive(childHwnd, collectedWindows); // Recursive call for each child
        }
    }

    public IEnumerable<Win32UIElement> Descendents
    {
        get
        {
            foreach (var descendentHwnd in GetAllDescendantWindows(Handle))
            {
                Win32UIElement child = new Win32UIElement(descendentHwnd);
                yield return child;
            }
        }
    }

    public Rectangle Rectangle
    {
        get
        {
            User32.GetWindowRect(hwnd, out RECT rect);
            return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }
    }

    public void Click()
    {
        User32.SendMessage(hwnd, User32.ButtonMessage.BM_CLICK, 0);
    }

    public void MouseClick()
    {
        GetWindowRect(hwnd, out RECT rect);
        int x = (rect.Left + rect.Right) / 2;
        int y = (rect.Top + rect.Bottom) / 2;
        SetCursorPos(x, y);
        mouse_event(MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN, x, y, 0, IntPtr.Zero);
        Thread.Sleep(50);
        mouse_event(MOUSEEVENTF.MOUSEEVENTF_LEFTUP, x, y, 0, IntPtr.Zero);
    }

    public void Paste()
    {
        User32.SendMessage(hwnd, User32.WindowMessage.WM_PASTE, 0);
    }

    public void SelectAll()
    {
        User32.SendMessage(hwnd, (uint)User32.EditMessage.EM_SETSEL, IntPtr.Zero, new IntPtr(-1));
    }

    public void Copy()
    {
        User32.SendMessage(hwnd, User32.WindowMessage.WM_COPY, 0);
    }

    public void Focus()
    {
        User32.SetFocus(hwnd);
    }

    public bool IsValid()
    {
        return User32.IsWindow(hwnd);
    }

    public BitmapData ToBitmap()
    {
        return ScreenshotHelper.CaptureWindow(hwnd);
    }

    public bool Equals(Win32UIElement obj)
    {
        return obj.hwnd == this.hwnd;
    }
    public override bool Equals(object? obj)
    {
        if (obj is Win32UIElement)
        {
            return Equals(obj, this);
        }
        return base.Equals(obj);
    }

    public static bool operator ==(Win32UIElement? e1, Win32UIElement? e2)
    {
        if ((object?)e1 != null && (object?)e2 != null)
        {
            return Equals(e1, e2);
        }
        else if ((object?)e1 == null && (object?)e2 == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator !=(Win32UIElement? e1, Win32UIElement? e2)
    {
        if ((object?)e1 != null && (object?)e2 != null)
        {
            return !Equals(e1, e2);
        }
        else if ((object?)e1 == null && (object?)e2 == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override int GetHashCode()
    {
        return this.hwnd.GetHashCode();
    }

    public override string ToString()
    {
        return this.hwnd.ToInt64().ToString();
    }
}