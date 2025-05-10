using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.WindowExtensionsTests;

public class GetMainMenuShould
{
    [Fact]
    public void ReturnMenu_WhenHavingMainMenu()
    {
        GUIWindows.Initialize();
        Process process = GUI.Application.LaunchApplication("notepad.exe");
        try
        {
            Window? win = process.WaitForWindowLikeTitle("*");
            ((object)win.GetMainMenu()).Should().NotBeNull();
        }
        finally
        {
            process.Kill();
        }
    }

    [Fact]
    public void ThrowException_WhenNoMainMenu()
    {
        GUIWindows.Initialize();
        string solutionRoot = TestHelpers.GetSolutionRootDirectory();
        string pathOfWinFormsAppForTest1 = TestHelpers.FindFile(solutionRoot, "WinFormsAppForTest1.exe");
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1);
        try
        {
            Window? win = process.WaitForWindowByTitle("WinFormsAppForTest1");
            Action action = () => win.GetMainMenu();
            action.Should().Throw<NotSupportedException>();
        }
        finally
        {
            process.Kill();
        }
    }
}