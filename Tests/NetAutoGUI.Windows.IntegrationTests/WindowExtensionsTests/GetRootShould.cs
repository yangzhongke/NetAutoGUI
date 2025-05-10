using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.WindowExtensionsTests;

public class GetRootShould
{
    [Fact]
    public void ThrowException_WhenRunChrome()
    {
        GUIWindows.Initialize();
        Process process = GUI.Application.LaunchApplication("Chrome");
        try
        {
            Window? win = process.WaitForWindowLikeTitle("*");
            Action action = () => win.GetRoot();
            action.Should().Throw<NotSupportedException>();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void ThrowException_WhenRunWPFApp()
    {
        GUIWindows.Initialize();
        string solutionRoot = TestHelpers.GetSolutionRootDirectory();
        string pathOfWinFormsAppForTest1 = TestHelpers.FindFile(solutionRoot, "WpfAppForTest1.exe");
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1);
        try
        {
            Window? win = process.WaitForWindowByTitle("MainWindow");
            Action action = () => win.GetRoot();
            action.Should().Throw<NotSupportedException>();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void ThrowException_WhenRunWinFormApp()
    {
        GUIWindows.Initialize();
        string solutionRoot = TestHelpers.GetSolutionRootDirectory();
        string pathOfWinFormsAppForTest1 = TestHelpers.FindFile(solutionRoot, "WinFormsAppForTest1.exe");
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1);
        try
        {
            Window? win = process.WaitForWindowByTitle("WinFormsAppForTest1");
            win.GetRoot().Should().NotBeNull();
        }
        finally
        {
            process.Kill();
        }
    }
}