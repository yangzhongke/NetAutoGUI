using System;
using System.Threading;
using System.Windows.Forms;

namespace NetAutoGUI.Windows
{
    public static class ClipboardHelpers
    {
        private static TResult RunInSTAThread<TResult>(Func<TResult> func)
        {
            //The Clipboard class can only be used in threads set to single thread apartment (STA) mode. To use this class, ensure that your Main method is marked with the STAThreadAttribute attribute.
            //https://blog.csdn.net/wangyue4/article/details/39431725
            TResult? result=default;
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                Thread thread = new Thread(() => {
                    result=func();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
            else
            {
                result = func();
            }
            return result;
        }

        public static IDataObject GetClipboardData()
        {
            return RunInSTAThread(() => {
                return Clipboard.GetDataObject();
            });
        }

        public static string GetClipboardText()
        {
            return RunInSTAThread(() => {
                return Clipboard.GetText();
            });
        }
    }
}
