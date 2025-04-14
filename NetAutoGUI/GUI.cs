using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace NetAutoGUI
{
    /// <summary>
    /// Entry of controllers
    /// </summary>
    public static class GUI
    {
        public static readonly IMouseController Mouse;
        public static readonly IKeyboardController Keyboard;
        public static readonly IDialogController Dialog;
        public static readonly IScreenshotController Screenshot;
        public static readonly IApplicationController Application;
        internal static readonly IWindowController Window;

        /// <summary>
        /// delay(seconds) after each call that may require some delay.
        /// </summary>
        public static double PauseInSeconds { get; set; } = 0.1;

        public static void Pause()
        {
            Thread.Sleep(TimeSpan.FromSeconds(PauseInSeconds));
        }

        public static async Task PauseAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Delay(TimeSpan.FromSeconds(PauseInSeconds), cancellationToken);
        }

        static GUI()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Assembly asm;
                try
                {
                    asm = Assembly.Load(new AssemblyName("NetAutoGUI.Windows"));
                }
                catch (FileNotFoundException)
                {
                    throw new InvalidOperationException("Assembly of NetAutoGUI.Windows is not found, please: Install-Package NetAutoGUI.Windows");
                }
                Type serviceLoaderType = asm.GetType("NetAutoGUI.Windows.WindowsServiceLoader");
                IServiceLoader serviceLoader = (IServiceLoader)Activator.CreateInstance(serviceLoaderType);
                Mouse = serviceLoader.LoadMouseController();
                Keyboard = serviceLoader.LoadKeyboardController();
                Dialog = serviceLoader.LoadDialogController();
                Screenshot = serviceLoader.LoadScreenshotController();
                Application = serviceLoader.LoadApplicationController();
                Window = serviceLoader.LoadWindowController();
            }
            else
            {
                throw new NotImplementedException("Only Windows is supported now, welcome to contribute code to support Linux and MacOS.");
            }
        }
    }
}
