using FluentAssertions;
using Vanara.PInvoke;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.DynamicMainMenuTests;

public class DynamicMainMenuShould
{
    [Fact]
    public void HandleNotePadExit_Correctly()
    {
        GUIWindows.Initialize();
        GUI.Application.KillProcesses("notepad");
        GUI.Application.LaunchApplication("notepad");
        GUI.Application.WaitForApplication("notepad");
        Window? win = GUI.Application.WaitForWindowLikeTitle("*Notepad*");
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        sut.File.Exit();
        Thread.Sleep(1000);
        GUI.Application.IsApplicationRunning("notepad").Should().BeFalse();
    }
}