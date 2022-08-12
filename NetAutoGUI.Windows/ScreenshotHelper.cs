using System;
using System.Drawing.Imaging;
using System.IO;

namespace NetAutoGUI.Windows
{
    internal static class ScreenshotHelper
    {
        public static System.Drawing.Rectangle ToSysRect(NetAutoGUI.Rectangle rect)
        {
            return new(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static NetAutoGUI.Rectangle ToAutoRect(System.Drawing.Rectangle rect)
        {
            return new(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static ImageFormat ParseImageFormat(string filename)
        {
            string ext = Path.GetExtension(filename);
            if (string.IsNullOrEmpty(ext))
            {
                throw new ArgumentException("the filename doesnot have extension", nameof(filename));
            }
            ext = ext.ToLower();
            switch (ext)
            {
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".jpg":
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                default:
                    throw new ArgumentException("unsupported file type:" + ext, nameof(filename));
            }
        }
    }
}
