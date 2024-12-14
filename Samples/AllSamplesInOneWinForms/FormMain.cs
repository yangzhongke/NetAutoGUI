using NetAutoGUI;

namespace AllSamplesInOneWinForms;

public partial class FormMain : Form
{
    public FormMain()
    {
        InitializeComponent();
    }

    private void BtnStartNotePadThenKill_Click(object sender, EventArgs e)
    {
        GUI.Application.LaunchApplication("notepad.exe", "C:\\Windows\\system.ini");
        GUI.Application.WaitForApplication("notepad.exe");
        var isRunning = GUI.Application.IsApplicationRunning("notepad.exe");
        if (isRunning && GUI.MessageBox.YesNoBox("Kill all notepad?")) GUI.Application.KillProcesses("notepad.exe");
    }

    private void BtnFindWindow_Click(object sender, EventArgs e)
    {
        var window = GUI.Application.FindWindowLikeTitle("*Notepad*");
        if (window == null)
            GUI.MessageBox.Alert("Notepad is not running.");
        else
        {
            GUI.MessageBox.Alert("Notepad is running:" + window.Title);
            window.Activate();
            window.Maximize();
        }
    }

    private void BtnClickNotepadMenu_Click(object sender, EventArgs e)
    {
        if (!GUI.Application.IsApplicationRunning("notepad.exe"))
        {
            GUI.Application.LaunchApplication("notepad.exe");
            GUI.Application.WaitForApplication("notepad.exe");
        }
        Window window = GUI.Application.WaitForWindow(w => w.Title.Contains("Notepad") && !w.Title.Contains("Notepad++"));
        window.Activate();
        window.GetMainMenu().Edit.TimeDate.Click();
        for (int i = 0; i < 5; i++)
        {
            Thread.Sleep(500);
            window.GetMainMenu().View.Zoom.ZoomIn.Click();
        }
    }

    private void timerMousePosition_Tick(object sender, EventArgs e)
    {
        var mousePos = GUI.Mouse.Position();
        TxtBoxMousePosition.Text = $"X:{mousePos.X}, Y:{mousePos.Y}";
    }

    private void BtnMoveMouseTo100_100_Click(object sender, EventArgs e)
    {
        GUI.Mouse.MoveTo(100, 100);
    }

    private void BtnMouseDrawRect_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < 50; i++)
        {
            GUI.Mouse.Move(10,0);
            Thread.Sleep(10);
        }
        for (int i = 0; i < 20; i++)
        {
            GUI.Mouse.Move(0,10);
            Thread.Sleep(10);
        }
        for (int i = 0; i < 50; i++)
        {
            GUI.Mouse.Move(-10, 0);
            Thread.Sleep(10);
        }
        for (int i = 0; i < 20; i++)
        {
            GUI.Mouse.Move(0, -10);
            Thread.Sleep(10);
        }
    }
}