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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        groupBox1 = new System.Windows.Forms.GroupBox();
        txtEmail = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        txtPhone = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        txtName = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        groupBox2 = new System.Windows.Forms.GroupBox();
        txtNum3 = new System.Windows.Forms.TextBox();
        BtnEqual = new System.Windows.Forms.Button();
        label4 = new System.Windows.Forms.Label();
        txtNum2 = new System.Windows.Forms.TextBox();
        txtNum1 = new System.Windows.Forms.TextBox();
        label5 = new System.Windows.Forms.Label();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(txtEmail);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(txtPhone);
        groupBox1.Controls.Add(label2);
        groupBox1.Location = new System.Drawing.Point(12, 43);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(699, 107);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Contact";
        // 
        // txtEmail
        // 
        txtEmail.Location = new System.Drawing.Point(62, 67);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new System.Drawing.Size(149, 23);
        txtEmail.TabIndex = 3;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(15, 73);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(36, 15);
        label3.TabIndex = 2;
        label3.Text = "Email";
        // 
        // txtPhone
        // 
        txtPhone.Location = new System.Drawing.Point(62, 20);
        txtPhone.Name = "txtPhone";
        txtPhone.Size = new System.Drawing.Size(149, 23);
        txtPhone.TabIndex = 1;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(15, 26);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(41, 15);
        label2.TabIndex = 0;
        label2.Text = "Phone";
        // 
        // txtName
        // 
        txtName.Location = new System.Drawing.Point(74, 12);
        txtName.Name = "txtName";
        txtName.Size = new System.Drawing.Size(149, 23);
        txtName.TabIndex = 1;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(27, 12);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(39, 15);
        label1.TabIndex = 2;
        label1.Text = "Name";
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(txtNum3);
        groupBox2.Controls.Add(BtnEqual);
        groupBox2.Controls.Add(label4);
        groupBox2.Controls.Add(txtNum2);
        groupBox2.Controls.Add(txtNum1);
        groupBox2.Location = new System.Drawing.Point(12, 156);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(699, 65);
        groupBox2.TabIndex = 3;
        groupBox2.TabStop = false;
        groupBox2.Text = "Calc";
        // 
        // txtNum3
        // 
        txtNum3.Location = new System.Drawing.Point(210, 22);
        txtNum3.Name = "txtNum3";
        txtNum3.ReadOnly = true;
        txtNum3.Size = new System.Drawing.Size(100, 23);
        txtNum3.TabIndex = 6;
        // 
        // BtnEqual
        // 
        BtnEqual.Location = new System.Drawing.Point(161, 24);
        BtnEqual.Name = "BtnEqual";
        BtnEqual.Size = new System.Drawing.Size(34, 21);
        BtnEqual.TabIndex = 5;
        BtnEqual.Text = "=";
        BtnEqual.UseVisualStyleBackColor = true;
        BtnEqual.Click += BtnEqual_Click;
        // 
        // label4
        // 
        label4.Location = new System.Drawing.Point(73, 27);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(18, 17);
        label4.TabIndex = 4;
        label4.Text = "+";
        // 
        // txtNum2
        // 
        txtNum2.Location = new System.Drawing.Point(101, 22);
        txtNum2.Name = "txtNum2";
        txtNum2.Size = new System.Drawing.Size(49, 23);
        txtNum2.TabIndex = 3;
        // 
        // txtNum1
        // 
        txtNum1.Location = new System.Drawing.Point(6, 22);
        txtNum1.Name = "txtNum1";
        txtNum1.Size = new System.Drawing.Size(62, 23);
        txtNum1.TabIndex = 1;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label5.Location = new System.Drawing.Point(287, 9);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(102, 32);
        label5.TabIndex = 4;
        label5.Text = "Zack666";
        // 
        // FormMain
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(label5);
        Controls.Add(groupBox2);
        Controls.Add(label1);
        Controls.Add(txtName);
        Controls.Add(groupBox1);
        Text = "WinFormsAppForTest1";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox txtNum2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtNum1;
    private System.Windows.Forms.Button BtnEqual;
    private System.Windows.Forms.TextBox txtNum3;

    #endregion

    private GroupBox groupBox1;
    private TextBox txtName;
    private Label label1;
    private TextBox txtEmail;
    private Label label3;
    private TextBox txtPhone;
    private Label label2;
    private Label label5;
}