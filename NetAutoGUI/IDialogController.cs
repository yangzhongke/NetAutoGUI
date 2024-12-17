namespace NetAutoGUI
{
    public interface IDialogController
    {
        public long? Parent { get; set; }

        public void Alert(string text, string title = "Alert");
        public bool Confirm(string text, string title = "Alert");
        public bool YesNoBox(string text, string title = "Ask");
        public string? Prompt(string title = "", string? okText = null, string? cancelText = null);
        public string? Password(string title = "", string? okText = null, string? cancelText = null);

        public string? SelectFolder();
        public string? SelectFileForSave(string filters = "");
        public string? SelectFileForLoad(string filters = "");
    }
}
