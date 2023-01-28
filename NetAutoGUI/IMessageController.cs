namespace NetAutoGUI
{
	public interface IMessageBoxController
	{
		public void Alert(string text, string title = "Alert");
		public bool Confirm(string text, string title = "Alert");
		public bool YesNoBox(string text, string title = "Ask");
		public string? Prompt(string title = "", string? okText = null, string? cancelText = null);
		public string? Password(string title = "", string? okText = null, string? cancelText = null);
	}
}
