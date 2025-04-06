using Xunit;
using FluentAssertions;
using System.Diagnostics;
using Xunit.Abstractions;
using System.Windows.Forms;


namespace NetAutoGUI.Windows.UnitTests.UIElementTests;

public class UIElementShould
{
    private readonly ITestOutputHelper output;

    public UIElementShould(ITestOutputHelper output)
    {
        this.output = output;
    }
    
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
    public void WinFormsAppForTest1_Calc_Correctly()
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
            textNum1.Focus();
            ClipboardHelpers.SetText("1");
            textNum1.Paste();

            textNum2.Focus();
            ClipboardHelpers.SetText("2");
            textNum2.Paste();

            btnAdd.Click();
            Thread.Sleep(200);

            textNum3.Focus();
            textNum3.SelectAll();
            textNum3.Copy();
            Thread.Sleep(200);
            //Clipboard.GetText().Should().Be("3");
            textNum3.Text.Should().Be("3");
        }
        finally
        {
            //process.Kill();
        }
    }
}