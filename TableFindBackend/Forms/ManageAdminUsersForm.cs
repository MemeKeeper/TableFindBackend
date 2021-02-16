using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

namespace TableFindBackend.Forms
{
    //This form is used to create Admin users which will have more elevated permissions on the program
    public partial class ManageAdminUsersForm : Form
    {
        //Basic Constructor
        public ManageAdminUsersForm()
        {
            InitializeComponent();
            PopulateList();
        }

        //Method that will populate the small DataGridView with all the existing Admin users
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

        //Button used to close the form if the user wishes to not save changes made
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //A method that will appear on all forms. It simulates a loading screen by showing and hiding all necessary buttons and interface elements
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

        //This method will open the AddEditNewAdminForm with a Null as parameter. This signals to the form that a new user is being created
        private void btnAddNewAdmins_Click(object sender, EventArgs e)
        {
            //Creating new Admin user
            AddEditNewAdminForm addForm = new AddEditNewAdminForm(null); 
            DialogResult result = addForm.ShowDialog();

            //Upon returning with success, the form will reload the DataGridView with the new information
            if (result == DialogResult.OK)
            {
                PopulateList();
            }
        }

        //This methods handles the DataGridView_OnCellDoubleClick event. It will also launch the AddEditAdminForm, but with a valid AdminPins object, signaling that an existing AdminPin is being edited
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

        //This methods handles the DataGridView_OnCellDoubleClick event. It will also launch the AddEditAdminForm, but with a valid AdminPins object, signaling that an existing AdminPin is being deleted or reactivated
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

        //Blocks the "alt F4" capability so that the user cannot close the program while a process is running
        private void ChangePinForm_FormClosing(object sender, FormClosingEventArgs e)
        {            
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }
    }
}
