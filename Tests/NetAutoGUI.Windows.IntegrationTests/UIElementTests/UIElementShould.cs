using Xunit;
using FluentAssertions;
using System.Diagnostics;
using PaddleOCRSharp;

namespace NetAutoGUI.Windows.UnitTests.UIElementTests;

public class UIElementShould
{
    [Fact]
    public void Handle_WinFormsAppForTest1_Correctly()
    {
        GUIWindows.Initialize();
        string solutionRoot = TestHelpers.GetSolutionRootDirectory();
        string pathOfWinFormsAppForTest1 = TestHelpers.FindFile(solutionRoot,"WinFormsAppForTest1.exe");
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1);
        try
        {
            Window? win = process.WaitForWindowByTitle("WinFormsAppForTest1");
            Win32UIElement? uiWindow = win?.GetRoot();
            uiWindow.ClassName.Should().Contain("WindowsForms");
            uiWindow.Text.Should().Be("WinFormsAppForTest1");
            uiWindow.Parent.Should().BeNull();
            uiWindow.Children.Should().Contain(c => c.Text.Equals("Name"));
            uiWindow.Children.Should().NotContain(c => c.Text.Equals("Email"));
            uiWindow.Descendents.Should().Contain(c => c.Text.Equals("Name"));
            uiWindow.Descendents.Should().Contain(c => c.Text.Equals("Email"));
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void WinFormsAppForTest1_Click_Calc_Correctly()
    {
        GUIWindows.Initialize();
        string solutionRoot = TestHelpers.GetSolutionRootDirectory();
        string pathOfWinFormsAppForTest1 = TestHelpers.FindFile(solutionRoot, "WinFormsAppForTest1.exe");
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1);
        try
        {
            Window? win = process.WaitForWindowByTitle("WinFormsAppForTest1");
            Win32UIElement? uiWindow = win?.GetRoot();
            var calcGroup = uiWindow.Descendents.Single(c => c.Text.Equals("Calc"));
            uiWindow.Rectangle.Contains(new Location(calcGroup.Rectangle.X, calcGroup.Rectangle.Y)).Should().BeTrue();
            var textBoxes = calcGroup.Children.Where(c => c.ClassName.Contains("Edit")).OrderBy(c => c.Rectangle.X)
                .ToArray();

            textBoxes.Should().HaveCount(3);
            var textNum1 = textBoxes[0];
            var textNum2 = textBoxes[1];
            var textNum3 = textBoxes[2];
            var btnAdd = calcGroup.Children.Single(c => c.ClassName.Contains("Button"));
            textNum1.Text = "1";
            textNum2.Text = "2";
            btnAdd.Click();
            textNum3.WaitForTextIsNotEmpty();
            textNum3.Text.Should().Be("3");
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void ToBitmap_Correctly()
    {
        GUIWindows.Initialize();
        string solutionRoot = TestHelpers.GetSolutionRootDirectory();
        string pathOfWinFormsAppForTest1 = TestHelpers.FindFile(solutionRoot, "WinFormsAppForTest1.exe");
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1);
        try
        {
            Window? win = process.WaitForWindowByTitle("WinFormsAppForTest1");
            Win32UIElement? uiWindow = win?.GetRoot();
            var labelName = uiWindow.Children.First(c => c.Text.Equals("Name"));
            var bitmapWindow = uiWindow.ToBitmap();
            var bitmapLabelName = labelName.ToBitmap();
            bitmapWindow.Height.Should().BeGreaterThan(bitmapLabelName.Height);
            bitmapWindow.Width.Should().BeGreaterThan(bitmapLabelName.Width);
            var ocr = new PaddleOCREngine();
            ocr.DetectText(bitmapLabelName.Data).Text.Should().Be("Name");
            var ocrResultOfWindow = ocr.DetectText(bitmapWindow.Data);
            ocrResultOfWindow.TextBlocks.Should().Contain(e => e.Text == "Name");
            ocrResultOfWindow.TextBlocks.Should().Contain(e => e.Text == "Contact");
            ocrResultOfWindow.TextBlocks.Should().Contain(e => e.Text == "Phone");
            ocrResultOfWindow.TextBlocks.Should().Contain(e => e.Text == "Email");
            ocrResultOfWindow.TextBlocks.Should().Contain(e => e.Text == "Calc");
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void Equals_Correctly()
    {
        GUIWindows.Initialize();
        string solutionRoot = TestHelpers.GetSolutionRootDirectory();
        string pathOfWinFormsAppForTest1 = TestHelpers.FindFile(solutionRoot, "WinFormsAppForTest1.exe");
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1);
        try
        {
            Window? win = process.WaitForWindowByTitle("WinFormsAppForTest1");
            Win32UIElement? uiWindow = win?.GetRoot();
            Win32UIElement labelName1 = uiWindow.Children.First(c => c.Text.Equals("Name"));
            Win32UIElement labelName2 = uiWindow.Children.First(c => c.Text.Equals("Name"));
            labelName1.Should().Be(labelName2);
            labelName1.Equals(labelName2).Should().BeTrue();
            labelName1.Equals((object)labelName2).Should().BeTrue();
            uiWindow.Should().NotBe(labelName1);
        }
        finally
        {
            process.Kill();
        }
    }
}