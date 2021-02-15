using System;
using System.Windows.Forms;

namespace TableFindBackend.Forms
{
    //This form acts as a digital manual for the user(s) of the program. All information on this form is hard coded onto the TabControl pages on RichEditBoxes
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
        }

        //Close button which closes the current form
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
