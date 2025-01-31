using OpenCvSharp;

namespace NetAutoGUI;

public static class BitmapDataExtensions
{
    /// <summary>
    /// Convert the BitmapData into a Mat of OpenCVSharp
    /// </summary>
    /// <param name="bitmapData">the bitmap data</param>
    /// <param name="flags">the flags</param>
    /// <returns>the converted Mat(It must be disposed after used)</returns>
    public static Mat ToMat(this BitmapData bitmapData, ImreadModes flags = ImreadModes.Unchanged)
    {
        return Cv2.ImDecode(bitmapData.Data, flags);
    }
}