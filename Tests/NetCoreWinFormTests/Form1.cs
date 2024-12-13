using NetAutoGUI;
using System;

namespace NetCoreWinFormTests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var screenshot = GUI.Screenshot.Screenshot();
            screenshot.Save("d:/1.png");
            var loc1 = GUI.Screenshot.LocateAll(screenshot, "test2.png").First().Center;
            Console.WriteLine(loc1);
            GUI.Mouse.DoubleClick(loc1.X, loc1.Y);
        }
    }
}
