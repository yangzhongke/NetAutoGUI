using System;
using System.Drawing;
using System.Windows.Forms;

namespace NetAutoGUI
{
    public static class GUIWindows
    {
        public static bool IsSetHighDpiModeInvoked{ get; private set; }
        public static void Initialize()
        {
            if (Application.MessageLoop)
            {
                throw new ApplicationException("GUIWindows.Initialize() must be called before Application.Run(new Form())");
            }
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            IsSetHighDpiModeInvoked = true;
            BitmapData.LoadFromFileFunc = (string imageFile)=>
            {
                using var image = Image.FromFile(imageFile);
                using Bitmap bitmap = new Bitmap(image);
                return bitmap.ToBitmapData();
            };
        }

        public static void CheckIsSetHighDpiModeInvoked()
        {
            if (!IsSetHighDpiModeInvoked)
            {
                throw new ApplicationException("GUIWindows.Initialize() must be called before Application.Run(new Form())");
            }
        }
    }
}
