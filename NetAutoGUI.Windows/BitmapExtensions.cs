using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace NetAutoGUI;

public static class BitmapExtensions
{
    /// <summary>
    /// Convert a Bitmp into BitmapData
    /// </summary>
    /// <param name="bitmap"></param>
    /// <returns></returns>
    public static BitmapData ToBitmapData(this Bitmap bitmap)
    {
        using MemoryStream memSteam = new MemoryStream();
        bitmap.Save(memSteam, ImageFormat.Bmp);
        memSteam.Position = 0;
        byte[] data = memSteam.ToArray();
        return new BitmapData(data, bitmap.Width, bitmap.Height);
    }
}