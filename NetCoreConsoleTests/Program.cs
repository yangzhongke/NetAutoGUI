using NetAutoGUI;

//GUI.Mouse.MoveTo(1000, 1000, 3, TweeningType.BounceInOut);
GUI.Mouse.MoveTo(1000, 800);
/*
for(int i=0;i<1;i++)
{
    GUI.Mouse.Scroll(-200);
    Thread.Sleep(1000);
    GUI.Mouse.Scroll(200);
    Thread.Sleep(1000);
    GUI.Mouse.Click();
    GUI.Keyboard.Write("杨中科");
}*/
/*
GUI.Mouse.Click();
GUI.Keyboard.HotKey(KeyBoardKey.CONTROL,KeyBoardKey.F1);
GUI.Keyboard.Press(KeyBoardKey.VK_A);
using (GUI.Keyboard.Hold(KeyBoardKey.SHIFT))
{
    GUI.Keyboard.Press(KeyBoardKey.VK_A);
    GUI.Keyboard.Press(KeyBoardKey.VK_A);
    Thread.Sleep(1000);
}
GUI.Keyboard.Press(KeyBoardKey.VK_A);*/
/*
GUI.MessageBox.Alert("Alert");
GUI.MessageBox.Alert(GUI.MessageBox.Confirm("真的吗？").ToString());
GUI.MessageBox.Alert(GUI.MessageBox.YesNoBox("你是男的吗？",title:"真的吗？").ToString());*/
/*
string? s= GUI.MessageBox.Prompt("请输入您的姓名");
if (s != null)
{
    var pwd = GUI.MessageBox.Password($"请输入{s}的密码", "欧了", "走开");
    GUI.MessageBox.Alert(pwd);
}*/
/*
GUI.Screenshot.Screenshot("d:/1.jpg"); 
GUI.Screenshot.Screenshot("d:/1.bmp");
GUI.Screenshot.Screenshot("d:/1.jpeg",region:new Rectangle(10,10,250,250));
GUI.Screenshot.Screenshot("d:/1.png");
var d= GUI.Screenshot.Screenshot();
Console.WriteLine(d);*/
/*
var loc = GUI.Screenshot.LocateOnScreen("1.png").Center;
GUI.Mouse.Click(loc.X, loc.Y);
loc = GUI.Screenshot.LocateOnScreen("2.png").Center;
GUI.Mouse.Click(loc.X, loc.Y);*/
/*
var loc = GUI.Screenshot.LocateOnScreen("1.png").Center;
GUI.Mouse.Click(loc.X, loc.Y);*/
/*
var loc = GUI.Screenshot.LocateCenterOnScreen("1.png",0.5);
GUI.Mouse.Click(loc.X,loc.Y);
(int x, int y) = GUI.Screenshot.LocateCenterOnScreen("2.png");
GUI.Mouse.Click(x, y);*/
/*
var items = GUI.Screenshot.LocateAllCentersOnScreen("4.png",0.99);
foreach (var item in items)
{
    Console.WriteLine(item);
}*/
/*
Thread.Sleep(1000);
GUI.Application.ActivateWindowByTitle(t=>t.Contains("info.txt"));*/


GUI.Application.KillProcesses("Calculator");
GUI.Application.LaunchApplication("calc.exe");
GUI.Application.WaitForWindowByTitle("Calculator");
GUI.Application.ActivateWindowByTitle("Calculator");

GUI.Screenshot.WaitAndClickOnScreen("calc/1.png");
GUI.Screenshot.WaitAndClickOnScreen("calc/plus.png");
GUI.Screenshot.WaitAndClickOnScreen("calc/2.png");
GUI.Screenshot.WaitAndClickOnScreen("calc/equal.png",confidence:0.6);

/*
GUI.Application.LaunchApplication("chrome.exe", "https://www.baidu.com");
GUI.Application.WaitForWindowByTitle(s => s.Contains("百度一下"),timeoutSeconds:5);
GUI.Screenshot.WaitAndClickOnScreen("baidu/input.png");
GUI.Keyboard.Write("youzack");
Thread.Sleep(500);
GUI.Screenshot.Highlight("baidu/searchbtn.png");
GUI.Screenshot.WaitAndClickOnScreen("baidu/searchbtn.png");*/