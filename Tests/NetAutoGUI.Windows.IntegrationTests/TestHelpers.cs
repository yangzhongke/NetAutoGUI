using IronOcr;
namespace NetAutoGUI.Windows.UnitTests
{
    internal class TestHelpers
    {
        public static string GetSolutionRootDirectory()
        {
            string currentDir = Directory.GetCurrentDirectory();

            while (!string.IsNullOrEmpty(currentDir))
            {
                if (Directory.GetFiles(currentDir, "*.sln").Length > 0||
                    Directory.GetFiles(currentDir, "*.slnx").Length > 0)
                {
                    return currentDir;
                }
                currentDir = Directory.GetParent(currentDir)?.FullName;
            }

            throw new Exception("Solution root not found.");
        }

        public static string FindFile(string root, string fileName)
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(root, fileName, SearchOption.AllDirectories))
                {
                    return file; // Return first occurrence
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }

            throw new Exception($"{fileName} not found.");
        }

        public static string RecognizeText(byte[] imageBytes)
        {
            //30 days trial license key for IronOCR only for testing purposes.
            License.LicenseKey =
                "IRONSUITE.YANGZHONGKE8.GMAIL.COM.22106-90FE0AAA3C-OD3XZ-PVWAV2FNGYIZ-YLPG6NXOE4GJ-P7DROHHD4LHA-N3C7WKRH3RXX-4OPJZH7DSLIS-HZMNQHLROTGX-4IXOUV-TKMJDR7O4VWPUA-DEPLOYMENT.TRIAL-ALR7LC.TRIAL.EXPIRES.20.JUL.2025";

            var ocr = new IronTesseract();
            using var ocrInput = new OcrInput();
            ocrInput.LoadImage(imageBytes);
            var ocrResult = ocr.Read(ocrInput);
            return ocrResult.Text;

            //todo: change to Azure OCR, and put credentials in environment variables to avoid hardcoding
        }
    }
}
