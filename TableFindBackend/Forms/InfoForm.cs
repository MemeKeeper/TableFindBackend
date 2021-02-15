using System;
using System.Windows.Forms;

namespace TableFindBackend.Forms
{
    public partial class InfoForm : Form
    {
        //A cute little form which acts as a digital manual for the user(s) of the program. All information on this form is hard coded onto the TabControl pages on RichEditBoxes
        public InfoForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Close button which closes the current form
            this.Close();
        }
    }
}
