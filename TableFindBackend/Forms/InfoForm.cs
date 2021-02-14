using System;
using System.Windows.Forms;

namespace TableFindBackend.Forms
{
    public partial class InfoForm : Form
    {
        //a cute little form which acts as a digital manual for the user(s) of the program. all information on this form is hard coded onto the TabControl pages on RichEditBoxes.
        public InfoForm()
        {
            //just a regular constructor, nothing to see here
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //just a regular close button which closes the current form
            this.Close();
        }
    }
}
