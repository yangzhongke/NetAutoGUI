namespace AllSamplesInOneWinForms;

partial class FormMain
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        tabControl1 = new TabControl();
        tabPage1 = new TabPage();
        BtnFindWindow = new Button();
        BtnClickNotepadMenu = new Button();
        BtnStartNotePadThenKill = new Button();
        tabPageMouse = new TabPage();
        TxtBoxMousePosition = new TextBox();
        label1 = new Label();
        BtnMoveMouseTo100_100 = new Button();
        tabPage3 = new TabPage();
        timerMousePosition = new System.Windows.Forms.Timer(components);
        BtnMouseDrawRect = new Button();
        tabControl1.SuspendLayout();
        tabPage1.SuspendLayout();
        tabPageMouse.SuspendLayout();
        SuspendLayout();
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPage1);
        tabControl1.Controls.Add(tabPageMouse);
        tabControl1.Controls.Add(tabPage3);
        tabControl1.Dock = DockStyle.Fill;
        tabControl1.Location = new Point(0, 0);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(784, 561);
        tabControl1.TabIndex = 0;
        // 
        // tabPage1
        // 
        tabPage1.Controls.Add(BtnFindWindow);
        tabPage1.Controls.Add(BtnClickNotepadMenu);
        tabPage1.Controls.Add(BtnStartNotePadThenKill);
        tabPage1.Location = new Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new Padding(3);
        tabPage1.Size = new Size(776, 533);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Application";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // BtnFindWindow
        // 
        BtnFindWindow.Location = new Point(176, 7);
        BtnFindWindow.Name = "BtnFindWindow";
        BtnFindWindow.Size = new Size(105, 34);
        BtnFindWindow.TabIndex = 1;
        BtnFindWindow.Text = "FindWindow";
        BtnFindWindow.UseVisualStyleBackColor = true;
        BtnFindWindow.Click += BtnFindWindow_Click;
        // 
        // BtnClickNotepadMenu
        // 
        BtnClickNotepadMenu.Location = new Point(308, 6);
        BtnClickNotepadMenu.Name = "BtnClickNotepadMenu";
        BtnClickNotepadMenu.Size = new Size(156, 35);
        BtnClickNotepadMenu.TabIndex = 1;
        BtnClickNotepadMenu.Text = "Click Notepad Menu";
        BtnClickNotepadMenu.UseVisualStyleBackColor = true;
        BtnClickNotepadMenu.Click += BtnClickNotepadMenu_Click;
        // 
        // BtnStartNotePadThenKill
        // 
        BtnStartNotePadThenKill.Location = new Point(6, 6);
        BtnStartNotePadThenKill.Name = "BtnStartNotePadThenKill";
        BtnStartNotePadThenKill.Size = new Size(152, 35);
        BtnStartNotePadThenKill.TabIndex = 0;
        BtnStartNotePadThenKill.Text = "Start NotePad Then Kill";
        BtnStartNotePadThenKill.UseVisualStyleBackColor = true;
        BtnStartNotePadThenKill.Click += BtnStartNotePadThenKill_Click;
        // 
        // tabPageMouse
        // 
        tabPageMouse.Controls.Add(BtnMouseDrawRect);
        tabPageMouse.Controls.Add(TxtBoxMousePosition);
        tabPageMouse.Controls.Add(label1);
        tabPageMouse.Controls.Add(BtnMoveMouseTo100_100);
        tabPageMouse.Location = new Point(4, 24);
        tabPageMouse.Name = "tabPageMouse";
        tabPageMouse.Padding = new Padding(3);
        tabPageMouse.Size = new Size(776, 533);
        tabPageMouse.TabIndex = 1;
        tabPageMouse.Text = "Mouse";
        tabPageMouse.UseVisualStyleBackColor = true;
        // 
        // TxtBoxMousePosition
        // 
        TxtBoxMousePosition.Location = new Point(125, 12);
        TxtBoxMousePosition.Name = "TxtBoxMousePosition";
        TxtBoxMousePosition.ReadOnly = true;
        TxtBoxMousePosition.Size = new Size(156, 23);
        TxtBoxMousePosition.TabIndex = 2;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(8, 17);
        label1.Name = "label1";
        label1.Size = new Size(92, 15);
        label1.TabIndex = 1;
        label1.Text = "Mouse Position:";
        // 
        // BtnMoveMouseTo100_100
        // 
        BtnMoveMouseTo100_100.Location = new Point(6, 58);
        BtnMoveMouseTo100_100.Name = "BtnMoveMouseTo100_100";
        BtnMoveMouseTo100_100.Size = new Size(118, 29);
        BtnMoveMouseTo100_100.TabIndex = 0;
        BtnMoveMouseTo100_100.Text = "Move To 100,100";
        BtnMoveMouseTo100_100.UseVisualStyleBackColor = true;
        BtnMoveMouseTo100_100.Click += BtnMoveMouseTo100_100_Click;
        // 
        // tabPage3
        // 
        tabPage3.Location = new Point(4, 24);
        tabPage3.Name = "tabPage3";
        tabPage3.Padding = new Padding(3);
        tabPage3.Size = new Size(776, 533);
        tabPage3.TabIndex = 2;
        tabPage3.Text = "tabPage3";
        tabPage3.UseVisualStyleBackColor = true;
        // 
        // timerMousePosition
        // 
        timerMousePosition.Enabled = true;
        timerMousePosition.Tick += timerMousePosition_Tick;
        // 
        // BtnMouseDrawRect
        // 
        BtnMouseDrawRect.Location = new Point(143, 58);
        BtnMouseDrawRect.Name = "BtnMouseDrawRect";
        BtnMouseDrawRect.Size = new Size(138, 29);
        BtnMouseDrawRect.TabIndex = 3;
        BtnMouseDrawRect.Text = "Mouse Draw Rect";
        BtnMouseDrawRect.UseVisualStyleBackColor = true;
        BtnMouseDrawRect.Click += BtnMouseDrawRect_Click;
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(784, 561);
        Controls.Add(tabControl1);
        MaximizeBox = false;
        Name = "FormMain";
        Text = "NetAutoGUI Samples";
        tabControl1.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        tabPageMouse.ResumeLayout(false);
        tabPageMouse.PerformLayout();
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button BtnFindWindow;

    private System.Windows.Forms.TabControl tabControl1;

    #endregion

    private TabPage tabPage1;
    private TabPage tabPageMouse;
    private TabPage tabPage3;
    private System.Windows.Forms.Button BtnStartNotePadThenKill;
    private Button BtnClickNotepadMenu;
    private Button BtnMoveMouseTo100_100;
    private Label label1;
    private TextBox TxtBoxMousePosition;
    private System.Windows.Forms.Timer timerMousePosition;
    private Button BtnMouseDrawRect;
}