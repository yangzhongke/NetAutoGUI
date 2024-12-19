using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace NetAutoGUI;

public static class BitmapExtensions
{
    public static BitmapData ToBitmapData(this Bitmap bitmap)
    {
        using MemoryStream memSteam = new MemoryStream();
        bitmap.Save(memSteam, ImageFormat.Bmp);
        memSteam.Position = 0;
        byte[] data = memSteam.ToArray();
        return new BitmapData(data, bitmap.Width, bitmap.Height);
    }
}