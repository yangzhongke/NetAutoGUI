using PaddleOCRSharp;

namespace NetAutoGUI
{
    public interface IOCRController
    {
        public OCRResult? DetectText(BitmapData bitmapData);
    }
}
