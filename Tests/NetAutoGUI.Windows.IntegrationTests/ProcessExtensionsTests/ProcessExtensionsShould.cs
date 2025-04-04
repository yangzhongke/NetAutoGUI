using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.ProcessExtensionsTests;

public class ProcessExtensionsShould
{
    [Fact]
    public void GetAllWindows_Correctly()
    {
        string solutionRoot = TestHelpers.GetSolutionRootDirectory();
        string pathOfWinFormsAppForTest1 = TestHelpers.FindFile(solutionRoot, "WinFormsAppForTest1.exe");
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1, "multi-windows");
        try
        {
            SpinWait.SpinUntil(() =>
            {
                var windows = process.GetAllWindows();
                return windows.Count() == 3;
            }, TimeSpan.FromSeconds(5)).Should().BeTrue();
        }
        finally
        {
            process.Kill();
        }
    }
}