namespace NetAutoGUI
{
    /// <summary>
    /// A controller for display dialogs
    /// </summary>
    public interface IDialogController
    {
        /// <summary>
        /// The parent window handler for dialogs of the controller.
        /// If no parent is specified, the current process's active window will be used as parent.
        /// </summary>
        public long? Parent { get; set; }

        /// <summary>
        /// Pop up an alert dialog.
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="title">title</param>
        public void Alert(string text, string title = "Alert");

        /// <summary>
        /// Popup a confirmation dialog
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="title">title</param>
        /// <returns>true: [Ok] button is pressed; false: [Cancel] button is pressed.</returns>
        public bool Confirm(string text, string title = "Confirm");

        /// <summary>
        /// Popup a Yes/No dialog
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="title">title</param>
        /// <returns>true: [Yes] button is pressed; false: [No] button is pressed.</returns>
        public bool YesNoBox(string text, string title = "Ask");

        /// <summary>
        /// Popup an entry dialog
        /// </summary>
        /// <param name="title">title</param>
        /// <param name="okText">text of [OK] button, defaulted to be [OK]</param>
        /// <param name="cancelText">text of [Cancel] button, defaulted to be [Cancel]</param>
        /// <returns>The text entered</returns>
        public string? Prompt(string title = "", string? okText = null, string? cancelText = null);

        /// <summary>
        /// Popup an entry dialog for password
        /// </summary>
        /// <param name="title">title</param>
        /// <param name="okText">text of [OK] button, defaulted to be [OK]</param>
        /// <param name="cancelText">text of [Cancel] button, defaulted to be [Cancel]</param>
        /// <returns>The password entered</returns>
        public string? Password(string title = "", string? okText = null, string? cancelText = null);

        /// <summary>
        /// Pop up a folder selection dialog.
        /// </summary>
        /// <returns>the selected path</returns>
        public string? SelectFolder();

        /// <summary>
        /// Pop up a saving file dialog.
        /// </summary>
        /// <param name="filters">The file filters. Example: "txt files (*.txt)|*.txt|All files (*.*)|*.*"</param>
        /// <returns>the selected file path</returns>
        public string? SelectFileForSave(string filters = "");

        /// <summary>
        /// Pop up a loading file dialog
        /// </summary>
        /// <param name="filters">The file filters. Example: "txt files (*.txt)|*.txt|All files (*.*)|*.*"</param>
        /// <returns>the selected file path</returns>
        public string? SelectFileForLoad(string filters = "");
    }
}
