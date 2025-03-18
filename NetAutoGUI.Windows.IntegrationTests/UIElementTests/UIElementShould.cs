using Xunit;
using FluentAssertions;


namespace NetAutoGUI.Windows.UnitTests.UIElementTests;

public class UIElementShould
{
    [Fact]
    public void Handle_MsPaint_Correctly()
    {
        GUIWindows.Initialize();
        GUI.Application.LaunchApplication(@".\AppsForTest\WinFormsAppForTest1.exe");
        Window? win = GUI.Application.WaitForWindowByTitle("WinFormsAppForTest1");
        UIElement? uiWindow = win?.GetRoot();
        uiWindow.ClassName.Should().Contain("WindowsForms");
        uiWindow.Text.Should().Be("WinFormsAppForTest1");
        GUI.Application.KillProcesses("WinFormsAppForTest1");
    }
}