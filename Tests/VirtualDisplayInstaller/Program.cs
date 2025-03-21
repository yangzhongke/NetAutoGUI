using System.Text;
using NetAutoGUI;
using Vanara.PInvoke;
using WildcardMatch;

string GetClassName(long windowHandle)
{
    StringBuilder sb = new StringBuilder(100);
    User32.GetClassName(new HWND(new IntPtr(windowHandle)), sb, sb.Capacity);
    return sb.ToString();
}

GUIWindows.Initialize();
GUI.Application.KillProcesses("Virtual.Display.Driver-v24.12.24-setup-x64");
GUI.Application.KillProcesses("Virtual.Display.Driver-v24.12.24-setup-x64.tmp");
GUI.Application.LaunchApplication("Virtual.Display.Driver-v24.12.24-setup-x64.exe");
var winStep1 =
    GUI.Application.WaitForWindowLikeTitle("Setup*Virtual Display Driver*");
winStep1.Activate();
winStep1.WaitForText("Cancel").Click();
//winStep1.WaitForText("I accept the agreement").Click();
//winStep1.WaitForText("Next").Click();