using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace NetAutoGUI.Windows.UnitTests.Issues;

public class Issue21
{
    [Fact]
    public void Issue21ShouldBeFixed()
    {
        /*
        GUIWindows.Initialize();
        string solutionRoot = TestHelpers.GetSolutionRootDirectory();
        string pathOfWinFormsAppForTest1 = TestHelpers.FindFile(solutionRoot, "WpfAppForTest1.exe");
        Process process = GUI.Application.LaunchApplication(pathOfWinFormsAppForTest1);
        try
        {
            Window? win = process.WaitForWindowByTitle("MainWindow");
            Action actionGetRoot = () => win?.GetRoot();
            actionGetRoot.Should().Throw<NotSupportedException>();
            Action actionGetMenu = () => win?.GetMainMenu();
            actionGetMenu.Should().Throw<NotSupportedException>();
            win.Activate();
            var centerOfTxtResult = GUI.Screenshot.WaitOnScreen("Issues/Resources/txtResult.png").Center;
            GUI.Screenshot.ClickOnScreen("Issues/Resources/txtName.png");
            GUI.Keyboard.Write("Zack");
            GUI.Screenshot.ClickOnScreen("Issues/Resources/cmbCountry.png");
            GUI.Screenshot.ClickOnScreen("Issues/Resources/countryNZ.png");
            GUI.Screenshot.ClickOnScreen("Issues/Resources/cbIsVIP.png");
            GUI.Screenshot.ClickOnScreen("Issues/Resources/btnSubmit.png");
            GUI.Mouse.Click(centerOfTxtResult.X, centerOfTxtResult.Y);
            GUI.Keyboard.Ctrl_A();
            GUI.Keyboard.Ctrl_C();
            string txtResult = ClipboardHelpers.GetText();
            txtResult.Should().Contain("VIP");
            txtResult.Should().Contain("NZ");
            txtResult.Should().Contain("Zack");
        }
        finally
        {
            process.Kill();
        }*/
    }
}