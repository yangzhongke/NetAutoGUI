using NetAutoGUI;

GUIWindows.Initialize();
var window = GUI.Application.FindWindowLikeTitle("*Notepad*");
if (window == null)
{
    Console.WriteLine("No Baidu Window Found!");
    return;
}
window.Activate();
window.WaitAndClick(BitmapData.FromFile("Baidu.png"));