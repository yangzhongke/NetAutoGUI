namespace NetAutoGUI
{
    public interface IServiceLoader
    {
        public IMouseController LoadMouseController();
        public IKeyboardController LoadKeyboardController();
        public IDialogController LoadDialogController();
        public IScreenshotController LoadScreenshotController();
        public IApplicationController LoadApplicationController();
    }
}
