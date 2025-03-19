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
        output.WriteLine("All Screens:");
        foreach (var screen in Screen.AllScreens)
        {
            output.WriteLine($"DeviceName: {screen.DeviceName}, Bounds: {screen.Bounds}");
        }
        Thread.Sleep(1000);
        Window? win = GUI.Application.WaitForWindowByTitle("WinFormsAppForTest1");
        UIElement? uiWindow = win?.GetRoot();
        uiWindow.ClassName.Should().Contain("WindowsForms");
        uiWindow.Text.Should().Be("WinFormsAppForTest1");
        uiWindow.Parent.Should().BeNull();
        GUI.Application.KillProcesses("WinFormsAppForTest1");
    }
}