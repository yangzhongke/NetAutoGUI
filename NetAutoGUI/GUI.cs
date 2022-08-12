using NetAutoGUI.Internals;
using System;
using System.IO;
using System.Reflection;

namespace NetAutoGUI
{
    public static class GUI
    {
        public static readonly IMouseController Mouse;
        public static readonly IKeyboardController Keyboard;
        public static readonly IMessageBoxController MessageBox;

        static GUI()
        {
            if(Environment.OSVersion.Platform== PlatformID.Win32NT)
            {
                Assembly asm;
                try
                {
                    asm = Assembly.Load(new AssemblyName("NetAutoGUI.Windows"));
                }
                catch(FileNotFoundException)
                {
                    throw new InvalidOperationException("Assembly of NetAutoGUI.Windows is not found, please: Install-Package NetAutoGUI.Windows");
                }
                var typeMouseCtrl = asm.GetType("NetAutoGUI.Windows.WindowsMouseController");
                Mouse = Activator.CreateInstance(typeMouseCtrl) as IMouseController;
                var typeKeyboard = asm.GetType("NetAutoGUI.Windows.WindowsKeyboardController");
                Keyboard = Activator.CreateInstance(typeKeyboard) as IKeyboardController;
                var typeMsgBox = asm.GetType("NetAutoGUI.Windows.WinFormMessageBoxController");
                MessageBox = Activator.CreateInstance(typeMsgBox) as IMessageBoxController;
            }
            else
            {
                throw new NotImplementedException("Only Windows is supported now, welcome to contribute code to support Linux and MacOS.");
            }
        }
    }
}
