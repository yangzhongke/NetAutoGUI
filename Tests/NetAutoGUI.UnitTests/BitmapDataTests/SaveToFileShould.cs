using FileSignatures;
using System.Drawing;
using NetAutoGUI.Windows;

namespace NetAutoGUI.UnitTests.BitmapDataTests
{
    public class SaveToFileShould
    {
        [InlineData("1.webp", "webp")]
        [InlineData("1.png", "png")]
        [InlineData("1.jpg", "jpg")]
        [InlineData("1.jpeg", "jpg")]
        [Theory]
        public void InferInferImageType_Correctly(string fileName, string expectedFileExtension)
        {
            //Arrange
            string bmpFilePath = Path.Combine("BitmapDataTests", "1.bmp");
            var bitmap = (Bitmap)Bitmap.FromFile(bmpFilePath);
            string filePath = Path.Combine( Path.GetTempPath(), fileName);
            //Act
            BitmapData sut =bitmap.ToBitmapData();
            Action action =()=> sut.Save(filePath);
            //Assert
            action.Should().NotThrow();
            using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var format = new FileFormatInspector().DetermineFileFormat(fileStream);
            format.Extension.Should().Be(expectedFileExtension);
        }
    }
}