using PaddleOCRSharp;

namespace NetAutoGUI.Internals
{
    public class DefaultOCRController : IOCRController
    {
        public OCRResult? DetectText(BitmapData bitmapData)
        {
            //使用默认中英文V3模型
            OCRModelConfig config = null;//default model(v3) for Chinese/English
            OCRParameter oCRParameter = new OCRParameter();
            using PaddleOCREngine engine = new PaddleOCREngine(config, oCRParameter);
            return engine.DetectText(bitmapData.Data);
        }
    }
}
