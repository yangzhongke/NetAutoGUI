namespace WinFormsAppForTest1;

public partial class FormMain : Form
{
    public FormMain()
    {
        InitializeComponent();
        this.Load += FormMainOnLoad;
    }

    private async void FormMainOnLoad(object? sender, EventArgs e)
    {
        await Task.Delay(1000);
        var scenario = Environment.GetCommandLineArgs().Skip(1).FirstOrDefault();
        if (scenario == "multi-windows")
        {
            Form1 form1 = new Form1();
            form1.Show(this);
            Form2 form2 = new Form2();
            form2.ShowDialog(this);
        }
    }

    private void BtnEqual_Click(object sender, EventArgs e)
    {
        txtNum3.Text = (Convert.ToInt32(txtNum1.Text) + Convert.ToInt32(txtNum2.Text)).ToString();
    }
}