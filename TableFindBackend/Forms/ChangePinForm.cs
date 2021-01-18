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

namespace TableFindBackend.Forms
{
    public partial class ChangePinForm : Form
    {
        public ChangePinForm()
        {
            InitializeComponent();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(tbxPin1.Text.Equals(tbxPinConfirm.Text))
                {
                if (File.Exists("TableFindMan") == true)
                {
                    File.Delete("TableFindMan");  //<--Deletes file to prevent duplications                
                }
                try
                {
                    StreamWriter sw = new StreamWriter("TableFindMan");
                    sw.WriteLine("<><><><><><><><><><><><><><><>");
                    sw.WriteLine(tbxPinConfirm.Text);
                    sw.WriteLine("<><><><><><><><><><><><><><><>");
                    sw.Close();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show(this, "The Two PINs you have entered does not match.");
                tbxPinConfirm.Text = "";
                tbxPin1.Text = "";
            }           
        }

        private void tbxPinConfirm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbxPin1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
