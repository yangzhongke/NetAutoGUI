using FileSignatures;

namespace NetAutoGUI.UnitTests.BitmapDataTests
{
    public class SaveToFileShould
    {
        [InlineData("1.webp", "webp")]
        [InlineData("1.png", "png")]
        [InlineData("1.jpg", "jpg")]
        [InlineData("1.jpeg", "jpg")]
        [InlineData("1.WEBP", "webp")]
        [InlineData("1.PNG", "png")]
        [InlineData("1.JPG", "jpg")]
        [InlineData("1.JPEG", "jpg")]
        [InlineData("1.Jpeg", "jpg")]
        [Theory]
        public void InferInferImageType_Correctly(string fileName, string expectedFileExtension)
        {
            //Arrange
            string bmpFilePath = Path.Combine("BitmapDataTests", "1.bmp");
            BitmapData sut = TestHelpers.LoadBitmapDataFromBmpFile(bmpFilePath);
            string filePath = Path.Combine( Path.GetTempPath(), fileName);
            //Act
            Action action =()=> sut.Save(filePath);
            //Assert
            action.Should().NotThrow();
            using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var format = new FileFormatInspector().DetermineFileFormat(fileStream);
            format.Extension.Should().Be(expectedFileExtension);
        }

        [InlineData("1.gif")]
        [InlineData("1.bmp")]
        [Theory]
        public void ThrowException_IfFileTypeNotSupported(string fileName)
        {
            //Arrange
            string bmpFilePath = Path.Combine("BitmapDataTests", "1.bmp");
            BitmapData sut = TestHelpers.LoadBitmapDataFromBmpFile(bmpFilePath);
            string filePath = Path.Combine( Path.GetTempPath(), fileName);
            //Act
            Action action =()=> sut.Save(filePath);
            //Assert
            action.Should().Throw<Exception>();
        }

        [Fact]
        public void CreateDirectoryIfNotExists()
        {
            //Arrange
            string bmpFilePath = Path.Combine("BitmapDataTests", "1.bmp");
            BitmapData sut = TestHelpers.LoadBitmapDataFromBmpFile(bmpFilePath);
            string filePath = Path.Combine( Path.GetTempPath(),Guid.NewGuid().ToString(), "1.jpg");
            //Act
            Action action =()=> sut.Save(filePath);
            //Assert
            action.Should().NotThrow();
            FileInfo fileInfo = new FileInfo(filePath);
            fileInfo.Exists.Should().BeTrue();
            fileInfo.Delete();
        }
    }
}