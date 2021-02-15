using BackendlessAPI;
using BackendlessAPI.Async;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;
using TableFindBackend.Output;

namespace TableFindBackend.Forms
{
    //This form is used to change some of the settings of the program and get access to more features
    public partial class EditRestaurantForm : Form
    {
        private MainForm _master; //An instance of the MainForm gets passed along so that is can be manipulated from this form
        //This form is created with an instance of the MainForm
        public EditRestaurantForm(MainForm master) 
        {
            InitializeComponent();
            _master = master;
            //All textboxes are filled in with valid information
            tbxName.Text = OwnerStorage.ThisRestaurant.Name;
            tbxLocation.Text = OwnerStorage.ThisRestaurant.LocationString;
            tbxContactNumber.Text = OwnerStorage.ThisRestaurant.ContactNumber;
            dtpOpen.Value = OwnerStorage.ThisRestaurant.Open;
            dtpClose.Value = OwnerStorage.ThisRestaurant.Close;

            //Ensures that the Reset to Defaults button is only enabled if there is a layout to replace
            if (File.Exists(@"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl"))
                btnDefault.Enabled = true;
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
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnClose.Enabled = false;
                btnBrowseLayout.Enabled = false;
                btnPrint.Enabled = false;
                btnDeactivate.Enabled = false;
                btnDefault.Enabled = false;
                pnlRestaurantDetails.Enabled = false;
                pnlRestaurantTimes.Enabled = false;
            }
            else
            {
                pbxLoading.Visible = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnClose.Enabled = true;
                btnBrowseLayout.Enabled = true;
                btnPrint.Enabled = true;
                btnDeactivate.Enabled = true;
                btnDefault.Enabled = true;
                pnlRestaurantDetails.Enabled = true;
                pnlRestaurantTimes.Enabled = true;
            }
        }

        //Saves the changes made by the user, as well as checking if the layout has been modified or cleared
        private void btnSave_Click(object sender, EventArgs e)
        {
            string file = ofdLayoutBrowse.FileName;
            ShowLoading(true);
            try
            {
                //The directory has to be created first. If it does not already exist, it's created here
                if (File.Exists("layouts") != true)
                    Directory.CreateDirectory("layouts");

                //Layout was chosen or left as is
                if (file.Equals("") != true)   
                {
                    string text = File.ReadAllText(file);
                    lblLayout.Text = ofdLayoutBrowse.FileName;
                    //If the file already exists, it has to be deleted first so that it can be replaced with the new one
                    if (File.Exists(@"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl"))
                    {
                        _master.DisableLayoutImage();
                        File.Delete(@"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl");
                    }
                    //The new layout image gets copied to the program files
                    File.Copy(ofdLayoutBrowse.FileName, @"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl");

                    //Log and document the event
                    OwnerStorage.FileWriter.WriteLineToFile("User changed the restaurant layout image", true);
                    OwnerStorage.LogInfo.Add("User changed the restaurant layout image");
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                }
                //Layout was reset or deleted
                else
                {
                    //Calls a method on the MainForm which disables the image so that the image can be deleted fromm the program files
                    _master.DisableLayoutImage();
                    //deletes the image file
                    File.Delete(@"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl");
                    //documents the events
                    OwnerStorage.FileWriter.WriteLineToFile("User cleared the restaurant layout image", true);
                    OwnerStorage.LogInfo.Add("User cleared the restaurant layout image");
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                }
            }
            //Something went wrong, so an error message gets displayed
            catch (IOException ex)
            {
                MessageBox.Show(this, "Error: " + ex.Message);
            }

            //Saving the rest of the restaurant information
            OwnerStorage.ThisRestaurant.ContactNumber = tbxContactNumber.Text;
            OwnerStorage.ThisRestaurant.Name = tbxName.Text;
            OwnerStorage.ThisRestaurant.LocationString = tbxLocation.Text;
            OwnerStorage.ThisRestaurant.Open = dtpOpen.Value;
            OwnerStorage.ThisRestaurant.Close = dtpClose.Value;

            AsyncCallback<Restaurant> updateObjectCallback = new AsyncCallback<Restaurant>(
            savedRestaurant =>
            {
                //Success the object is now updated. the form will now close
                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    ShowLoading(false);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }));
                //Log the event
                OwnerStorage.FileWriter.WriteLineToFile("User made changes to the restaurant settings", true);
                OwnerStorage.LogInfo.Add("User made changes to the restaurant settings");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

            },
            error =>
            {
                //Something went wrong, an error message will now display
                //Runs visual aspects on a new thread because you cannot alter visual aspects on any thread other than the GUI thread
                Invoke(new Action(() =>
                {
                    ShowLoading(false);
                    MessageBox.Show(error.Message.ToString());
                }));
            });

            AsyncCallback<Restaurant> saveObjectCallback = new AsyncCallback<Restaurant>(
              savedRestaurant =>
              {
                  //The object has to be saved first before it can be updated
                  Backendless.Persistence.Of<Restaurant>().Save(savedRestaurant, updateObjectCallback);
              },
              error =>
              {
                  //Something went wrong, an error message will now display
                  //Runs visual aspects on a new thread because you can not alter visual aspects on any thread other than the GUI thread
                  Invoke(new Action(() =>
                  {
                      ShowLoading(false);
                      MessageBox.Show(error.Message.ToString());
                  }));
              }
            );

            Backendless.Persistence.Of<Restaurant>().Save(OwnerStorage.ThisRestaurant, saveObjectCallback);

        }

        #region feature declared obsolete
        //feature declared obsolete / unpractical
        //private void btnBrowse_Click(object sender, EventArgs e)
        //{
        //    long size = -1;
        //    ofdMenuBrowse.Filter = "pdf files (*.pdf)|*.pdf|Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
        //    DialogResult result = ofdMenuBrowse.ShowDialog(); // Show the dialog.
        //    if (result == DialogResult.OK) // Test result.
        //    {
        //        string file = ofdMenuBrowse.FileName;
        //        try
        //        {
        //            string text = File.ReadAllText(file);
        //            tbxMenu.Text = ofdMenuBrowse.FileName;
        //            size = text.Length / 1024;
        //            lblSize.Text = "File size: " + size+ "KB";
        //            btnUpload.Enabled = true;
        //        }
        //        catch (IOException)
        //        {
        //        }
        //    }
        //}

        //private void btnUpload_Click(object sender, EventArgs e)
        //{
        //    if (tbxMenu.Text!="")
        //    { 
        //    pbxLoading.Visible = true;
        //        btnSave.Enabled = false;



        //        lblSize.Text = "Removing Existing File...";
        //        AsyncCallback<object> deleteCallback = new AsyncCallback<object>(
        //        result =>
        //        {
        //            Invoke(new Action(() =>
        //            {
        //                lblSize.Text = "Uploading...";
        //            }));
        //            AsyncCallback<BackendlessAPI.File.BackendlessFile> callback = new AsyncCallback<BackendlessAPI.File.BackendlessFile>(
        //            success =>
        //            {
        //                OwnerStorage.ThisRestaurant.MenuLink = success.FileURL;
        //                Invoke(new Action(() =>
        //                {
        //                    OwnerStorage.FileWriter.WriteLineToFile("User Uploaded new menu pdf", true);
        //                    OwnerStorage.LogInfo.Add("User Uploaded new menu pdf");
        //                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

        //                    pbxLoading.Visible = false;
        //                    lblSize.Text = "Upload completed";
        //                    btnSave.Enabled = true;
        //                }));
        //            },

        //            fault =>
        //            {
        //                Invoke(new Action(() =>
        //                {
        //                    OwnerStorage.FileWriter.WriteLineToFile("Menu Upload Failed", true);

        //                    pbxLoading.Visible = false;
        //                    MessageBox.Show(this, "Error: " + fault.Message.ToString());
        //                    lblSize.Text = "Upload failed";
        //                    btnSave.Enabled = true;
        //                }));
        //            });

        //            FileStream fs = new FileStream(tbxMenu.Text, FileMode.Open, FileAccess.Read);
        //            BackendlessAPI.Backendless.Files.Upload(fs, "Menu/" + OwnerStorage.ThisRestaurant.objectId, callback);
        //        },

        //        fault =>
        //        {
        //            Invoke(new Action(() =>
        //            {
        //                lblSize.Text = "Uploading...";
        //            }));
        //            AsyncCallback<BackendlessAPI.File.BackendlessFile> callback = new AsyncCallback<BackendlessAPI.File.BackendlessFile>(
        //            success =>
        //            {
        //                OwnerStorage.ThisRestaurant.MenuLink = success.FileURL;
        //                Invoke(new Action(() =>
        //                {
        //                    OwnerStorage.FileWriter.WriteLineToFile("User Uploaded new menu pdf", true);
        //                    OwnerStorage.LogInfo.Add("User Uploaded new menu pdf");
        //                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

        //                    pbxLoading.Visible = false;
        //                    lblSize.Text = "Upload completed";
        //                    btnSave.Enabled = true;
        //                }));
        //            },

        //            uploadFault =>
        //            {
        //                Invoke(new Action(() =>
        //                {
        //                    OwnerStorage.FileWriter.WriteLineToFile("Menu Upload Failed", true);

        //                    pbxLoading.Visible = false;
        //                    MessageBox.Show(this, "Error: " + uploadFault.Message.ToString());
        //                    lblSize.Text = "Upload failed";
        //                    btnSave.Enabled = true;
        //                }));
        //            });

        //            FileStream fs = new FileStream(tbxMenu.Text, FileMode.Open, FileAccess.Read);
        //            BackendlessAPI.Backendless.Files.Upload(fs, "Menu/" + OwnerStorage.ThisRestaurant.objectId, callback);
        //        });

        //        BackendlessAPI.Backendless.Files.Remove("Menu/"+OwnerStorage.ThisRestaurant.objectId, deleteCallback);

        //        }
        //        else
        //        {
        //            MessageBox.Show(this, "Please browse for a file to upload first");
        //        }
        //}
        #endregion

        //Method which displays an OpenFileDialog form in which the user can locate a layout image of format either BMP, JPG or PNG
        private void btnBrowseLayout_Click(object sender, EventArgs e)
        { 
            //If the user is advanced enough he/she can even locate the .tbl files which is what happens after a regular image is selected by the TableFind Program
            ofdLayoutBrowse.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|TableFindBackend Layout files (*.tbl)|*.tbl";
            //Show the dialog
            DialogResult result = ofdLayoutBrowse.ShowDialog();

            //Test result
            if (result == DialogResult.OK) 
            {
                //Enables and clears the appropriate controls
                string file = ofdLayoutBrowse.FileName;
                lblLayout.Text = ofdLayoutBrowse.FileName;
                btnDefault.Enabled = true;
            }
        }

        //Opens the SystemReportForm where the user can generate documents containing information about the report of the day
        private void btnPrint_Click(object sender, EventArgs e)
        {
            SystemReportForm sysReportForm = new SystemReportForm();
            sysReportForm.ShowDialog();
        }

        //This method will clear the layout image of the restaurant on the MainForm
        private void btnDefault_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you would like to reset the restaurant layout to a blank canvas?", "Reset Restaurant layout", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                lblLayout.Text = ofdLayoutBrowse.FileName;
                lblLayout.Text = "Change restaurant layout image";
                btnDefault.Enabled = false;
                ofdLayoutBrowse.FileName = null; //As long as this is empty, the layout will be reset once the user clicks 'save'
            }
        }

        //Button used to close the form if the user wishes to not save changes made
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Shows the ConfirmRestaurantDeactivationForm, which will allow the user to deactivate his/her restaurant
        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            ConfirmRestaurantDeactivationForm form = new ConfirmRestaurantDeactivationForm();
            form.ShowDialog();
        }

        private void EditRestaurantForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Blocks the "alt F4" capability so that the user cannot close the program while a process is running
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing && pbxLoading.Visible == true)
            {
                e.Cancel = true;
            }
        }

        //Sets the coulors in the panel to fit with the theme of the functions on the panel
        private void pnlDangerZone_Paint(object sender, PaintEventArgs e)
        {
            Color color = Color.Red;
            Panel panel = (Panel)sender;
            float width = (float)4.0;
            Pen pen = new Pen(color, width);
            e.Graphics.DrawLine(pen, 0, 0, 0, panel.Height - 0);
            e.Graphics.DrawLine(pen, 0, 0, panel.Width - 0, 0);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, 0, panel.Height - 1);
            e.Graphics.DrawLine(pen, panel.Width - 1, panel.Height - 1, panel.Width - 1, 0);
        }

        private void btnChangePassoword_Click(object sender, EventArgs e)
        {
            ChangePasswordForm passChangeForm = new ChangePasswordForm();
            passChangeForm.ShowDialog();
        }
    }
}
