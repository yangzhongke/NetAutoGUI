using FluentAssertions;
using Vanara.PInvoke;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.DynamicMainMenuTests;

public class DynamicMainMenuShould
{
    [Fact]
    public void HandleNotepadExit_ImplicitlyClick_Correctly()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*Notepad*");
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        sut.File.Exit();
        Thread.Sleep(1000);
        GUI.Application.IsApplicationRunning("notepad").Should().BeFalse();
    }

    [Fact]
    public void HandleNotepadExit_ExplicitlyClick_Correctly()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*Notepad*");
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        sut.File.Exit.Click();
        Thread.Sleep(1000);
        GUI.Application.IsApplicationRunning("notepad").Should().BeFalse();
    }

    [Fact]
    public void Blocked_WhenClickAndWait()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*Notepad*");
        win.Activate();
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        bool clickAndWait_Returned = false;
        Thread thread = new Thread(() =>
        {
            clickAndWait_Returned.Should().BeFalse();
            var aboutWin = process.WaitForWindowByTitle("About Notepad");
            aboutWin.Close();
            Thread.Sleep(200);
            clickAndWait_Returned.Should().BeTrue();
        });
        thread.Start();
        sut.Help.AboutNotepad.ClickAndWait();
        clickAndWait_Returned = true;
        thread.Join();
        GUI.Application.KillProcesses("notepad");
    }

    [Fact]
    public void Unblocked_WhenClickExplicitly()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*Notepad*");
        win.Activate();
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        bool clickAndWait_Returned = false;
        Thread thread = new Thread(() =>
        {
            Thread.Sleep(500);
            clickAndWait_Returned.Should().BeTrue();
        });
        thread.Start();
        sut.Help.AboutNotepad.Click();
        clickAndWait_Returned = true;
        thread.Join();
        GUI.Application.KillProcesses("notepad");
    }

    [Fact]
    public void Unblocked_WhenClickImplicitly()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*Notepad*");
        win.Activate();
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        bool clickAndWait_Returned = false;
        Thread thread = new Thread(() =>
        {
            Thread.Sleep(500);
            clickAndWait_Returned.Should().BeTrue();
        });
        thread.Start();
        sut.Help.AboutNotepad();
        clickAndWait_Returned = true;
        thread.Join();
        GUI.Application.KillProcesses("notepad");
    }
}