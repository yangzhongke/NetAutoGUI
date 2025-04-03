using System.Runtime.Versioning;
using System.Windows.Forms;

namespace NetAutoGUI.Windows
{
    [SupportedOSPlatform("windows")]
    public class WindowsServiceLoader : IServiceLoader
    {
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

        public IWindowController LoadWindowController()
        {
            return new WindowsWindowController();
        }

        public IKeyboardController LoadKeyboardController()
        {
            return new WindowsKeyboardController();
        }
    }
}
