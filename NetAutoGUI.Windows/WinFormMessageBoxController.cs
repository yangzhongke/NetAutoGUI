using System.Runtime.Versioning;
using System.Windows.Forms;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
	[SupportedOSPlatform("windows")]
	internal class WinFormMessageBoxController : IMessageBoxController
	{
		public void Alert(string text, string title = " ")
		{
			User32.MessageBox(HWND.NULL, text, title, User32.MB_FLAGS.MB_ICONINFORMATION | User32.MB_FLAGS.MB_OK);
		}

		public bool Confirm(string text, string title = " ")
		{
			var result = User32.MessageBox(HWND.NULL, text, title, User32.MB_FLAGS.MB_ICONQUESTION | User32.MB_FLAGS.MB_OKCANCEL);
			return result == User32.MB_RESULT.IDOK;
		}

		public string? Password(string title = "", string? okText = null, string? cancelText = null)
		{
			return ShowInputForm(title, okText, cancelText, FormInputType.Password);
		}

		public string? Prompt(string title = "", string? okText = null, string? cancelText = null)
		{
			return ShowInputForm(title, okText, cancelText, FormInputType.Plain);
		}

		private static string? ShowInputForm(string title, string? okText, string? cancelText, FormInputType inputType)
		{
			using FormInput form = new FormInput();
			form.InputType = inputType;
			form.Text = title;
			if (!string.IsNullOrEmpty(okText))
			{
				form.OKText = okText;
			}
			if (!string.IsNullOrEmpty(cancelText))
			{
				form.CancelText = cancelText;
			}
			if (form.ShowDialog() == DialogResult.OK)
			{
				return form.Value;
			}
			else
			{
				return null;
			}
		}

		public bool YesNoBox(string text, string title = " ")
		{
			var result = User32.MessageBox(HWND.NULL, text, title, User32.MB_FLAGS.MB_ICONQUESTION | User32.MB_FLAGS.MB_YESNO);
			return result == User32.MB_RESULT.IDYES;
		}
	}
}
