using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    public partial class ManageAdminUsersForm : Form
    {
        public ManageAdminUsersForm()
        {
            InitializeComponent();
            PopulateList();
        }
        private void PopulateList()
        {
            dgvAdmins.Rows.Clear();
            dgvUnactive.Rows.Clear();
            foreach (AdminPins a in OwnerStorage.ListOfAdmins)
            {
                if (a.Active == true)
                    dgvAdmins.Rows.Add(a.UserName, a.ContactNumber, a.objectId);
                else
                    dgvUnactive.Rows.Add(a.UserName, a.ContactNumber, a.objectId);
            }
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
        private void ShowLoading(bool toggle)
        {
            if (toggle == true)
            {
                pbxLoading.Visible = true;
                btnCancel.Enabled = false;
                btnConfirm.Enabled = false;
                btnExit.Enabled = false;
            }
            else
            {
                pbxLoading.Visible = false;
                btnCancel.Enabled = true;
                btnConfirm.Enabled = true;
                btnExit.Enabled = true;
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ShowLoading(true);
            AsyncCallback<BackendlessUser> callback = new AsyncCallback<BackendlessUser>(
            user =>
            {
                if (user.ObjectId == OwnerStorage.CurrentlyLoggedIn.ObjectId)
                {
                    Invoke(new Action(() =>
                    {
                        ShowLoading(false);
                        System.Console.WriteLine("User logged in. Assigned ID - " + user.ObjectId);
                        pnlPin.Enabled = true;
                        pnlLogin.Enabled = false;

                        OwnerStorage.FileWriter.WriteLineToFile("User logged in with valid login", true);

                    }));
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        ShowLoading(false);
                        MessageBox.Show(this, "You have logged in with another user's login credentials. Please login using your correct login credentials");
                    }));
                }
            },
            fault =>
            {
                Invoke(new Action(() =>
                {
                    ShowLoading(false);
                    MessageBox.Show(this, "Error: " + fault.Message);
                }));
                System.Console.WriteLine(fault.ToString());

            });

            String login = tbxEmail.Text;
            String password = tbxPassword.Text;
            Backendless.UserService.Login(login, password, callback);
        }
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
        private void btnAddNewAdmins_Click(object sender, EventArgs e)
        {
            AddEditNewAdminForm addForm = new AddEditNewAdminForm(null); // creating new admin user
            DialogResult result = addForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                PopulateList();
            }
        }

        private void dgvAdmins_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= -1)
            {
                string selected;
                selected = dgvAdmins.CurrentRow.Cells[2].Value.ToString();
                AdminPins temp = null;
                bool flag = false;
                foreach (AdminPins a in OwnerStorage.ListOfAdmins)
                {
                    if (a.objectId == selected)
                    {
                        temp = a;
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    MessageBox.Show(this, "No Admin User could be found. Please reopen the form and try again.", "No Admin Found");
                }
                else
                {
                    AddEditNewAdminForm newPin = new AddEditNewAdminForm(temp);
                    DialogResult result = newPin.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        PopulateList();
                    }
                }


            }
        }

        private void dgvUnactive_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= -1)
            {
                string selected;
                selected = dgvUnactive.CurrentRow.Cells[2].Value.ToString();
                AdminPins temp = null;
                bool flag = false;
                foreach (AdminPins a in OwnerStorage.ListOfAdmins)
                {
                    if (a.objectId == selected)
                    {
                        temp = a;
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    MessageBox.Show(this, "No Admin User could be found. Please reopen the form and try again.", "No Admin Found");
                }
                else
                {
                    AddEditNewAdminForm newPin = new AddEditNewAdminForm(temp);
                    DialogResult result = newPin.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        PopulateList();
                    }
                }

            }
        }

        private void ChangePinForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }
    }
}
