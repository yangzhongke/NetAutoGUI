using NetAutoGUI;

GUIWindows.Initialize();
var window = GUI.Application.FindWindowLikeTitle("*百度*");
if (window == null)
{
    Console.WriteLine("No Baidu Window Found!");
    return;
}
window.Activate();
window.WaitAndClick(BitmapData.FromFile("Baidu.png"));