using System.Runtime.Versioning;
using System.Windows.Forms;

namespace NetAutoGUI.Windows
{
    [SupportedOSPlatform("windows")]
    public class WindowsServiceLoader : IServiceLoader
    {
        public WindowsServiceLoader()
        {
            //Only this is set, we can get the correct virtual screen shot for multiple monitors with different scale.
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        }

        public IDialogController LoadDialogController()
        {
            return new WinFormDialogController();
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
