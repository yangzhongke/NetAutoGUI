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
        groupBox5 = new GroupBox();
        BtnHighLightRect = new Button();
        BtnWindowShot = new Button();
        BtnFullScreenShot = new Button();
        groupBox4 = new GroupBox();
        BtnSaveOfficeDocs = new Button();
        BtnOpenTextFile = new Button();
        BtnSelectFolder = new Button();
        BtnDialog = new Button();
        groupBox3 = new GroupBox();
        BtnHoldKey = new Button();
        BtnHotKey = new Button();
        BtnKeyDownUp = new Button();
        BtnPress = new Button();
        BtnKeyboardWrite = new Button();
        groupBox2 = new GroupBox();
        BtnMouseScrollAndClick = new Button();
        BtnDrawInPaint = new Button();
        BtnMouseDrawRect = new Button();
        TxtBoxMousePosition = new TextBox();
        label1 = new Label();
        BtnMoveMouseTo100_100 = new Button();
        groupBox1 = new GroupBox();
        BtnViewFile = new Button();
        BtnStartNotePadThenKill = new Button();
        BtnClickNotepadMenu = new Button();
        BtnFindWindow = new Button();
        tabPage3 = new TabPage();
        timerMousePosition = new System.Windows.Forms.Timer(components);
        BtnLocateAll = new Button();
        tabControl1.SuspendLayout();
        tabPage1.SuspendLayout();
        groupBox5.SuspendLayout();
        groupBox4.SuspendLayout();
        groupBox3.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox1.SuspendLayout();
        SuspendLayout();
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPage1);
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
        tabPage1.Controls.Add(groupBox5);
        tabPage1.Controls.Add(groupBox4);
        tabPage1.Controls.Add(groupBox3);
        tabPage1.Controls.Add(groupBox2);
        tabPage1.Controls.Add(groupBox1);
        tabPage1.Location = new Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new Padding(3);
        tabPage1.Size = new Size(776, 533);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Basic";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // groupBox5
        // 
        groupBox5.Controls.Add(BtnLocateAll);
        groupBox5.Controls.Add(BtnHighLightRect);
        groupBox5.Controls.Add(BtnWindowShot);
        groupBox5.Controls.Add(BtnFullScreenShot);
        groupBox5.Location = new Point(6, 410);
        groupBox5.Name = "groupBox5";
        groupBox5.Size = new Size(762, 115);
        groupBox5.TabIndex = 6;
        groupBox5.TabStop = false;
        groupBox5.Text = "Screenshot";
        // 
        // BtnHighLightRect
        // 
        BtnHighLightRect.Location = new Point(348, 22);
        BtnHighLightRect.Name = "BtnHighLightRect";
        BtnHighLightRect.Size = new Size(156, 34);
        BtnHighLightRect.TabIndex = 2;
        BtnHighLightRect.Text = "HighLight Rect";
        BtnHighLightRect.UseVisualStyleBackColor = true;
        BtnHighLightRect.Click += BtnHighLightRect_Click;
        // 
        // BtnWindowShot
        // 
        BtnWindowShot.Location = new Point(176, 22);
        BtnWindowShot.Name = "BtnWindowShot";
        BtnWindowShot.Size = new Size(138, 34);
        BtnWindowShot.TabIndex = 1;
        BtnWindowShot.Text = "Window Shot";
        BtnWindowShot.UseVisualStyleBackColor = true;
        BtnWindowShot.Click += BtnWindowShot_Click;
        // 
        // BtnFullScreenShot
        // 
        BtnFullScreenShot.Location = new Point(6, 22);
        BtnFullScreenShot.Name = "BtnFullScreenShot";
        BtnFullScreenShot.Size = new Size(152, 34);
        BtnFullScreenShot.TabIndex = 0;
        BtnFullScreenShot.Text = "FullScreenShot";
        BtnFullScreenShot.UseVisualStyleBackColor = true;
        BtnFullScreenShot.Click += BtnFullScreenShot_Click;
        // 
        // groupBox4
        // 
        groupBox4.Controls.Add(BtnSaveOfficeDocs);
        groupBox4.Controls.Add(BtnOpenTextFile);
        groupBox4.Controls.Add(BtnSelectFolder);
        groupBox4.Controls.Add(BtnDialog);
        groupBox4.Location = new Point(6, 322);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(764, 82);
        groupBox4.TabIndex = 5;
        groupBox4.TabStop = false;
        groupBox4.Text = "Message";
        // 
        // BtnSaveOfficeDocs
        // 
        BtnSaveOfficeDocs.Location = new Point(560, 37);
        BtnSaveOfficeDocs.Name = "BtnSaveOfficeDocs";
        BtnSaveOfficeDocs.Size = new Size(134, 28);
        BtnSaveOfficeDocs.TabIndex = 3;
        BtnSaveOfficeDocs.Text = "Save Office Docs";
        BtnSaveOfficeDocs.UseVisualStyleBackColor = true;
        BtnSaveOfficeDocs.Click += BtnSaveOfficeDocs_Click;
        // 
        // BtnOpenTextFile
        // 
        BtnOpenTextFile.Location = new Point(348, 36);
        BtnOpenTextFile.Name = "BtnOpenTextFile";
        BtnOpenTextFile.Size = new Size(156, 29);
        BtnOpenTextFile.TabIndex = 2;
        BtnOpenTextFile.Text = "Open Text File";
        BtnOpenTextFile.UseVisualStyleBackColor = true;
        BtnOpenTextFile.Click += BtnOpenTextFile_Click;
        // 
        // BtnSelectFolder
        // 
        BtnSelectFolder.Location = new Point(176, 33);
        BtnSelectFolder.Name = "BtnSelectFolder";
        BtnSelectFolder.Size = new Size(138, 32);
        BtnSelectFolder.TabIndex = 1;
        BtnSelectFolder.Text = "Select Folder";
        BtnSelectFolder.UseVisualStyleBackColor = true;
        BtnSelectFolder.Click += BtnSelectFolder_Click;
        // 
        // BtnDialog
        // 
        BtnDialog.Location = new Point(6, 33);
        BtnDialog.Name = "BtnDialog";
        BtnDialog.Size = new Size(152, 32);
        BtnDialog.TabIndex = 0;
        BtnDialog.Text = "Message and Prompt";
        BtnDialog.UseVisualStyleBackColor = true;
        BtnDialog.Click += BtnDialog_Click;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(BtnHoldKey);
        groupBox3.Controls.Add(BtnHotKey);
        groupBox3.Controls.Add(BtnKeyDownUp);
        groupBox3.Controls.Add(BtnPress);
        groupBox3.Controls.Add(BtnKeyboardWrite);
        groupBox3.Location = new Point(4, 195);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(766, 110);
        groupBox3.TabIndex = 4;
        groupBox3.TabStop = false;
        groupBox3.Text = "Keyboard";
        // 
        // BtnHoldKey
        // 
        BtnHoldKey.Location = new Point(8, 64);
        BtnHoldKey.Name = "BtnHoldKey";
        BtnHoldKey.Size = new Size(152, 31);
        BtnHoldKey.TabIndex = 4;
        BtnHoldKey.Text = "Hold";
        BtnHoldKey.UseVisualStyleBackColor = true;
        BtnHoldKey.Click += BtnHoldKey_Click;
        // 
        // BtnHotKey
        // 
        BtnHotKey.Location = new Point(552, 20);
        BtnHotKey.Name = "BtnHotKey";
        BtnHotKey.Size = new Size(144, 34);
        BtnHotKey.TabIndex = 3;
        BtnHotKey.Text = "HotKey";
        BtnHotKey.UseVisualStyleBackColor = true;
        BtnHotKey.Click += BtnHotKey_Click;
        // 
        // BtnKeyDownUp
        // 
        BtnKeyDownUp.Location = new Point(350, 21);
        BtnKeyDownUp.Name = "BtnKeyDownUp";
        BtnKeyDownUp.Size = new Size(156, 33);
        BtnKeyDownUp.TabIndex = 2;
        BtnKeyDownUp.Text = "KeyDown/Up";
        BtnKeyDownUp.UseVisualStyleBackColor = true;
        BtnKeyDownUp.Click += BtnKeyDownUp_Click;
        // 
        // BtnPress
        // 
        BtnPress.Location = new Point(178, 20);
        BtnPress.Name = "BtnPress";
        BtnPress.Size = new Size(138, 34);
        BtnPress.TabIndex = 1;
        BtnPress.Text = "Press";
        BtnPress.UseVisualStyleBackColor = true;
        BtnPress.Click += BtnPress_Click;
        // 
        // BtnKeyboardWrite
        // 
        BtnKeyboardWrite.Location = new Point(8, 22);
        BtnKeyboardWrite.Name = "BtnKeyboardWrite";
        BtnKeyboardWrite.Size = new Size(152, 34);
        BtnKeyboardWrite.TabIndex = 0;
        BtnKeyboardWrite.Text = "Write";
        BtnKeyboardWrite.UseVisualStyleBackColor = true;
        BtnKeyboardWrite.Click += BtnKeyboardWrite_Click;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(BtnMouseScrollAndClick);
        groupBox2.Controls.Add(BtnDrawInPaint);
        groupBox2.Controls.Add(BtnMouseDrawRect);
        groupBox2.Controls.Add(TxtBoxMousePosition);
        groupBox2.Controls.Add(label1);
        groupBox2.Controls.Add(BtnMoveMouseTo100_100);
        groupBox2.Location = new Point(3, 83);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(767, 100);
        groupBox2.TabIndex = 3;
        groupBox2.TabStop = false;
        groupBox2.Text = "Mouse";
        // 
        // BtnMouseScrollAndClick
        // 
        BtnMouseScrollAndClick.Location = new Point(553, 55);
        BtnMouseScrollAndClick.Name = "BtnMouseScrollAndClick";
        BtnMouseScrollAndClick.Size = new Size(144, 34);
        BtnMouseScrollAndClick.TabIndex = 11;
        BtnMouseScrollAndClick.Text = "Scroll and click";
        BtnMouseScrollAndClick.UseVisualStyleBackColor = true;
        BtnMouseScrollAndClick.Click += BtnMouseScrollAndClick_Click;
        // 
        // BtnDrawInPaint
        // 
        BtnDrawInPaint.Location = new Point(351, 55);
        BtnDrawInPaint.Name = "BtnDrawInPaint";
        BtnDrawInPaint.Size = new Size(156, 34);
        BtnDrawInPaint.TabIndex = 10;
        BtnDrawInPaint.Text = "Draw In Paint";
        BtnDrawInPaint.UseVisualStyleBackColor = true;
        BtnDrawInPaint.Click += BtnDrawInPaint_Click;
        // 
        // BtnMouseDrawRect
        // 
        BtnMouseDrawRect.Location = new Point(179, 55);
        BtnMouseDrawRect.Name = "BtnMouseDrawRect";
        BtnMouseDrawRect.Size = new Size(138, 34);
        BtnMouseDrawRect.TabIndex = 9;
        BtnMouseDrawRect.Text = "Mouse Draw Rect";
        BtnMouseDrawRect.UseVisualStyleBackColor = true;
        BtnMouseDrawRect.Click += BtnMouseDrawRect_Click;
        // 
        // TxtBoxMousePosition
        // 
        TxtBoxMousePosition.Location = new Point(120, 14);
        TxtBoxMousePosition.Name = "TxtBoxMousePosition";
        TxtBoxMousePosition.ReadOnly = true;
        TxtBoxMousePosition.Size = new Size(156, 23);
        TxtBoxMousePosition.TabIndex = 8;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(3, 19);
        label1.Name = "label1";
        label1.Size = new Size(92, 15);
        label1.TabIndex = 7;
        label1.Text = "Mouse Position:";
        // 
        // BtnMoveMouseTo100_100
        // 
        BtnMoveMouseTo100_100.Location = new Point(9, 55);
        BtnMoveMouseTo100_100.Name = "BtnMoveMouseTo100_100";
        BtnMoveMouseTo100_100.Size = new Size(152, 34);
        BtnMoveMouseTo100_100.TabIndex = 6;
        BtnMoveMouseTo100_100.Text = "Move To 100,100";
        BtnMoveMouseTo100_100.UseVisualStyleBackColor = true;
        BtnMoveMouseTo100_100.Click += BtnMoveMouseTo100_100_Click;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(BtnViewFile);
        groupBox1.Controls.Add(BtnStartNotePadThenKill);
        groupBox1.Controls.Add(BtnClickNotepadMenu);
        groupBox1.Controls.Add(BtnFindWindow);
        groupBox1.Location = new Point(4, 0);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(766, 77);
        groupBox1.TabIndex = 2;
        groupBox1.TabStop = false;
        groupBox1.Text = "Application";
        // 
        // BtnViewFile
        // 
        BtnViewFile.Location = new Point(552, 22);
        BtnViewFile.Name = "BtnViewFile";
        BtnViewFile.Size = new Size(144, 34);
        BtnViewFile.TabIndex = 2;
        BtnViewFile.Text = "View File";
        BtnViewFile.UseVisualStyleBackColor = true;
        BtnViewFile.Click += BtnViewFile_Click;
        // 
        // BtnStartNotePadThenKill
        // 
        BtnStartNotePadThenKill.Location = new Point(8, 22);
        BtnStartNotePadThenKill.Name = "BtnStartNotePadThenKill";
        BtnStartNotePadThenKill.Size = new Size(152, 34);
        BtnStartNotePadThenKill.TabIndex = 0;
        BtnStartNotePadThenKill.Text = "Start NotePad Then Kill";
        BtnStartNotePadThenKill.UseVisualStyleBackColor = true;
        BtnStartNotePadThenKill.Click += BtnStartNotePadThenKill_Click;
        // 
        // BtnClickNotepadMenu
        // 
        BtnClickNotepadMenu.Location = new Point(350, 23);
        BtnClickNotepadMenu.Name = "BtnClickNotepadMenu";
        BtnClickNotepadMenu.Size = new Size(156, 34);
        BtnClickNotepadMenu.TabIndex = 1;
        BtnClickNotepadMenu.Text = "Click Notepad Menu";
        BtnClickNotepadMenu.UseVisualStyleBackColor = true;
        BtnClickNotepadMenu.Click += BtnClickNotepadMenu_Click;
        // 
        // BtnFindWindow
        // 
        BtnFindWindow.Location = new Point(178, 23);
        BtnFindWindow.Name = "BtnFindWindow";
        BtnFindWindow.Size = new Size(138, 34);
        BtnFindWindow.TabIndex = 1;
        BtnFindWindow.Text = "FindWindow";
        BtnFindWindow.UseVisualStyleBackColor = true;
        BtnFindWindow.Click += BtnFindWindow_Click;
        // 
        // tabPage3
        // 
        tabPage3.Location = new Point(4, 24);
        tabPage3.Name = "tabPage3";
        tabPage3.Padding = new Padding(3);
        tabPage3.Size = new Size(776, 533);
        tabPage3.TabIndex = 2;
        tabPage3.Text = "Advanced";
        tabPage3.UseVisualStyleBackColor = true;
        // 
        // timerMousePosition
        // 
        timerMousePosition.Enabled = true;
        timerMousePosition.Tick += timerMousePosition_Tick;
        // 
        // BtnLocateAll
        // 
        BtnLocateAll.Location = new Point(560, 22);
        BtnLocateAll.Name = "BtnLocateAll";
        BtnLocateAll.Size = new Size(134, 34);
        BtnLocateAll.TabIndex = 3;
        BtnLocateAll.Text = "Locate All";
        BtnLocateAll.UseVisualStyleBackColor = true;
        BtnLocateAll.Click += BtnLocateAll_Click;
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
        groupBox5.ResumeLayout(false);
        groupBox4.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox1.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button BtnFindWindow;

    private System.Windows.Forms.TabControl tabControl1;

    #endregion

    private TabPage tabPage1;
    private TabPage tabPage3;
    private System.Windows.Forms.Button BtnStartNotePadThenKill;
    private Button BtnClickNotepadMenu;
    private System.Windows.Forms.Timer timerMousePosition;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private Button BtnMouseScrollAndClick;
    private Button BtnDrawInPaint;
    private Button BtnMouseDrawRect;
    private TextBox TxtBoxMousePosition;
    private Label label1;
    private Button BtnMoveMouseTo100_100;
    private GroupBox groupBox3;
    private Button BtnKeyboardWrite;
    private Button BtnPress;
    private Button BtnKeyDownUp;
    private Button BtnHotKey;
    private Button BtnHoldKey;
    private GroupBox groupBox4;
    private Button BtnDialog;
    private GroupBox groupBox5;
    private Button BtnFullScreenShot;
    private Button BtnSelectFolder;
    private Button BtnWindowShot;
    private Button BtnOpenTextFile;
    private Button BtnSaveOfficeDocs;
    private Button BtnViewFile;
    private Button BtnHighLightRect;
    private Button BtnLocateAll;
}