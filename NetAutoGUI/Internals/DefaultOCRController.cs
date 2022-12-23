using PaddleOCRSharp;
using System;

namespace NetAutoGUI.Internals
{
    public class DefaultOCRController : IOCRController
    {
        private PaddleOCREngine engine;
        public DefaultOCRController()
        {
            OCRModelConfig config = null;//default model(v3) for Chinese/English
            OCRParameter oCRParameter = new OCRParameter();
            this.engine = new PaddleOCREngine(config, oCRParameter);
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            this.engine.Dispose();
        }

        public OCRResult? DetectText(BitmapData bitmapData)
        {
            return engine.DetectText(bitmapData.Data);
        }
    }
}
