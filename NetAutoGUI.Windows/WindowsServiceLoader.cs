using NetAutoGUI.Internals;
using System.Runtime.Versioning;
using System.Threading;

namespace NetAutoGUI.Windows
{
    [SupportedOSPlatform("windows")]
    public class WindowsServiceLoader : IServiceLoader
    {
        public WindowsServiceLoader()
        {
			//Clipboard operation requires STA
			Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
			Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
		}

        public IMessageBoxController LoadMessageBoxController()
        {
            return new WinFormMessageBoxController();
        }

        public IMouseController LoadMouseController()
        {
            return new WindowsMouseController();
        }

        public IScreenshotController LoadScreenshotController()
        {
            return new WindowsScreenshotController();
        }

        public IApplicationController LoadApplicationController()
        {
            return new WindowsApplicationController();
        }

        public IKeyboardController LoadKeyboardController()
        {
            return new WindowsKeyboardController();
        }
    }
}
