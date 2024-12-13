using OpenCvSharp;

namespace NetAutoGUI;

public static class BitmapDataExtensions
{
    public static Mat ToMat(this BitmapData bitmapData, ImreadModes flags = ImreadModes.Unchanged)
    {
        return Cv2.ImDecode(bitmapData.Data, flags);
    }
}