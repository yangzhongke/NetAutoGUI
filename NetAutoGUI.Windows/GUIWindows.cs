using System;
using System.Drawing;
using System.Windows.Forms;

namespace NetAutoGUI
{
    public static class GUIWindows
    {
        private static bool _isSetHighDpiModeInvoked;

        /// <summary>
        /// It must be called before Application.Run(new Form())
        /// </summary>
        public static void Initialize()
        {
            if (Application.MessageLoop)
            {
                throw new ApplicationException("GUIWindows.Initialize() must be called at the beginning of Main");
            }
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            _isSetHighDpiModeInvoked = true;
            BitmapData.LoadFromFileFunc = (string imageFile)=>
            {
                using var image = Image.FromFile(imageFile);
                using Bitmap bitmap = new Bitmap(image);
                return bitmap.ToBitmapData();
            };
        }

        internal static void CheckIsSetHighDpiModeInvoked()
        {
            if (!_isSetHighDpiModeInvoked)
            {
                throw new ApplicationException("GUIWindows.Initialize() must be called at the beginning of Main");
            }
        }
    }
}
