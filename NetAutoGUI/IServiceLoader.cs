namespace NetAutoGUI
{
    /// <summary>
    /// Service loader for different OS. 
    /// </summary>
    public interface IServiceLoader
    {
        /// <summary>
        /// Load MouseController
        /// </summary>
        /// <returns></returns>
        public IMouseController LoadMouseController();

        /// <summary>
        /// Load KeyboardController
        /// </summary>
        /// <returns></returns>
        public IKeyboardController LoadKeyboardController();

        /// <summary>
        /// Load DialogController
        /// </summary>
        /// <returns></returns>
        public IDialogController LoadDialogController();

        /// <summary>
        /// Load ScreenshotController
        /// </summary>
        /// <returns></returns>
        public IScreenshotController LoadScreenshotController();

        /// <summary>
        /// Load ApplicationController
        /// </summary>
        /// <returns></returns>
        public IApplicationController LoadApplicationController();
    }
}
