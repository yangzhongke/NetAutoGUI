using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.WindowExtensionsTests;

public class MaximizeShould
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
            var originalBoundary = win.Boundary;
            win.Maximize();
            GUI.Pause(1);
            var newBoundary = win.Boundary;
            newBoundary.Area.Should().BeGreaterThan(originalBoundary.Area);

            newBoundary.X.Should().BeLessThanOrEqualTo(0);
            newBoundary.Y.Should().BeLessThanOrEqualTo(0);
            newBoundary.Width.Should().BeGreaterThan(originalBoundary.Width);
            newBoundary.Height.Should().BeGreaterThan(originalBoundary.Height);
        }
        finally
        {
            process.Kill();
        }
    }
}