using NetAutoGUI;

GUIWindows.Initialize();
GUI.Application.KillProcesses("Virtual.Display.Driver-v24.12.24-setup-x64");
GUI.Application.KillProcesses("Virtual.Display.Driver-v24.12.24-setup-x64.tmp");
var process = GUI.Application.LaunchApplication("Virtual.Display.Driver-v24.12.24-setup-x64.exe");
var winStep1 =
    GUI.Application.WaitForWindowLikeTitle("Setup*Virtual Display Driver*", 60);
winStep1.Activate();
winStep1.WaitForText("I accept the agreement").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("I accept the agreement").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("I accept the agreement").Click();
winStep1.WaitForText("Next").Click();
return;
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("Next").Click();
winStep1.WaitForText("Install").Click();
var confirmDialog =
    GUI.Application.WaitForWindowLikeTitle("*Windows*Security*", 180);
confirmDialog.WaitForText("Install").Click();
winStep1.WaitForText("Finish", 180).Click();