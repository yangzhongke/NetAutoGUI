using NetAutoGUI;

GUIWindows.Initialize();
GUI.Application.KillProcesses("Virtual.Display.Driver-v24.12.24-setup-x64");
GUI.Application.KillProcesses("Virtual.Display.Driver-v24.12.24-setup-x64.tmp");
GUI.Application.LaunchApplication("Virtual.Display.Driver-v24.12.24-setup-x64.exe");
var winStep1 =
    GUI.Application.WaitForWindowLikeTitle("Setup*Virtual Display Driver*", 20);
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
winStep1.WaitForText("Install").Click();