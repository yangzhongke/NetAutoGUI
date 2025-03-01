using System.Drawing;
using System.Windows.Forms;
using FluentAssertions;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.ClipboardHelpersTests;

public class GetClipboardDataShould
{
    [Fact]
    public void GetText_ShouldReturnEmpty_WhenClipboardIsEmpty()
    {
        STAHelper.RunInSTAThread(Clipboard.Clear);
        var actual = ClipboardHelpers.GetText();
        actual.Should().BeNullOrEmpty();
    }

    [Fact]
    public void GetText_ShouldReturnEmpty_WhenClipboardIsImage()
    {
        STAHelper.RunInSTAThread(() => { Clipboard.SetImage(new Bitmap(100, 100)); });
        var actual = ClipboardHelpers.GetText();
        actual.Should().BeNullOrEmpty();
        STAHelper.RunInSTAThread(Clipboard.Clear);
    }

    [Fact]
    public void GetText_ShouldReturn_WhenClipboardIsText()
    {
        STAHelper.RunInSTAThread(() => { Clipboard.SetText("abc"); });
        var actual = ClipboardHelpers.GetText();
        actual.Should().Be("abc");
        STAHelper.RunInSTAThread(Clipboard.Clear);
    }

    [Fact]
    public void GetImage_ShouldReturnEmpty_WhenClipboardIsEmpty()
    {
        STAHelper.RunInSTAThread(Clipboard.Clear);
        var actual = ClipboardHelpers.GetImage();
        actual.Should().BeNull();
    }

    [Fact]
    public void GetImage_ShouldReturnEmpty_WhenClipboardIsText()
    {
        STAHelper.RunInSTAThread(() => { Clipboard.SetText("abc"); });
        var actual = ClipboardHelpers.GetImage();
        actual.Should().BeNull();
        STAHelper.RunInSTAThread(Clipboard.Clear);
    }

    [Fact]
    public void GetImage_ShouldReturn_WhenClipboardIsImage()
    {
        STAHelper.RunInSTAThread(() => { Clipboard.SetImage(new Bitmap(30, 40)); });
        var actual = ClipboardHelpers.GetImage();
        actual.Should().NotBeNull();
        actual.Width.Should().Be(30);
        actual.Height.Should().Be(40);
        STAHelper.RunInSTAThread(Clipboard.Clear);
    }
}