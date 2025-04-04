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
        Window? win = process.WaitForWindowByTitle("WinFormsAppForTest1");
        Win32UIElement? uiWindow = win?.GetRoot();
        uiWindow.ClassName.Should().Contain("WindowsForms");
        uiWindow.Text.Should().Be("WinFormsAppForTest1");
        uiWindow.Parent.Should().BeNull();
        uiWindow.Children.Should().Contain(c => c.Text.Equals("Name"));
        uiWindow.Children.Should().NotContain(c => c.Text.Equals("Email"));
        uiWindow.Descendents.Should().Contain(c => c.Text.Equals("Name"));
        uiWindow.Descendents.Should().Contain(c => c.Text.Equals("Email"));
        process.Kill();
    }
}