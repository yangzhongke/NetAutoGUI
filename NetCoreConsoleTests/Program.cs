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
GUI.MessageBox.Alert("Alert");
GUI.MessageBox.Alert(GUI.MessageBox.Confirm("真的吗？").ToString());
GUI.MessageBox.Alert(GUI.MessageBox.YesNoBox("你是男的吗？",title:"真的吗？").ToString());