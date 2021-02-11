using System;
using System.Windows.Forms;

namespace TableFindBackend.Forms
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void btnTables_Click(object sender, EventArgs e)
        {
            rtbStaff.Text = "When in admin mode, the User can drag the table items on the right-hand side of the screen to change the table's position. \n" +
                "The user can also double click on a table to " +
                "open the table editor, which will allow the user to change the name of the table, maximum capacity, availability, and view the reservations made under it by pressing " +
                "the 'bookings' button\n\n" +
                "When not in admin mode, the User can double click on any table item on the right-hand side of the screen. This will open the Bookings window where the user can " +
                "add reservations under the selected table.";
            lblHeading.Text = "Using and Managing the Tables";
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {

        }

        private void btnMenuEditor_Click(object sender, EventArgs e)
        {
            rtbStaff.Text = "Overview:\n\nThe Interactive Menu Editor can only be accessed after a successful Manager PIN has been provided in the given space. " +
               "The Edit Menu window will allow the user to to add, update ,and delete menu items. \n\n" +
               "Edit/Update\n\n" +
               "The user must select the menu item he/she would like to modify on the right side of the window. After selecting an item, the left side will become " +
               "enabled and can then be used to make changes to the selected item.\n\n" +
               "Adding Menu Item\n\n" +
               "The User can click on the 'Add Item' button. This will provide a new interface that will allow the user to specify all the details of the new item.";
            lblHeading.Text = "Interactive Menu Editor";
        }
    }
}
