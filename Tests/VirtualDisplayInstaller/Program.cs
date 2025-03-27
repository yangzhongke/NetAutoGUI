using NetAutoGUI;

GUIWindows.Initialize();
GUI.Application.KillProcesses("Virtual.Display.Driver-v24.12.24-setup-x64");
GUI.Application.KillProcesses("Virtual.Display.Driver-v24.12.24-setup-x64.tmp");
GUI.Application.LaunchApplication("Virtual.Display.Driver-v24.12.24-setup-x64.exe");
var winStep1 =
    GUI.Application.WaitForWindowLikeTitle("Setup*Virtual Display Driver*", 60);
winStep1.Activate();
winStep1.WaitForText("I accept the agreement").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("I accept the agreement").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("I accept the agreement").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("Install").MouseClick();
var confirmDialog =
    GUI.Application.WaitForWindowLikeTitle("*Windows*Security*", 180);
confirmDialog.WaitForText("Install").MouseClick();
winStep1.WaitForText("Finish", 180).Click();