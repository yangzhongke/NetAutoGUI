using System.Windows.Forms;

namespace NetAutoGUI.Windows
{
    partial class FormInput : Form
    {
        private FormInputType inputType = FormInputType.Plain;

        public FormInput()
        {
            InitializeComponent();
        }

        public FormInputType InputType
        {
            get => inputType;
            set
            {
                inputType = value;
                this.txtValue.UseSystemPasswordChar = value == FormInputType.Password;
            }
        }

        public string Value
        {
            get
            {
                return this.txtValue.Text;
            }
            set
            {
                this.txtValue.Text = value;
            }
        }

        public string OKText
        {
            get
            {
                return this.btnOK.Text;
            }
            set
            {
                this.btnOK.Text = value;
            }
        }

        public string CancelText
        {
            get
            {
                return this.btnCancel.Text;
            }
            set
            {
                this.btnCancel.Text = value;
            }
        }
    }
}
