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
using TableFindBackend.Output;

namespace TableFindBackend.Forms
{

    public partial class EditRestaurantForm : Form
    {
        private MainForm _master;
        public EditRestaurantForm(MainForm master)
        {
            _master = master;
            InitializeComponent();

            tbxName.Text = OwnerStorage.ThisRestaurant.Name;
            tbxLocation.Text = OwnerStorage.ThisRestaurant.LocationString;
            tbxContactNumber.Text = OwnerStorage.ThisRestaurant.ContactNumber;
            tbxMenu.Text = OwnerStorage.ThisRestaurant.MenuLink;
            dtpOpen.Value = OwnerStorage.ThisRestaurant.Open;
            dtpClose.Value = OwnerStorage.ThisRestaurant.Close;

            if (File.Exists(@"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl"))
                try
                {
                    btnDefault.Enabled = true;                 
                }
                catch (IOException)
                { 
                }
                
    }

        private void lblX_Click(object sender, EventArgs e)
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
            if(toggle ==true)
            {
                pbxLoading.Visible = true;
                btnBrowse.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnClose.Enabled = false;
                btnUpload.Enabled = false;
                btnBrowseLayout.Enabled = false;
                btnPrint.Enabled = false;
                btnDeactivate.Enabled = false;
                btnDefault.Enabled = false;

            }
            else
            {
                pbxLoading.Visible = false;
                btnBrowse.Enabled = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnClose.Enabled = true;
                btnUpload.Enabled = true;
                btnBrowseLayout.Enabled = true;
                btnPrint.Enabled = true;
                btnDeactivate.Enabled = true;
                btnDefault.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string file = ofdLayoutBrowse.FileName;
            ShowLoading(true);
            try
            {
                if (File.Exists("layouts") != true)
                    Directory.CreateDirectory("layouts");

                if (file.Equals("")!=true )   //    <---Layout was chosen or left as is
                {
                    string text = File.ReadAllText(file);
                    lblLayout.Text = ofdLayoutBrowse.FileName;
                    if (File.Exists(@"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl"))
                        try
                        {
                            _master.DisableLayoutImage();
                            File.Delete(@"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl");
                        }
                        catch (IOException)
                        { }
                    File.Copy(ofdLayoutBrowse.FileName, @"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl");
                    OwnerStorage.FileWriter.WriteLineToFile("User changed the restaurant layout image", true);
                    OwnerStorage.LogInfo.Add("User changed the restaurant layout image");
                    OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                }
                else
                {
                    //<-----------Layout was reset or deleted.
                    try
                    {
                        _master.DisableLayoutImage();
                        File.Delete(@"layouts\" + OwnerStorage.ThisRestaurant.objectId + "_" + OwnerStorage.ThisRestaurant.LocationString + "_layout.tbl");
                        OwnerStorage.FileWriter.WriteLineToFile("User cleared the restaurant layout image", true);
                        OwnerStorage.LogInfo.Add("User cleared the restaurant layout image");
                        OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));
                    }
                    catch(IOException)
                    {

                    }
                }
            }
            catch (IOException)
            {
            }

            OwnerStorage.ThisRestaurant.ContactNumber = tbxContactNumber.Text;
            OwnerStorage.ThisRestaurant.Name = tbxName.Text;
            OwnerStorage.ThisRestaurant.LocationString = tbxLocation.Text;
            OwnerStorage.ThisRestaurant.Open = dtpOpen.Value;
            OwnerStorage.ThisRestaurant.Close = dtpClose.Value;

            AsyncCallback<Restaurant> updateObjectCallback = new AsyncCallback<Restaurant>(
            savedRestaurant =>
            {
                Invoke(new Action(() =>
                {
                    ShowLoading(false);
                    DialogResult = DialogResult.OK;
                    this.Close();

                }));
                OwnerStorage.FileWriter.WriteLineToFile("User made changes to the restaurant settings", true);
                OwnerStorage.LogInfo.Add("User made changes to the restaurant settings");
                OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

            },
            error =>
            {
                Invoke(new Action(() =>
                {
                    ShowLoading(false);
                    MessageBox.Show(error.Message.ToString());
                }));
            });

            AsyncCallback<Restaurant> saveObjectCallback = new AsyncCallback<Restaurant>(
              savedRestaurant =>
              {
                  Backendless.Persistence.Of<Restaurant>().Save(savedRestaurant, updateObjectCallback);
              },
              error =>
              {
                  Invoke(new Action(() =>
                  {
                      ShowLoading(false);
                      MessageBox.Show(error.Message.ToString());
                  }));
              }
            );

            Backendless.Persistence.Of<Restaurant>().Save(OwnerStorage.ThisRestaurant, saveObjectCallback);

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            long size = -1;
            ofdMenuBrowse.Filter = "pdf files (*.pdf)|*.pdf|Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            DialogResult result = ofdMenuBrowse.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = ofdMenuBrowse.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    tbxMenu.Text = ofdMenuBrowse.FileName;
                    size = text.Length / 1024;
                    lblSize.Text = "File size: " + size+ "KB";
                    btnUpload.Enabled = true;
                }
                catch (IOException)
                {
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (tbxMenu.Text!="")
            { 
            pbxLoading.Visible = true;
                btnSave.Enabled = false;
            


                lblSize.Text = "Removing Existing File...";
                AsyncCallback<object> deleteCallback = new AsyncCallback<object>(
                result =>
                {
                    Invoke(new Action(() =>
                    {
                        lblSize.Text = "Uploading...";
                    }));
                    AsyncCallback<BackendlessAPI.File.BackendlessFile> callback = new AsyncCallback<BackendlessAPI.File.BackendlessFile>(
                    success =>
                    {
                        OwnerStorage.ThisRestaurant.MenuLink = success.FileURL;
                        Invoke(new Action(() =>
                        {
                            OwnerStorage.FileWriter.WriteLineToFile("User Uploaded new menu pdf", true);
                            OwnerStorage.LogInfo.Add("User Uploaded new menu pdf");
                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

                            pbxLoading.Visible = false;
                            lblSize.Text = "Upload completed";
                            btnSave.Enabled = true;
                        }));
                    },

                    fault =>
                    {
                        Invoke(new Action(() =>
                        {
                            OwnerStorage.FileWriter.WriteLineToFile("Menu Upload Failed", true);

                            pbxLoading.Visible = false;
                            MessageBox.Show(this, "Error: " + fault.Message.ToString());
                            lblSize.Text = "Upload failed";
                            btnSave.Enabled = true;
                        }));
                    });

                    FileStream fs = new FileStream(tbxMenu.Text, FileMode.Open, FileAccess.Read);
                    BackendlessAPI.Backendless.Files.Upload(fs, "Menu/" + OwnerStorage.ThisRestaurant.objectId, callback);
                },

                fault =>
                {
                    Invoke(new Action(() =>
                    {
                        lblSize.Text = "Uploading...";
                    }));
                    AsyncCallback<BackendlessAPI.File.BackendlessFile> callback = new AsyncCallback<BackendlessAPI.File.BackendlessFile>(
                    success =>
                    {
                        OwnerStorage.ThisRestaurant.MenuLink = success.FileURL;
                        Invoke(new Action(() =>
                        {
                            OwnerStorage.FileWriter.WriteLineToFile("User Uploaded new menu pdf", true);
                            OwnerStorage.LogInfo.Add("User Uploaded new menu pdf");
                            OwnerStorage.LogTimes.Add(System.DateTime.Now.ToString("HH:mm:ss"));

                            pbxLoading.Visible = false;
                            lblSize.Text = "Upload completed";
                            btnSave.Enabled = true;
                        }));
                    },

                    uploadFault =>
                    {
                        Invoke(new Action(() =>
                        {
                            OwnerStorage.FileWriter.WriteLineToFile("Menu Upload Failed", true);

                            pbxLoading.Visible = false;
                            MessageBox.Show(this, "Error: " + uploadFault.Message.ToString());
                            lblSize.Text = "Upload failed";
                            btnSave.Enabled = true;
                        }));
                    });

                    FileStream fs = new FileStream(tbxMenu.Text, FileMode.Open, FileAccess.Read);
                    BackendlessAPI.Backendless.Files.Upload(fs, "Menu/" + OwnerStorage.ThisRestaurant.objectId, callback);
                });

                BackendlessAPI.Backendless.Files.Remove("Menu/"+OwnerStorage.ThisRestaurant.objectId, deleteCallback);
                
                }
                else
                {
                    MessageBox.Show(this, "Please browse for a file to upload first");
                }
        }

        private void btnBrowseLayout_Click(object sender, EventArgs e)
        {
            ofdLayoutBrowse.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|TableFindBackend Layout files (*.tbl)|*.tbl";
            DialogResult result = ofdLayoutBrowse.ShowDialog(); // Show the dialog.
            
            if (result == DialogResult.OK) // Test result.
            {
                string file = ofdLayoutBrowse.FileName;
                lblLayout.Text = ofdLayoutBrowse.FileName;
                btnDefault.Enabled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SystemReportForm printerPage = new SystemReportForm();
            printerPage.ShowDialog();
        }

        private void btnApplyImage_Click(object sender, EventArgs e)
        {
          
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you would like to reset the restaurant layout to a blank canvas?", "Reset Restaurant layout", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    lblLayout.Text = ofdLayoutBrowse.FileName;
                    try
                    {
                        lblLayout.Text = "Change restaurant layout image";
                        btnDefault.Enabled = false;
                        ofdLayoutBrowse.FileName = null;

                    }
                    catch (IOException)
                    { }
                }
                catch (IOException)
                {
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dtpOpen_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpClose_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
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

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            ConfirmRestaurantDeactivationForm form = new ConfirmRestaurantDeactivationForm();
            form.ShowDialog();
        }
    }
}
