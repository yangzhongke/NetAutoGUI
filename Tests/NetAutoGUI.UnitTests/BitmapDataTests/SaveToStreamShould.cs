using FileSignatures;
using SkiaSharp;

namespace NetAutoGUI.UnitTests.BitmapDataTests
{
    public class SaveToStreamShould
    {
        [InlineData(ImageType.WebP, "webp")]
        [InlineData(ImageType.Png, "png")]
        [InlineData(ImageType.Jpg, "jpg")]
        [Theory]
        public void Save_Correctly(ImageType imgType, string expectedFileExtension)
        {
            //Arrange
            string bmpFilePath = Path.Combine("BitmapDataTests", "1.bmp");
            BitmapData sut = TestHelpers.LoadBitmapDataFromBmpFile(bmpFilePath);
            //Act
            using MemoryStream memoryStream = new MemoryStream();
            Action action =()=> sut.Save(memoryStream, imgType);
            memoryStream.Position = 0;
            //Assert
            action.Should().NotThrow();
            var format = new FileFormatInspector().DetermineFileFormat(memoryStream);
            format.Extension.Should().Be(expectedFileExtension);
            memoryStream.Position = 0;
            using SKBitmap skBitmap = SKBitmap.Decode(memoryStream);            
            skBitmap.Width.Should().Be(sut.Width);
            skBitmap.Height.Should().Be(sut.Height);
        }
    }
}