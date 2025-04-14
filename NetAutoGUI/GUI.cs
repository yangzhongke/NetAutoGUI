using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

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

        public static PauseMethod PauseMethod { get; set; } = PauseMethod.SpinWait;

        public static void Pause(double seconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
            switch (PauseMethod)
            {
                case PauseMethod.Sleep:
                    Thread.Sleep(timeSpan);
                    break;
                case PauseMethod.SpinWait:
                    var sw = Stopwatch.StartNew();
                    while (sw.Elapsed < timeSpan)
                    {
                        Thread.SpinWait(100);
                    }

                    sw.Stop();
                    break;
                default:
                    throw new NotSupportedException(PauseMethod.ToString());
            }
        }

        public static void WaitFor(Func<bool> condition, double seconds = 1)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (seconds <= 0) throw new ArgumentOutOfRangeException(nameof(seconds));
            var sw = Stopwatch.StartNew();
            while (condition() == false)
            {
                if (sw.ElapsedMilliseconds > seconds * 1000)
                {
                    throw new TimeoutException();
                }

                Pause(10);
            }
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
