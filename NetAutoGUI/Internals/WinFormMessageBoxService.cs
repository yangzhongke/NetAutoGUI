using Vanara.PInvoke;

namespace NetAutoGUI.Internals
{
    internal class WinFormMessageBoxService : IMessageBoxService
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

        public bool YesNoBox(string text, string title = " ")
        {
            var result = User32.MessageBox(HWND.NULL, text, title, User32.MB_FLAGS.MB_ICONQUESTION | User32.MB_FLAGS.MB_YESNO);
            return result == User32.MB_RESULT.IDYES;
        }
    }
}
