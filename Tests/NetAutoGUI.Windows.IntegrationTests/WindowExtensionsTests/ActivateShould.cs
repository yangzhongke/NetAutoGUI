using System.Diagnostics;
using FluentAssertions;
using Vanara.PInvoke;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.WindowExtensionsTests;

public class ActivateShould
{
    [Fact]
    public void Work_Correctly()
    {
        GUIWindows.Initialize();
        string solutionRoot = TestHelpers.GetSolutionRootDirectory();
        string pathOfWinFormsAppForTest1 = TestHelpers.FindFile(solutionRoot, "WinFormsAppForTest1.exe");
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1);
        try
        {
            Window? win = process.WaitForWindowByTitle("WinFormsAppForTest1");
            User32.ShowWindow(new HWND(new IntPtr(win.Id)), ShowWindowCommand.SW_MINIMIZE); //Hide the window first.
            TestHelpers.RecognizeText(GUI.Screenshot.Screenshot().Data).Should().NotContain("Zack666");
            win?.Activate();
            TestHelpers.RecognizeText(GUI.Screenshot.Screenshot().Data).Should().Contain("Zack666");
        }
        finally
        {
            process.Kill();
        }
    }
}