using System;
using System.Collections.Generic;
using System.Text;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows;

public class UIElement
{
    private HWND hwnd;

    public UIElement(long hwnd)
    {
        this.hwnd = hwnd.ToHWND();
    }

    public UIElement(HWND hwnd)
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
    public UIElement? Parent
    {
        get
        {
            var pHwnd = User32.GetParent(hwnd);
            if (pHwnd.IsNull)
            {
                return null;
            }
            return new UIElement(pHwnd);
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

    public IEnumerable<UIElement> Children
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
                    yield return new UIElement(childHwnd);
                }
            }
        }
    }

    public IEnumerable<UIElement> Descendents
    {
        get
        {
            List<HWND> result = new List<HWND>();
            Queue<HWND> queue = new Queue<HWND>();

            queue.Enqueue(this.hwnd);

            while (queue.Count > 0)
            {
                HWND currentWindow = queue.Dequeue();

                User32.EnumChildWindows(currentWindow, (hWnd, _) =>
                {
                    result.Add(hWnd);
                    queue.Enqueue(hWnd); // Add to queue for further processing
                    return true;
                }, IntPtr.Zero);
            }

            foreach (var descendentHwnd in result)
            {
                UIElement child = new UIElement(descendentHwnd);
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

    public bool Equals(UIElement obj)
    {
        return obj.hwnd == this.hwnd;
    }
    public override bool Equals(object? obj)
    {
        if (obj is UIElement)
        {
            return Equals(obj, this);
        }
        return base.Equals(obj);
    }

    public static bool operator ==(UIElement? e1, UIElement? e2)
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

    public static bool operator !=(UIElement? e1, UIElement? e2)
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