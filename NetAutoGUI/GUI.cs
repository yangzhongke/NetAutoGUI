using NetAutoGUI.Internals;
using System;

namespace NetAutoGUI
{
    public static class GUI
    {
        public static readonly IMouseController Mouse;
        public static readonly IKeyboardController Keyboard;
        public static readonly IMessageBoxService MessageBox;

        static GUI()
        {
            if(Environment.OSVersion.Platform== PlatformID.Win32NT)
            {
                Mouse = new WindowsMouseController();
                Keyboard = new WindowsKeyboardController();
                MessageBox = new WinFormMessageBoxService();
            }
            else
            {
                throw new NotImplementedException("Only Windows is supported now, welcome to contribute code to support Linux and MacOS.");
            }
        }
    }
}
