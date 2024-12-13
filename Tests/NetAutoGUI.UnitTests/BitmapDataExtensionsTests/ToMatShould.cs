using OpenCvSharp;

namespace NetAutoGUI.UnitTests.BitmapDataExtensionsTests;

public class ToMatShould
{
    [Fact]
    public void Run_Correctly()
    {
        //Arrange
        string bmpFilePath = Path.Combine("BitmapDataExtensionsTests", "1.bmp");
        BitmapData sut = TestHelpers.LoadBitmapDataFromBmpFile(bmpFilePath);
        //Act
        Mat mat =sut.ToMat();
        mat.Should().NotBeNull();
        mat.Width.Should().Be(sut.Width);
        mat.Height.Should().Be(sut.Height);
    }
}