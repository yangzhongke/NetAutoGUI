namespace WinFormsAppForTest1;

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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        groupBox1 = new GroupBox();
        txtName = new TextBox();
        label1 = new Label();
        label2 = new Label();
        txtPhone = new TextBox();
        txtEmail = new TextBox();
        label3 = new Label();
        groupBox1.SuspendLayout();
        SuspendLayout();
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(txtEmail);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(txtPhone);
        groupBox1.Controls.Add(label2);
        groupBox1.Location = new Point(12, 43);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(699, 107);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Contact";
        // 
        // txtName
        // 
        txtName.Location = new Point(74, 12);
        txtName.Name = "txtName";
        txtName.Size = new Size(149, 23);
        txtName.TabIndex = 1;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(27, 12);
        label1.Name = "label1";
        label1.Size = new Size(39, 15);
        label1.TabIndex = 2;
        label1.Text = "Name";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(15, 26);
        label2.Name = "label2";
        label2.Size = new Size(41, 15);
        label2.TabIndex = 0;
        label2.Text = "Phone";
        // 
        // txtPhone
        // 
        txtPhone.Location = new Point(62, 20);
        txtPhone.Name = "txtPhone";
        txtPhone.Size = new Size(149, 23);
        txtPhone.TabIndex = 1;
        // 
        // txtEmail
        // 
        txtEmail.Location = new Point(62, 67);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(149, 23);
        txtEmail.TabIndex = 3;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(15, 73);
        label3.Name = "label3";
        label3.Size = new Size(36, 15);
        label3.TabIndex = 2;
        label3.Text = "Email";
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(label1);
        Controls.Add(txtName);
        Controls.Add(groupBox1);
        Name = "FormMain";
        Text = "WinFormsAppForTest1";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private GroupBox groupBox1;
    private TextBox txtName;
    private Label label1;
    private TextBox txtEmail;
    private Label label3;
    private TextBox txtPhone;
    private Label label2;
}