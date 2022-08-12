namespace NetAutoGUI
{
    public interface IServiceLoader
    {
        public IMouseController LoadMouseController();
        public IKeyboardController LoadKeyboardController();
        public IMessageBoxController LoadMessageBoxController();
        public IScreenshotController LoadScreenshotController();
    }
}
