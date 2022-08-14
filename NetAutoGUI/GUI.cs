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
        public static readonly IScreenshotController Screenshot;
        public static readonly IApplicationController Application;

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
                Type serviceLoaderType = asm.GetType("NetAutoGUI.Windows.WindowsServiceLoader");
                IServiceLoader serviceLoader = (IServiceLoader)Activator.CreateInstance(serviceLoaderType);
                Mouse = serviceLoader.LoadMouseController();
                Keyboard = serviceLoader.LoadKeyboardController();
                MessageBox = serviceLoader.LoadMessageBoxController();
                Screenshot = serviceLoader.LoadScreenshotController();
                Application = serviceLoader.LoadApplicationController();
            }
            else
            {
                throw new NotImplementedException("Only Windows is supported now, welcome to contribute code to support Linux and MacOS.");
            }
        }
    }
}
