using System.Drawing;
using FluentAssertions;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.BitmapExtensionsTests;

public class ToBitmapDataShould
{
    [Fact]
    public void Execute_Correctly()
    {
        using Bitmap bitmap = new(30, 40);
        var actualBitmap = BitmapExtensions.ToBitmapData(bitmap);
        actualBitmap.Width.Should().Be(30);
        actualBitmap.Height.Should().Be(40);

        actualBitmap.Data.Length.Should().BeGreaterThan(1200);
        actualBitmap.Data.Take(2).Should()
            .BeEquivalentTo(new byte[] { 0x42, 0x4D });
    }
}