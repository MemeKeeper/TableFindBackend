﻿using BackendlessAPI.Async;
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

            tbxName.Text = OwnerStorage.ThisRestaurant.name;
            tbxLocation.Text = OwnerStorage.ThisRestaurant.locationString;
            tbxContactNumber.Text = OwnerStorage.ThisRestaurant.contactNumber;

            if (File.Exists(@"layouts\" + OwnerStorage.ThisRestaurant.name + "_" + OwnerStorage.ThisRestaurant.locationString + "_layout.tbl"))
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string file = ofdBrowse.FileName;
            try
            {
                if (File.Exists("layouts") != true)
                    Directory.CreateDirectory("layouts");

                if (file != "")   //    <---Layout was chosen or left as is
                {
                    string text = File.ReadAllText(file);
                    lblLayout.Text = ofdBrowse.FileName;
                    if (File.Exists(@"layouts\" + OwnerStorage.ThisRestaurant.name + "_" + OwnerStorage.ThisRestaurant.locationString + "_layout.tbl"))
                        try
                        {
                            _master.DisableLayoutImage();
                            File.Delete(@"layouts\" + OwnerStorage.ThisRestaurant.name + "_" + OwnerStorage.ThisRestaurant.locationString + "_layout.tbl");
                        }
                        catch (IOException)
                        { }
                    File.Copy(ofdBrowse.FileName, @"layouts\" + OwnerStorage.ThisRestaurant.name + "_" + OwnerStorage.ThisRestaurant.locationString + "_layout.tbl");
                }
                else
                {
                    //<-----------Layout was reset or deleted.
                    try
                    {
                        _master.DisableLayoutImage();
                        File.Delete(@"layouts\" + OwnerStorage.ThisRestaurant.name + "_" + OwnerStorage.ThisRestaurant.locationString + "_layout.tbl");                        
                    }
                    catch(IOException)
                    {

                    }
                }
            }
            catch (IOException)
            {
            }

            OwnerStorage.ThisRestaurant.contactNumber = tbxContactNumber.Text;
            OwnerStorage.ThisRestaurant.name = tbxName.Text;
            OwnerStorage.ThisRestaurant.locationString = tbxLocation.Text;                 
            OwnerStorage.ThisRestaurant.EditRestaurant();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            long size = -1;
            DialogResult result = ofdBrowse.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = ofdBrowse.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    tbxMenu.Text = ofdBrowse.FileName;
                    size = text.Length / 1024;
                    lblSize.Text = "File size: " + size+ "KB";
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
            
                lblSize.Text = "Uploading...";
                AsyncCallback<BackendlessAPI.File.BackendlessFile> callback = new AsyncCallback<BackendlessAPI.File.BackendlessFile>(
                result =>
                {
                    OwnerStorage.ThisRestaurant.menuLink = result.FileURL;
                    Invoke(new Action(() =>
                    {
                        OwnerStorage.FileWriter.WriteLineToFile("User Uploaded new menu pdf", true);


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
                        MessageBox.Show(this, "Error: " + fault.Message);
                        lblSize.Text = "Upload failed";
                        btnSave.Enabled = true;
                    }));
                });

                FileStream fs = new FileStream(tbxMenu.Text, FileMode.Open, FileAccess.Read);
                BackendlessAPI.Backendless.Files.Upload(fs, "myfiles", callback);
            }
            else
            {
                MessageBox.Show(this, "Please browse for a file to upload first");
            }
        }

        private void btnBrowseLayout_Click(object sender, EventArgs e)
        {
            DialogResult result = ofdBrowse.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = ofdBrowse.FileName;
                lblLayout.Text = ofdBrowse.FileName;
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
                    lblLayout.Text = ofdBrowse.FileName;
                    try
                    {
                        lblLayout.Text = "Change restaurant layout image";
                        btnDefault.Enabled = false;
                        ofdBrowse.FileName = null;

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
    }
}
