using System;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
    internal static class HWNDExtensions
    {
        public static HWND ToHWND(this long value)
        {
            return new HWND(new IntPtr(value));
        }

        public static long ToInt64(this HWND value)
        {
            return ((IntPtr)value).ToInt64();
        }
    }
}
