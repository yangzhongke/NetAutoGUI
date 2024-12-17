using NetAutoGUI;
using NetAutoGUI.Windows;

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
        if (isRunning && GUI.Dialog.YesNoBox("Kill all notepad?")) GUI.Application.KillProcesses("notepad.exe");
    }

    private void BtnFindWindow_Click(object sender, EventArgs e)
    {
        var window = GUI.Application.FindWindowLikeTitle("*Notepad*");
        if (window == null)
            GUI.Dialog.Alert("Notepad is not running.");
        else
        {
            GUI.Dialog.Alert("Notepad is running:" + window.Title);
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
            GUI.Mouse.Move(10, 0);
            Thread.Sleep(10);
        }
        for (int i = 0; i < 20; i++)
        {
            GUI.Mouse.Move(0, 10);
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

    private void BtnDrawInPaint_Click(object sender, EventArgs e)
    {
        GUI.Application.LaunchApplication("mspaint.exe");
        GUI.Application.WaitForApplication("mspaint.exe");
        Window window = GUI.Application.WaitForWindow(w => w.Title.Contains("Paint"));
        window.Activate();
        window.Maximize();
        GUI.Mouse.MoveTo(100, 300);
        GUI.Mouse.MouseDown();
        for (int i = 0; i < 50; i++)
        {
            GUI.Mouse.Move(10, 0);
            Thread.Sleep(10);
        }
        GUI.Mouse.MouseUp();
    }

    private void BtnMouseScrollAndClick_Click(object sender, EventArgs e)
    {
        GUI.Application.LaunchApplication("chrome", "https://www.google.com/maps");
        GUI.Application.WaitForApplication("chrome");
        Window window = GUI.Application.WaitForWindow(w => w.Title.Contains("Google"));
        window.Activate();
        Thread.Sleep(2000);
        GUI.Mouse.MoveTo(300, 300);
        for (int i = 0; i < 5; i++)
        {
            GUI.Mouse.Scroll(50);
            Thread.Sleep(500);
        }
        for (int i = 0; i < 5; i++)
        {
            GUI.Mouse.Scroll(-50);
            Thread.Sleep(500);
        }
        GUI.Mouse.Click();
        Thread.Sleep(1000);
        GUI.Mouse.Click(button: MouseButtonType.Right);
        Thread.Sleep(1000);
        GUI.Mouse.DoubleClick(500, 500);
    }

    private void BtnKeyboardWrite_Click(object sender, EventArgs e)
    {
        GUI.Application.LaunchApplication("notepad");
        Window window = GUI.Application.WaitForWindow(w => w.Title.Contains("Notepad"));
        window.Activate();

        GUI.Keyboard.Write('a');
        GUI.Keyboard.Write('\n');
        GUI.Keyboard.Write("I'm Zack.\n");
        GUI.Keyboard.Write("Hello, World!", interval: 0.1);
    }

    private void BtnPress_Click(object sender, EventArgs e)
    {
        GUI.Application.LaunchApplication("notepad");
        Window window = GUI.Application.WaitForWindow(w => w.Title.Contains("Notepad"));
        window.Activate();
        GUI.Keyboard.Press(VirtualKeyCode.VK_0, VirtualKeyCode.VK_A);
    }

    private void BtnKeyDownUp_Click(object sender, EventArgs e)
    {
        GUI.Application.LaunchApplication("notepad");
        Window window = GUI.Application.WaitForWindow(w => w.Title.Contains("Notepad"));
        window.Activate();
        GUI.Keyboard.KeyDown(VirtualKeyCode.VK_A);
        GUI.Keyboard.KeyUp(VirtualKeyCode.VK_A);
    }

    private void BtnHotKey_Click(object sender, EventArgs e)
    {
        GUI.Application.LaunchApplication("notepad");
        Window window = GUI.Application.WaitForWindow(w => w.Title.Contains("Notepad"));
        window.Activate();
        GUI.Keyboard.Write("Hello, World!");
        GUI.Keyboard.HotKey(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);
        GUI.Keyboard.HotKey(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
        GUI.Keyboard.Press(VirtualKeyCode.RIGHT);
        GUI.Keyboard.HotKey(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
        GUI.Keyboard.HotKey(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
    }

    private void BtnHoldKey_Click(object sender, EventArgs e)
    {
        GUI.Application.LaunchApplication("notepad");
        Window window = GUI.Application.WaitForWindow(w => w.Title.Contains("Notepad"));
        window.Activate();
        GUI.Keyboard.Write("Hello, World!");
        using (GUI.Keyboard.Hold(VirtualKeyCode.CONTROL))
        {
            GUI.Keyboard.Press(VirtualKeyCode.VK_A);
        }
    }

    private void BtnDialog_Click(object sender, EventArgs e)
    {
        if (GUI.Dialog.Confirm("Are you sure?"))
        {
            GUI.Dialog.Alert("You clicked OK.");
        }
        else
        {
            GUI.Dialog.Alert("You clicked Cancel.");
        }
        if (GUI.Dialog.YesNoBox("Are you an alien?"))
        {
            GUI.Dialog.Alert("Welcome to Earth.");
        }
        else
        {
            GUI.Dialog.Alert("Welcome to New Zealand.");
        }
        string? name = GUI.Dialog.Prompt("What's your name?");
        if (name != null)
        {
            GUI.Dialog.Alert($"Hello, {name}.");
        }
        string? password = GUI.Dialog.Password("Enter your password");
        if (password != null)
        {
            GUI.Dialog.Alert($"Your password is {password}.");
        }
    }

    private void BtnFullScreenShot_Click(object sender, EventArgs e)
    {
        GUI.Screenshot.Screenshot().Save("1.png");
        GUI.Application.OpenFileWithDefaultApp("1.png");
    }

    private void BtnSelectFolder_Click(object sender, EventArgs e)
    {
        string? folder = GUI.Dialog.SelectFolder();
        if (folder != null)
        {
            GUI.Dialog.Alert($"You selected folder: {folder}");
        }
    }

    private void BtnOpenTextFile_Click(object sender, EventArgs e)
    {
        string? filePath = GUI.Dialog.SelectFileForLoad("Text files (*.txt)|*.txt|All files (*.*)|*.*");
        if (filePath != null)
        {
            GUI.Dialog.Alert($"You selected file: {filePath}");
        }
    }

    private void BtnSaveOfficeDocs_Click(object sender, EventArgs e)
    {
        string? filePath = GUI.Dialog.SelectFileForSave("Word files (*.docx)|*.docx|Excel files (*.xlsx)|*.xlsx|PowerPoint files (*.pptx)|*.pptx|All files (*.*)|*.*");
        if (filePath != null)
        {
            GUI.Dialog.Alert($"You selected file: {filePath}");
        }
    }

    private void BtnViewFile_Click(object sender, EventArgs e)
    {
        GUI.Application.OpenFileWithDefaultApp("C:\\Windows\\system.ini");
    }

    private void BtnWindowShot_Click(object sender, EventArgs e)
    {
        Window? window = GUI.Application.FindWindowLikeTitle("*Visual Studio*");
        if (window == null)
        {
            GUI.Dialog.Alert("Visual Studio is not running.");
            return;
        }
        window.Activate();
        GUI.Screenshot.Screenshot(window).Save("1.png");
        GUI.Application.OpenFileWithDefaultApp("1.png");
    }
}