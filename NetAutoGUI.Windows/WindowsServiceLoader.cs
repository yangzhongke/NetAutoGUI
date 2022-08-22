using System.Runtime.Versioning;

namespace NetAutoGUI.Windows
{
    [SupportedOSPlatform("windows")]
    public class WindowsServiceLoader : IServiceLoader
    {
        public IKeyboardController LoadKeyboardController()
        {
            return new WindowsKeyboardController();
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
    }
}
