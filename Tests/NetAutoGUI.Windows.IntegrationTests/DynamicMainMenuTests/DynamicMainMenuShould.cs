using FluentAssertions;
using NetAutoGUI.Windows.UnitTests.TestUtils;
using Vanara.PInvoke;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.DynamicMainMenuTests;

public class DynamicMainMenuShould
{
    [EnOnlyFact]
    public void HandleNotepadExit_ImplicitlyClick_Correctly_En()
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

    [ZhsOnlyFact]
    public void HandleNotepadExit_ImplicitlyClick_Correctly_Zhs()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*记事本*");
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        sut.文件.退出();
        Thread.Sleep(1000);
        GUI.Application.IsApplicationRunning("notepad").Should().BeFalse();
    }

    [EnOnlyFact]
    public void HandleNotepadExit_ExplicitlyClick_Correctly_En()
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

    [ZhsOnlyFact]
    public void HandleNotepadExit_ExplicitlyClick_Correctly_Zhs()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*记事本*");
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        sut.文件.退出.Click();
        Thread.Sleep(1000);
        GUI.Application.IsApplicationRunning("notepad").Should().BeFalse();
    }

    [EnOnlyFact]
    public void Blocked_WhenClickAndWait_En()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*Notepad*");
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
        process.Kill();
    }

    [ZhsOnlyFact]
    public void Blocked_WhenClickAndWait_Zhs()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*记事本*");
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        bool clickAndWait_Returned = false;
        Thread thread = new Thread(() =>
        {
            clickAndWait_Returned.Should().BeFalse();
            var aboutWin = process.WaitForWindowLikeTitle("关于*记事本*");
            aboutWin.Close();
            Thread.Sleep(200);
            clickAndWait_Returned.Should().BeTrue();
        });
        thread.Start();
        sut.帮助.关于记事本.ClickAndWait();
        clickAndWait_Returned = true;
        thread.Join();
        process.Kill();
    }

    [EnOnlyFact]
    public void Unblocked_WhenClickExplicitly_En()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*Notepad*");
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
        process.Kill();
    }

    [ZhsOnlyFact]
    public void Unblocked_WhenClickExplicitly_Zhs()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*记事本*");
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        bool clickAndWait_Returned = false;
        Thread thread = new Thread(() =>
        {
            Thread.Sleep(500);
            clickAndWait_Returned.Should().BeTrue();
        });
        thread.Start();
        sut.帮助.关于记事本.Click();
        clickAndWait_Returned = true;
        thread.Join();
        process.Kill();
    }

    [EnOnlyFact]
    public void Unblocked_WhenClickImplicitly_En()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*Notepad*");
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
        process.Kill();
    }

    [ZhsOnlyFact]
    public void Unblocked_WhenClickImplicitly_Zhs()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*记事本*");
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        bool clickAndWait_Returned = false;
        Thread thread = new Thread(() =>
        {
            Thread.Sleep(500);
            clickAndWait_Returned.Should().BeTrue();
        });
        thread.Start();
        sut.帮助.关于记事本();
        clickAndWait_Returned = true;
        thread.Join();
        process.Kill();
    }

    [EnOnlyFact]
    public void SubMenuItem_ShouldWorkWell_En()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*Notepad*");
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        Action action = () => sut.View.Zoom.ZoomOut();
        action.Should().NotThrow();
        process.Kill();
    }

    [ZhsOnlyFact]
    public void SubMenuItem_ShouldWorkWell_Zhs()
    {
        GUIWindows.Initialize();
        var process = GUI.Application.LaunchApplication("notepad");
        Window? win = process.WaitForWindowLikeTitle("*记事本*");
        win.Should().NotBeNull();
        dynamic sut = new DynamicMainMenu(new HWND((IntPtr)win.Id));
        Action action = () => sut.查看.缩放.缩小();
        action.Should().NotThrow();
        process.Kill();
    }
}