using OpenCvSharp;

namespace NetAutoGUI
{
    public class BitmapData
    {
        private byte[] data;

        public BitmapData(byte[] data)
        {
            this.data = data;
        }
        public byte[] Data
        {
            get { return data; }
        }

        internal Mat ToMat()
        {
            return Cv2.ImDecode(data,ImreadModes.Unchanged);
        }
    }
}
