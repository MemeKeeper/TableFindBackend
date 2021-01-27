using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    public partial class ChangePinForm : Form
    {
        public ChangePinForm()
        {
            InitializeComponent();
            dgvAdmins.DataSource = OwnerStorage.ListOfAdmins;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            pbxLoading.Visible = true;
            AsyncCallback<BackendlessUser> callback = new AsyncCallback<BackendlessUser>(
            user =>
            {
                Invoke(new Action(() =>
                {
                    pbxLoading.Visible = false;
                    System.Console.WriteLine("User logged in. Assigned ID - " + user.ObjectId);
                    pnlPin.Enabled = true;
                    pnlLogin.Enabled = false;

                    OwnerStorage.FileWriter.WriteLineToFile("User logged in with valid login", true);

                }));
            },
            fault =>
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, "Error: " + fault.Message);
                    pbxLoading.Visible = false;
                }));
                System.Console.WriteLine(fault.ToString());
                
            });

            String login = tbxEmail.Text;
            String password = tbxPassword.Text;
            Backendless.UserService.Login(login, password, callback);
        }

        private void lblPin1_Click(object sender, EventArgs e)
        {

        }       

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnAddNewAdmins_Click(object sender, EventArgs e)
        {
            AddEditNewAdminForm addForm = new AddEditNewAdminForm(null); // creating new admin user
            DialogResult result = addForm.ShowDialog();

            if(result == DialogResult.OK)
            {
                dgvAdmins.Update();
                dgvAdmins.Refresh();
            }
        }

        private void dgvAdmins_CellDoubleClick(object sender, DataGridViewCellEventArgs e)        
        {
            if (e.RowIndex >= 0 && !(sender is DataGridViewHeaderCell))
            {
                AdminPins selectedAdmin = (AdminPins)dgvAdmins.CurrentRow.DataBoundItem;

                AddEditNewAdminForm newPin = new AddEditNewAdminForm(selectedAdmin);
                DialogResult result = newPin.ShowDialog();

                if (result == DialogResult.OK)
                {
                    dgvAdmins.Update();
                    dgvAdmins.Refresh();

                }
            }
        }
    }
}
