using System;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Windows.Forms;
using Vanara.PInvoke;

namespace NetAutoGUI.Windows
{
    [SupportedOSPlatform("windows")]
    internal class WinFormDialogController : IDialogController
    {
        public long? Parent { get; set; }

        private IntPtr GetParentWindowHandle()
        {
            if (Parent!=null)
            {
                return new IntPtr(Parent.Value);
            }
            else
            {
                HWND foregroundWindow = User32.GetForegroundWindow();
                if (foregroundWindow != HWND.NULL)
                {
                    User32.GetWindowThreadProcessId(foregroundWindow, out uint processId);
                    uint currentProcessId = (uint)Process.GetCurrentProcess().Id;
                    if (processId == currentProcessId)
                    {
                        return new IntPtr(foregroundWindow.ToInt64());
                    }
                    else
                    {
                        return Process.GetCurrentProcess().MainWindowHandle;
                    }
                }
                else
                {
                    return Process.GetCurrentProcess().MainWindowHandle;
                }
            }
        }

        private IWin32Window GetParentWindow()
        {
            return NativeWindow.FromHandle(GetParentWindowHandle());
        }


        public void Alert(string text, string title = " ")
        {
            User32.MessageBox(GetParentWindowHandle(), text, title, User32.MB_FLAGS.MB_ICONINFORMATION | User32.MB_FLAGS.MB_OK);
        }

        public bool Confirm(string text, string title = " ")
        {
            var result = User32.MessageBox(GetParentWindowHandle(), text, title, User32.MB_FLAGS.MB_ICONQUESTION | User32.MB_FLAGS.MB_OKCANCEL);
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

        private string? ShowInputForm(string title, string? okText, string? cancelText, FormInputType inputType)
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
            if (form.ShowDialog(GetParentWindow()) == DialogResult.OK)
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
            var result = User32.MessageBox(GetParentWindowHandle(), text, title, User32.MB_FLAGS.MB_ICONQUESTION | User32.MB_FLAGS.MB_YESNO);
            return result == User32.MB_RESULT.IDYES;
        }

        public string? SelectFolder()
        {
            using FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(GetParentWindow()) == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            else
            {
                return null;
            }
        }

        public string? SelectFileForSave(string filters = "")
        {
            using SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = filters;
            if (dialog.ShowDialog(GetParentWindow()) == DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {
                return null;
            }
        }

        public string? SelectFileForLoad(string filters = "")
        {
            using OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filters;
            if (dialog.ShowDialog(GetParentWindow()) == DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {
                return null;
            }
        }
    }
}
