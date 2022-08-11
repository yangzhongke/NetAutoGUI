namespace NetAutoGUI
{
    public interface IMessageBoxService
    {
        public void Alert(string text, string title = "Alert");
        public bool Confirm(string text, string title = "Alert");
        public bool YesNoBox(string text, string title = " ");
    }
}
