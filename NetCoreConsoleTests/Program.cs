using NetAutoGUI;
using NetAutoGUI.Windows;
/*
GUI.Mouse.MoveTo(1000, 1000, 3, TweeningType.BounceInOut);
GUI.Mouse.MoveTo(1000, 800);*/
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
GUI.Keyboard.HotKey(VirtualKeyCode.CONTROL, VirtualKeyCode.F1);
GUI.Keyboard.Press(VirtualKeyCode.VK_A);
using (GUI.Keyboard.Hold(VirtualKeyCode.SHIFT))
{
    GUI.Keyboard.Press(VirtualKeyCode.VK_A);
    GUI.Keyboard.Press(VirtualKeyCode.VK_A);
    Thread.Sleep(1000);
}
GUI.Keyboard.Press(VirtualKeyCode.VK_A);*/
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
GUI.Screenshot.Screenshot().Save("e:/temp/1.jpg"); 
GUI.Screenshot.Screenshot().Save("d:/1.png");
GUI.Screenshot.Screenshot(new Rectangle(10, 10, 250, 250)).Save("d:/1.jpeg");
GUI.Screenshot.Screenshot().Save("d:/1.webp",ImageType.WebP);*/

/*
GUI.Application.KillProcesses("Calculator");
GUI.Application.LaunchApplication("calc.exe");
Window winCalc = GUI.Application.WaitForWindowByTitle("Calculator");
winCalc.Activate();
winCalc.WaitAndClick("calc/1.png",confidence:0.6);
winCalc.WaitAndClick("calc/plus.png", confidence: 0.6);
winCalc.WaitAndClick("calc/2.png", confidence: 0.6);
winCalc.WaitAndClick("calc/equal.png",confidence:0.6);
*/


GUI.Application.LaunchApplication("chrome.exe", "https://www.baidu.com");
var winChrome = GUI.Application.WaitForWindowLikeTitle("*百度一下*", timeoutSeconds:5);
Thread.Sleep(1000);
GUI.Keyboard.Write("youzack");
GUI.Keyboard.Press(VirtualKeyCode.RETURN);
/*
GUI.Application.LaunchApplication("notepad.exe");
GUI.Application.WaitForWindowByTitle(t => t.Contains("记事本"));
GUI.Application.ActivateWindowByTitle(t=>t.Contains("记事本"));
GUI.Keyboard.Write("你好，我是杨中科");*/
/*
GUI.Application.LaunchApplication("wordpad.exe");
GUI.Application.WaitForWindowLikeTitle("*WordPad");
GUI.Application.ActivateWindowLikeTitle("*WordPad");
GUI.Keyboard.Write("你好，我是杨中科");
using (GUI.Keyboard.Hold(VirtualKeyCode.CONTROL))
{
    GUI.Keyboard.Press(VirtualKeyCode.VK_S);
}*/
/*
Window win = GUI.Application.FindWindowLikeTitle("*微信小号*");
win.Activate();
win.Maximize();
string lastWord = "";
while(true)
{
    //获取按照纵坐标排序最后一个元素
    var lastZack = GUI.Screenshot.LocateAllOnScreen("zack.png").LastOrDefault();
    if (lastZack == null) break;
    var lastSentenseX = lastZack.X + 60;
    var lastSentenseY = lastZack.Y + 10;
    GUI.Mouse.DoubleClick(lastSentenseX, lastSentenseY);
    GUI.Keyboard.Ctrl_C();
    var clipTxt = ClipboardHelpers.GetClipboardText();
    if(clipTxt!=lastWord)
    {
        string response = "你说啥？";
        if(clipTxt.Contains("是谁")|| clipTxt.Contains("名字"))
        {
            response = "我是杨中科";
        }
        else if(clipTxt.Contains("天气"))
        {
            response = "今天天气晴朗";
        }
        else if (clipTxt.Contains("岁")|| clipTxt.Contains("年龄"))
        {
            response = "我18岁了";
        }
        var rectEmoji = GUI.Screenshot.LocateOnScreen("emoji.png");
        GUI.Mouse.Click(rectEmoji.X, rectEmoji.Y + 50);
        GUI.Keyboard.Write(response);
        GUI.Keyboard.Press(VirtualKeyCode.RETURN);
    }
    lastWord = clipTxt;
}*/

/*
Window win = GUI.Application.FindWindowLikeTitle("*微信小号*");
win.Activate();
win.Maximize();
string lastWord = "";
while (true)
{
    //获取按照纵坐标排序最后一个元素
    var lastZack = GUI.Screenshot.LocateAllOnScreen("zack.png").LastOrDefault();
    if (lastZack == null) break;
    var lastSentenseX = lastZack.X + 60;
    var lastSentenseY = lastZack.Y + 10;
    GUI.Mouse.DoubleClick(lastSentenseX, lastSentenseY);
    GUI.Keyboard.Ctrl_C();
    var clipTxt = ClipboardHelpers.GetClipboardText();
    if (clipTxt != lastWord)
    {
        string response = "你说啥？";
        if (clipTxt.Contains("是谁") || clipTxt.Contains("名字"))
        {
            response = "我是杨中科";
        }
        else if (clipTxt.Contains("天气"))
        {
            response = "今天天气晴朗";
        }
        else if (clipTxt.Contains("岁") || clipTxt.Contains("年龄"))
        {
            response = "我18岁了";
        }
        var rectEmoji = GUI.Screenshot.LocateOnScreen("emoji.png");
        GUI.Mouse.Click(rectEmoji.X, rectEmoji.Y + 50);
        GUI.Keyboard.Write(response);
        GUI.Keyboard.Press(VirtualKeyCode.RETURN);
    }
    lastWord = clipTxt;
}*/

/*
Window win = GUI.Application.FindWindowLikeTitle("*微信小号*");
win.Activate();
string lastWord = "";
while (true)
{
    //获取按照纵坐标排序最后一个元素
    var lastZack = win.LocateAll("zack.png").LastOrDefault();
    if (lastZack == null) break;
    var lastSentenseX = lastZack.X + 60;
    var lastSentenseY = lastZack.Y + 10;
    win.DoubleClick(lastSentenseX, lastSentenseY);
    GUI.Keyboard.Ctrl_C();
    var clipTxt = ClipboardHelpers.GetClipboardText();
    if (clipTxt != lastWord)
    {
        string response = "你说啥？";
        if (clipTxt.Contains("是谁") || clipTxt.Contains("名字"))
        {
            response = "我是杨中科";
        }
        else if (clipTxt.Contains("天气"))
        {
            response = "今天天气晴朗";
        }
        else if (clipTxt.Contains("岁") || clipTxt.Contains("年龄"))
        {
            response = "我18岁了";
        }
        var rectEmoji = win.Locate("emoji.png");
        win.Click(rectEmoji.X, rectEmoji.Y + 50);
        GUI.Keyboard.Write(response);
        GUI.Keyboard.Press(VirtualKeyCode.RETURN);
    }
    lastWord = clipTxt;
}*/