using OpenCvSharp;

namespace NetAutoGUI
{
    public record BitmapData(byte[] Data,int Width,int Height)
    {
        public Mat ToMat()
        {
            return Cv2.ImDecode(Data,ImreadModes.Unchanged);
        }
    }
}
