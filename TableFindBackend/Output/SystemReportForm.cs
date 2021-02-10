using System;
using Syncfusion.XlsIO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableFindBackend.Global_Variables;
using TableFindBackend.Models;

using System.IO;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using Document = Spire.Doc.Document;

namespace TableFindBackend.Output
{
    public partial class SystemReportForm : Form
    {
        public SystemReportForm()
        {
            InitializeComponent();
            foreach (RestaurantTable table in OwnerStorage.RestaurantTables)
            {
                List<Reservation> tempList = new List<Reservation>();
                foreach (Reservation reservation in OwnerStorage.ActiveReservations)
                {
                    if (reservation.TableId == table.objectId)
                        tempList.Add(reservation);

                }
                if (tempList.Count!=0)
                {
                    AddRestaurantReservationTable(table, tempList);
                }
            }

            dgvTables.DataSource = OwnerStorage.RestaurantTables; // <-- displays restaurant table info (restaurant table tab)
            //rtbLog.Lines = OwnerStorage.Log.ToArray();

            //displays system log info (system log tab)
            for (int i = 0;i<OwnerStorage.LogInfo.Count;i++)
            {
                if (OwnerStorage.LogTimes[i]=="blank")
                {
                    rtbLog.Text += OwnerStorage.LogInfo[i]+"\n";
                }
                else
                {
                    rtbLog.Text += OwnerStorage.LogInfo[i] + "\t" + OwnerStorage.LogTimes[i] + "\n";
                }
            }
            PopulateAdminReport();
        }

        private void PopulateAdminReport()
        {
            foreach (AdminPins a in OwnerStorage.ListOfAdmins)
            {
                List<string> tempList = new List<string>();
                foreach (string[] s in OwnerStorage.AdminLog)
                {
                    if (s[0] == a.objectId)
                    {
                        tempList.Add(s[1]);

                    }
                }
                AddAdminUserLoginTable(tempList, a);
            }
        }

        
        private void ToggleLoading(bool toggle)
        {
                if (toggle == true)
                {
                    pbxLoading.Visible = true;
                    btnExcel.Enabled = false;
                    btnWord.Enabled = false;
                    btnPDF.Enabled = false;
                }
                else
                {
                    pbxLoading.Visible = false;
                    btnExcel.Enabled = true;
                    btnWord.Enabled = true;
                    btnPDF.Enabled = true;
                }            
        }
        private void AddAdminUserLoginTable(List<string> log, AdminPins a)
        {
            Panel backPanel = new Panel();
            backPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            backPanel.Width = flpAdminLog.Width - 10;
            backPanel.Height = 50;
            backPanel.BackColor = SystemColors.ControlDark;
            Label titleLabel = new Label();
            titleLabel.Font = new System.Drawing.Font("Century Gothic", 10);
            titleLabel.Text = a.UserName;
            titleLabel.AutoSize = true;
            titleLabel.Location = new System.Drawing.Point(10, 10);
            backPanel.Controls.Add(titleLabel);
            System.Windows.Forms.TextBox tbxAmount = new System.Windows.Forms.TextBox();
            tbxAmount.Width = 180;
            tbxAmount.ReadOnly = true;
            tbxAmount.Font = new System.Drawing.Font("Century Gothic", 10);
            tbxAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbxAmount.BackColor = SystemColors.ControlDark;
            tbxAmount.Location = new System.Drawing.Point(150, 10);
            backPanel.Controls.Add(tbxAmount);
            if (log.Count == 1)
            {
                tbxAmount.Text = log.Count.ToString() + " login during this session";
            }
            else
            {
                tbxAmount.Text = log.Count.ToString() + " logins during this session";                               
            }
            if(log.Count !=0)
            {
                flpAdminLog.Controls.Add(backPanel);
                DataGridView dataGridView = new DataGridView();
                dataGridView.Height = 22;
                dataGridView.Columns.Add("recordedTime", "Recorded Time");
                dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                backPanel.Height += 23;
                foreach (string s in log)
                {
                    dataGridView.Rows.Add(s);
                    dataGridView.Height += 22;
                    backPanel.Height += 22;
                }
                dataGridView.Width = 150;
                dataGridView.Location = new System.Drawing.Point(150, 40);
                dataGridView.AllowUserToAddRows = false;
                dataGridView.AllowUserToOrderColumns = false;
                dataGridView.AllowUserToAddRows = false;
                dataGridView.AllowUserToResizeRows = false;
                dataGridView.AllowUserToDeleteRows = false;
                dataGridView.RowHeadersVisible = false;
                backPanel.Controls.Add(dataGridView);
            }            
        }

        //displays reservations under each table (right-hand side panel)
        private void AddRestaurantReservationTable(RestaurantTable table,List<Reservation> list)
        {

            Panel backPanel = new Panel();
            backPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            backPanel.Width = flpReservationTables.Width-10;
            backPanel.Height = 80;
            Label titleLabel = new Label();
            titleLabel.Font=new System.Drawing.Font("Century Gothic", 10);
            titleLabel.Text = table.Name;
            titleLabel.AutoSize = true;
            titleLabel.Location = new System.Drawing.Point(10, 10);
            backPanel.Controls.Add(titleLabel);             
            DataGridView newView = new DataGridView();
            newView.Location = new System.Drawing.Point(5, 40);
            newView.ReadOnly = true;
            newView.AllowUserToAddRows = false;
            newView.AllowUserToDeleteRows = false;
            newView.Width = 460;
            newView.Height = 27;
            newView.Columns.Add("name","Name for Reservation");
            newView.Columns.Add("takenFrom", "Taken From");
            newView.Columns.Add("takenTo", "Taken To");
            newView.Columns.Add("contactNumber", "Contact number");
            foreach (Reservation reservation in list)
            {
                newView.Rows.Add(reservation.Name, reservation.TakenFrom, reservation.TakenTo, reservation.Number);
                newView.Height += 22;
                backPanel.Height += 22;
            }
            backPanel.Controls.Add(newView);

            flpReservationTables.Controls.Add(backPanel);
        }

        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            ToggleLoading(true);
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                app.SheetsInNewWorkbook = 5;
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet wsTables = null;
                Microsoft.Office.Interop.Excel.Range range;

                #region //restaurant Tables
                //Setup for Sheet1
                wsTables = workbook.Sheets["Sheet1"];
                wsTables = workbook.ActiveSheet;
                wsTables.Name = "Restaurant Tables";
                range = wsTables.get_Range("A1","D1");
                range.Font.Color = System.Drawing.Color.FromName("White");
                for (int i = 1; i < dgvTables.Columns.Count + 1; i++)
                {                    
                    wsTables.Cells[1, i] = dgvTables.Columns[i - 1].HeaderText;
                    wsTables.Cells[1, i].Interior.Color = System.Drawing.Color.FromName("Silver");                    
                }
                for (int i = 0; i < dgvTables.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvTables.Columns.Count; j++)
                    {
                        wsTables.Cells[i + 2, j + 1] = dgvTables.Rows[i].Cells[j].Value.ToString();

                    }
                }
                range = wsTables.get_Range("A1", "H1000");
                range.Rows.AutoFit();
                range.Columns.AutoFit();

                #endregion
                #region //System Log
                //Setup for Sheet2
                wsTables = workbook.Sheets["Sheet2"];
                wsTables.Name = "System Log";     
                wsTables.Cells[1, 1] = "System Events";
                wsTables.Cells[1, 2] = "Recorded Time";
                wsTables.Cells[1, 1].Interior.Color = System.Drawing.Color.FromName("Silver");
                wsTables.Cells[1, 2].Interior.Color = System.Drawing.Color.FromName("Silver");
                range = wsTables.get_Range("A1", "B1");
                range.Font.Color = System.Drawing.Color.FromName("White");

                for (int i = 0; i < OwnerStorage.LogInfo.Count; i++)
                {
                    wsTables.Cells[i + 2, 1] = OwnerStorage.LogInfo[i];
                    if (OwnerStorage.LogTimes[i] != "blank")
                    {
                        wsTables.Cells[i+2, 2] = OwnerStorage.LogTimes[i];
                    }
                }
                range = wsTables.get_Range("A1", "H1000");
                range.Rows.AutoFit();
                range.Columns.AutoFit();
                #endregion
                #region//Reservations
                //setup for Sheet3
                wsTables = workbook.Sheets["Sheet3"];
                wsTables.Name = "Reservations";
                int rowHeadingIndex = 0;
                for (int i = 0; i < OwnerStorage.RestaurantTables.Count;i++) 
                {
                    List<Reservation> tempList = new List<Reservation>();
                    foreach (Reservation reservation in OwnerStorage.ActiveReservations)
                    {
                        if (reservation.TableId == OwnerStorage.RestaurantTables[i].objectId)
                            tempList.Add(reservation);
                    }
                    if (tempList.Count != 0)
                    {
                        rowHeadingIndex += 1;

                        wsTables.Range[wsTables.Cells[rowHeadingIndex , 1], wsTables.Cells[rowHeadingIndex , 4]].Merge();
                        wsTables.Cells[rowHeadingIndex , 1] = OwnerStorage.RestaurantTables[i].Name;

                        wsTables.Cells[rowHeadingIndex , 1].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.get_Range("A" + (rowHeadingIndex + 1).ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenterAcrossSelection;

                        wsTables.Cells[rowHeadingIndex + 1, 1] ="Name";
                        wsTables.Cells[rowHeadingIndex + 1, 2] = "Date Taken From";
                        wsTables.Cells[rowHeadingIndex + 1, 3] = "Date Taken To";
                        wsTables.Cells[rowHeadingIndex + 1, 4] = "Contact Number";
                        wsTables.Cells[rowHeadingIndex + 1, 1].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.Cells[rowHeadingIndex + 1, 2].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.Cells[rowHeadingIndex + 1, 3].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.Cells[rowHeadingIndex + 1, 4].Interior.Color = System.Drawing.Color.FromName("Silver");
                        range = wsTables.get_Range("A"+ (rowHeadingIndex + 1).ToString(), "D" + (rowHeadingIndex + 1).ToString());
                        range.Font.Color = System.Drawing.Color.FromName("White");
                        for (int inner = 0; inner< tempList.Count; inner++)
                        {
                            wsTables.Cells[rowHeadingIndex + inner + 2, 1] = tempList[inner].Name;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 2] = tempList[inner].TakenFrom;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 3] = tempList[inner].TakenTo;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 4] = tempList[inner].Number;                            
                        }
                        rowHeadingIndex += tempList.Count+2;
                    }
                }
                range = wsTables.get_Range("A1", "H1000");
                range.Rows.AutoFit();
                range.Columns.AutoFit();
                #endregion
                #region//Expired Reservations
                //setup for sheet4
                wsTables = workbook.Sheets["Sheet4"];
                wsTables.Name = "Expired Reservations";
                rowHeadingIndex = 0;
                for (int i = 0; i < OwnerStorage.RestaurantTables.Count; i++)
                {
                    List<Reservation> tempList = new List<Reservation>();
                    foreach (Reservation reservation in OwnerStorage.PastReservations)
                    {
                        if (reservation.TableId == OwnerStorage.RestaurantTables[i].objectId)
                            tempList.Add(reservation);
                    }
                    if (tempList.Count != 0)
                    {
                        rowHeadingIndex += 1;

                        wsTables.Range[wsTables.Cells[rowHeadingIndex, 1], wsTables.Cells[rowHeadingIndex, 4]].Merge();
                        wsTables.Cells[rowHeadingIndex, 1] = OwnerStorage.RestaurantTables[i].Name;

                        wsTables.Cells[rowHeadingIndex, 1].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.get_Range("A" + (rowHeadingIndex + 1).ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenterAcrossSelection;

                        wsTables.Cells[rowHeadingIndex + 1, 1] = "Name";
                        wsTables.Cells[rowHeadingIndex + 1, 2] = "Date Taken From";
                        wsTables.Cells[rowHeadingIndex + 1, 3] = "Date Taken To";
                        wsTables.Cells[rowHeadingIndex + 1, 4] = "Contact Number";
                        wsTables.Cells[rowHeadingIndex + 1, 1].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.Cells[rowHeadingIndex + 1, 2].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.Cells[rowHeadingIndex + 1, 3].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.Cells[rowHeadingIndex + 1, 4].Interior.Color = System.Drawing.Color.FromName("Silver");
                        range = wsTables.get_Range("A" + (rowHeadingIndex + 1).ToString(), "D" + (rowHeadingIndex + 1).ToString());
                        range.Font.Color = System.Drawing.Color.FromName("White");
                        for (int inner = 0; inner < tempList.Count; inner++)
                        {
                            wsTables.Cells[rowHeadingIndex + inner + 2, 1] = tempList[inner].Name;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 2] = tempList[inner].TakenFrom;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 3] = tempList[inner].TakenTo;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 4] = tempList[inner].Number;
                        }
                        rowHeadingIndex += tempList.Count + 2;
                    }
                }
                range = wsTables.get_Range("A1", "H1000");
                range.Rows.AutoFit();
                range.Columns.AutoFit();
                #endregion
                #region//Admin Log

                //setup for sheet5 
                wsTables = workbook.Sheets["Sheet5"];
                wsTables.Name = "Admin Log";
                rowHeadingIndex = 0;
                for (int i = 0; i < OwnerStorage.ListOfAdmins.Count; i++)
                {
                    List<String> tempList = new List<String>();
                    for(int inner = 0; inner < OwnerStorage.AdminLog.Count; inner++)
                    {
                        if (OwnerStorage.AdminLog[inner][0] == OwnerStorage.ListOfAdmins[i].objectId)
                            tempList.Add(OwnerStorage.AdminLog[inner][1]);
                    }

                        rowHeadingIndex += 1;
                        wsTables.Cells[rowHeadingIndex, 1] = OwnerStorage.ListOfAdmins[i].UserName;
                        wsTables.Cells[rowHeadingIndex+1, 1] = "Total Times Logged in during session";
                        wsTables.Cells[rowHeadingIndex+1, 2] = tempList.Count;
                        wsTables.Cells[rowHeadingIndex, 1].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.Cells[rowHeadingIndex + 1, 1].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.Cells[rowHeadingIndex + 1, 2].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.get_Range("A" + (rowHeadingIndex + 1).ToString()).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenterAcrossSelection;
                        wsTables.Range[wsTables.Cells[rowHeadingIndex, 1], wsTables.Cells[rowHeadingIndex, 2]].Merge();


                    if (tempList.Count != 0)
                    {


                        wsTables.Cells[rowHeadingIndex + 2, 1] = "Recorded Time";
                        wsTables.Cells[rowHeadingIndex + 2, 1].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.Cells[rowHeadingIndex+2, 1].Interior.Color = System.Drawing.Color.FromName("Silver");
                        wsTables.Range[wsTables.Cells[rowHeadingIndex+2, 1], wsTables.Cells[rowHeadingIndex+2, 2]].Merge();
                        range = wsTables.get_Range("A" + (rowHeadingIndex + 1).ToString(), "D" + (rowHeadingIndex + 1).ToString());
                        range.Font.Color = System.Drawing.Color.FromName("White");
                        for (int inner = 0; inner < tempList.Count; inner++)
                        {
                            wsTables.Cells[rowHeadingIndex + inner + 3, 1] = tempList[inner];
                        }
                        rowHeadingIndex += tempList.Count + 3;
                    }
                    else
                    {
                        rowHeadingIndex += 2;
                    }
                }
                range = wsTables.get_Range("A1", "A1000");
                range.Rows.AutoFit();
                range.Columns.AutoFit();
                #endregion

               app.Visible = true;

                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), @"TableFindBackend\System Reports\"+OwnerStorage.ThisRestaurant.Name+@"\"+OwnerStorage.ThisRestaurant.objectId);
                if (File.Exists(path) != true)
                    Directory.CreateDirectory(path);

                workbook.SaveAs("TableFindBackend\\System Reports\\"+OwnerStorage.ThisRestaurant.Name+@"\"+OwnerStorage.ThisRestaurant.objectId + "\\SystemReport_"+System.DateTime.Now.ToString("dd-MM-yyyy")+ ".xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                ToggleLoading(false);

            }
            catch(Exception ex)
            {
                MessageBox.Show("There was an error generating the workbook: " + ex.Message.ToString()) ;
                ToggleLoading(false);
            }

        }
        private void GenerateWordDoc(Boolean word)
        {

            try
            {
                //Create an instance for word app  
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                winword.ShowAnimation = false;
                //Set status for word application is to be visible or not.  
                winword.Visible = false;
                //Create a missing variable for missing value  
                object missing = System.Reflection.Missing.Value;

                //Create a new document  
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                //Add header into the document  
                foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                {
                    //Get the header range and add the header details.  
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
                    headerRange.Font.Size = 10;
                    headerRange.Text = "TableFindBackend System Report for " + OwnerStorage.ThisRestaurant.Name;
                }

                //Add the footers into the document  
                foreach (Microsoft.Office.Interop.Word.Section wordSection in document.Sections)
                {
                    //Get the footer range and add the footer details.  
                    Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
                    footerRange.Font.Size = 10;
                    footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    footerRange.Text = "System Report for " + System.DateTime.Now.ToString("D") + " as captured at " + System.DateTime.Now.ToString("t");
                }

                //possibly add logo here
                Range docRange = document.Range();
                var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                string filePath = Path.Combine(projectPath, "Resources\\Logo_small.png");

                Word.Paragraph imageParagraph = document.Content.Paragraphs.Add(ref missing);
                imageParagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                imageParagraph.Range.InlineShapes.AddPicture(filePath);
                
                //System Log output

                //Add paragraph with Heading 1 style  
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                object styleHeading1 = "Heading 1";
                para1.Range.set_Style(ref styleHeading1);
                para1.Range.Font.Bold = 1;
                para1.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                para1.Range.Text = "System Log";
                para1.Range.InsertParagraphAfter();

                #region
                Microsoft.Office.Interop.Word.Table firstTable = document.Tables.Add(para1.Range, OwnerStorage.LogInfo.Count + 1, 2, ref missing, ref missing);
                firstTable.Borders.Enable = 1;
                firstTable.Cell(1, 1).Range.Text = "Recorded Event";
                firstTable.Cell(1, 2).Range.Text = "Recorded Time";

                firstTable.Rows[1].Range.Font.Bold = 1;
                firstTable.Rows[1].Range.Font.Name = "verdana";
                firstTable.Rows[1].Range.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                firstTable.Rows[1].Range.Font.Size = 10;
                firstTable.Rows[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                for (int i = 0; i < OwnerStorage.LogInfo.Count; i++)
                {
                    firstTable.Cell(i + 2, 1).Range.Text = OwnerStorage.LogInfo[i].ToString();
                    firstTable.Cell(i + 2, 2).Range.Text = OwnerStorage.LogTimes[i].ToString();
                }

                Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);

                #endregion

                //Admin Log output  
                Microsoft.Office.Interop.Word.Paragraph para3 = document.Content.Paragraphs.Add(ref missing);
                para3.Range.set_Style(ref styleHeading1);
                para3.Range.Font.Bold = 1;
                para3.Range.Text = "Admin Log";
                para3.Range.InsertParagraphAfter();

                #region
                Microsoft.Office.Interop.Word.Table adminTable = document.Tables.Add(para3.Range, OwnerStorage.ListOfAdmins.Count + 1, 3, ref missing, ref missing);
                adminTable.Borders.Enable = 1;
                adminTable.Cell(1, 1).Range.Text = "Admin User";
                adminTable.Cell(1, 2).Range.Text = "Login Count";
                adminTable.Cell(1, 3).Range.Text = "Login Times";

                adminTable.Rows[1].Range.Font.Bold = 1;
                adminTable.Rows[1].Range.Font.Name = "verdana";
                adminTable.Rows[1].Range.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                adminTable.Rows[1].Range.Font.Size = 10;
                adminTable.Rows[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                //cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                
                for (int i = 0; i < OwnerStorage.ListOfAdmins.Count; i++)
                {
                    int counter = 0;
                    string times = "";
                    foreach (string[] s in OwnerStorage.AdminLog)
                    {
                        if (s[0] == OwnerStorage.ListOfAdmins[i].objectId)
                        {
                            counter++;
                        }
                    }
                    int innerCounter = 0;
                    foreach(string[] s in OwnerStorage.AdminLog)
                    {

                        if (s[0] == OwnerStorage.ListOfAdmins[i].objectId)
                        {
                            innerCounter++;
                            if (counter == innerCounter)
                                times = times + s[1];
                            else
                                times = times + s[1] + "\n";
                        }
                    }

                    adminTable.Cell(i + 2, 1).Range.Text = OwnerStorage.ListOfAdmins[i].UserName.ToString();
                    adminTable.Cell(i + 2, 2).Range.Text = counter.ToString() + " logins during this session.";
                    adminTable.Cell(i + 2, 3).Range.Text = times;
                }

                Microsoft.Office.Interop.Word.Paragraph para4 = document.Content.Paragraphs.Add(ref missing);
                #endregion

                //Active Reservations output
                if (OwnerStorage.ActiveReservations.Count != 0)
                {
                    Microsoft.Office.Interop.Word.Paragraph para5 = document.Content.Paragraphs.Add(ref missing);
                    para5.Range.set_Style(ref styleHeading1);
                    para5.Range.Font.Bold = 1;
                    para5.Range.Text = "Active Reservations";
                    para5.Range.InsertParagraphAfter();

                    foreach (RestaurantTable t in OwnerStorage.RestaurantTables)
                    {
                        List<Reservation> tempList = new List<Reservation>();
                        foreach (Reservation r in OwnerStorage.ActiveReservations)
                        {
                            if (r.TableId == t.objectId)
                            {
                                tempList.Add(r);
                            }

                        }

                        if (tempList.Count != 0)
                        {
                            
                            Microsoft.Office.Interop.Word.Table activeTable = document.Tables.Add(para5.Range, tempList.Count + 2, 4, ref missing, ref missing);
                            activeTable.Borders.Enable = 1;
                            activeTable.Rows[1].Cells[1].Merge(activeTable.Rows[1].Cells[4]);
                            activeTable.Cell(1, 1).Range.Text = t.Name.ToString();
                            //format merged heading
                            activeTable.Cell(1,1).Range.Font.Bold = 1;
                            activeTable.Cell(1, 1).Range.Font.Name = "verdana";
                            activeTable.Cell(1, 1).Range.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                            activeTable.Cell(1, 1).Range.Font.Size = 10;
                            activeTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                            //format secondary headings
                            activeTable.Rows[2].Range.Font.Bold = 1;
                            activeTable.Rows[2].Range.Font.Name = "verdana";
                            activeTable.Rows[2].Range.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                            activeTable.Rows[2].Range.Font.Size = 10;
                            activeTable.Rows[2].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                            activeTable.Cell(2, 1).Range.Text = "Name";
                            activeTable.Cell(2, 2).Range.Text = "Contact Nr.";
                            activeTable.Cell(2, 3).Range.Text = "Taken From";
                            activeTable.Cell(2, 4).Range.Text = "Taken To";

                            for(int i = 0; i < tempList.Count; i++)
                            {
                                activeTable.Cell(i + 3, 1).Range.Text = tempList[i].Name.ToString();
                                activeTable.Cell(i + 3, 2).Range.Text = tempList[i].Number.ToString();
                                activeTable.Cell(i + 3, 3).Range.Text = tempList[i].TakenFrom.ToString();
                                activeTable.Cell(i + 3, 4).Range.Text = tempList[i].TakenTo.ToString();
                            }
                            Microsoft.Office.Interop.Word.Paragraph para6 = document.Content.Paragraphs.Add(ref missing);
                            
                        }
                    }
                }
                //Past Reservations displayed
                if (OwnerStorage.PastReservations.Count != 0)
                {
                    Microsoft.Office.Interop.Word.Paragraph para7 = document.Content.Paragraphs.Add(ref missing);
                    para7.Range.set_Style(ref styleHeading1);
                    para7.Range.Font.Bold = 1;
                    para7.Range.Text = "Past Reservations";
                    para7.Range.InsertParagraphAfter();

                    foreach (RestaurantTable t in OwnerStorage.RestaurantTables)
                    {
                        List<Reservation> tempList = new List<Reservation>();
                        foreach (Reservation r in OwnerStorage.PastReservations)
                        {
                            if (r.TableId == t.objectId)
                            {
                                tempList.Add(r);
                            }

                        }

                        if (tempList.Count != 0)
                        {

                            Microsoft.Office.Interop.Word.Table pastTable = document.Tables.Add(para7.Range, tempList.Count + 2, 4, ref missing, ref missing);
                            pastTable.Borders.Enable = 1;
                            pastTable.Rows[1].Cells[1].Merge(pastTable.Rows[1].Cells[4]);
                            pastTable.Cell(1, 1).Range.Text = t.Name.ToString();
                            //format merged heading
                            pastTable.Cell(1, 1).Range.Font.Bold = 1;
                            pastTable.Cell(1, 1).Range.Font.Name = "verdana";
                            pastTable.Cell(1, 1).Range.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                            pastTable.Cell(1, 1).Range.Font.Size = 10;
                            pastTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                            //format secondary headings
                            pastTable.Rows[2].Range.Font.Bold = 1;
                            pastTable.Rows[2].Range.Font.Name = "verdana";
                            pastTable.Rows[2].Range.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                            pastTable.Rows[2].Range.Font.Size = 10;
                            pastTable.Rows[2].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                            pastTable.Cell(2, 1).Range.Text = "Name";
                            pastTable.Cell(2, 2).Range.Text = "Contact Nr.";
                            pastTable.Cell(2, 3).Range.Text = "Taken From";
                            pastTable.Cell(2, 4).Range.Text = "Taken To";

                            for (int i = 0; i < tempList.Count; i++)
                            {
                                pastTable.Cell(i + 3, 1).Range.Text = tempList[i].Name.ToString();
                                pastTable.Cell(i + 3, 2).Range.Text = tempList[i].Number.ToString();
                                pastTable.Cell(i + 3, 3).Range.Text = tempList[i].TakenFrom.ToString();
                                pastTable.Cell(i + 3, 4).Range.Text = tempList[i].TakenTo.ToString();
                            }
                            Microsoft.Office.Interop.Word.Paragraph paraSpace = document.Content.Paragraphs.Add(ref missing);
                        }
                    }
                }


                //Save the directory  
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), @"TableFindBackend\System Reports\" + OwnerStorage.ThisRestaurant.Name + @"\" + OwnerStorage.ThisRestaurant.objectId);
                if (File.Exists(path) != true)
                    Directory.CreateDirectory(path);

                FileInfo fInfo = new FileInfo(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx");



                if (word == true)
                {
                    document.SaveAs("TableFindBackend\\System Reports\\" + OwnerStorage.ThisRestaurant.Name + @"\" + OwnerStorage.ThisRestaurant.objectId + "\\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx");
                    document.Close(false);
                    winword.Quit(false);
                    System.Diagnostics.Process.Start(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx");
                }
                else
                {
                    //if (IsFileLocked(fInfo) == true)
                    //{
                        document.SaveAs("TableFindBackend\\System Reports\\" + OwnerStorage.ThisRestaurant.Name + @"\" + OwnerStorage.ThisRestaurant.objectId + "\\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx");
                    //}
                    document.Close(false);
                    winword.Quit(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            #region
            //Document document = new Document();

            //Section section = document.AddSection();
            //HeaderFooter header = section.HeadersFooters.Header;
            //Paragraph headerParagraph = header.AddParagraph();
            //headerParagraph.Format.AfterSpacing = 10;
            //headerParagraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            //TextRange headerText = headerParagraph.AppendText("TableFindBackend System Report for " + OwnerStorage.ThisRestaurant.Name); 
            //headerText.CharacterFormat.Bold = true;


            //Paragraph p2 = section.AddParagraph();
            //p2.AppendText("System Report for " + System.DateTime.Now.ToString("D") + " as captured at " + System.DateTime.Now.ToString("t"));
            //p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

            //Paragraph p4 = section.AddParagraph();
            //p4.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

            ////PictureWatermark logo = new PictureWatermark();
            ////logo.Picture = Image.FromFile(TableFindBackend.Properties.Resources.Logo); <-- can't convert bitmap to string
            ////logo.Scaling = 100;
            ////logo.IsWashout = false;
            ////document.Watermark = logo;

            //DocPicture logo = p4.AppendPicture(TableFindBackend.Properties.Resources.Logo);
            //logo.Width = 100;
            //logo.Height = 100;

            ////active reservations displayed
            //Paragraph p5 = section.AddParagraph();
            //p5.AppendText("Active Reservations");
            //p5.Format.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Double;
            //p5.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

            //if (OwnerStorage.ActiveReservations.Count != 0)
            //{
            //    foreach (RestaurantTable t in OwnerStorage.RestaurantTables)
            //    {
            //        List<Reservation> tempList = new List<Reservation>();
            //        foreach (Reservation rE in OwnerStorage.ActiveReservations)
            //        {
            //            if (rE.TableId == t.objectId)
            //            {
            //                tempList.Add(rE);
            //            }
            //        }


            //        if (tempList.Count != 0)
            //        {
            //            Paragraph paragraph = section.AddParagraph();
            //            //paragraph.AppendText("Table Name: " + t.name);
            //            TextRange tableNames = paragraph.AppendText(t.Name);
            //            tableNames.CharacterFormat.Bold = true;
            //            paragraph.Format.BeforeSpacing = 5;
            //            paragraph.Format.AfterSpacing = 5;


            //            Table table = section.AddTable(true);
            //            table.TableFormat.HorizontalAlignment = RowAlignment.Center;

            //            String[] Header = { "Name", "Date Taken From", "Date Taken To", "Contact Number" };

            //            List<String[]> data = new List<string[]>();

            //            foreach (Reservation rE in tempList)
            //            {
            //                data.Add(new String[] { rE.Name, rE.TakenFrom.ToString(), rE.TakenTo.ToString(), rE.Number });
            //            }

            //            table.ResetCells(data.Count + 1, Header.Length);
            //            //Header Row
            //            TableRow FRow = table.Rows[0];
            //            FRow.IsHeader = true;
            //            //Row Height
            //            FRow.Height = 23;
            //            //Header Format
            //            FRow.RowFormat.BackColor = Color.AliceBlue;
            //            for (int i = 0; i < Header.Length; i++)
            //            {
            //                //Cell Alignment
            //                Paragraph p = FRow.Cells[i].AddParagraph();
            //                FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //                p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            //                //Data Format
            //                TextRange TR = p.AppendText(Header[i]);
            //                TR.CharacterFormat.FontName = "Calibri";
            //                TR.CharacterFormat.FontSize = 14;
            //                TR.CharacterFormat.TextColor = Color.Teal;
            //                TR.CharacterFormat.Bold = true;
            //            }

            //            //Data Row
            //            for (int rE = 0; rE < data.Count; rE++)
            //            {
            //                TableRow DataRow = table.Rows[rE + 1];

            //                //Row Height
            //                DataRow.Height = 20;

            //                //C Represents Column.
            //                for (int cE = 0; cE < data[rE].Length; cE++)
            //                {
            //                    //Cell Alignment
            //                    DataRow.Cells[cE].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //                    //Fill Data in Rows
            //                    Paragraph p3 = DataRow.Cells[cE].AddParagraph();
            //                    TextRange TR2 = p3.AppendText(data[rE][cE]);
            //                    //Format Cells
            //                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            //                    TR2.CharacterFormat.FontName = "Calibri";
            //                    TR2.CharacterFormat.FontSize = 12;
            //                    TR2.CharacterFormat.TextColor = Color.Black;
            //                }
            //            }
            //        }
            //    }

            //}
            //else
            //{
            //    Paragraph empty = section.AddParagraph();
            //    empty.AppendText("There are currently no reservations that expired in the current session");
            //    empty.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            //    empty.Format.BeforeSpacing = 5;
            //    empty.Format.AfterSpacing = 5;
            //}

            ////past reservations displayed

            //Paragraph p6 = section.AddParagraph();
            //p6.AppendText("Past Reservations");
            //p6.Format.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Double;
            //p6.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            //p6.Format.BeforeSpacing = 20;

            //if (OwnerStorage.PastReservations.Count != 0)
            //{

            //    foreach (RestaurantTable t in OwnerStorage.RestaurantTables)
            //    {
            //        List<Reservation> tempList1 = new List<Reservation>();
            //        foreach (Reservation rE in OwnerStorage.PastReservations)
            //        {
            //            if (rE.TableId == t.objectId)
            //            {
            //                tempList1.Add(rE);
            //            }
            //        }

            //        if (tempList1.Count != 0)
            //        {
            //            Paragraph paragraph = section.AddParagraph();
            //            //paragraph.AppendText("Table Name: " + t.name);
            //            TextRange tableNames = paragraph.AppendText(t.Name);
            //            tableNames.CharacterFormat.Bold = true;
            //            paragraph.Format.BeforeSpacing = 5;
            //            paragraph.Format.AfterSpacing = 5;


            //            Table table = section.AddTable(true);
            //            table.TableFormat.HorizontalAlignment = RowAlignment.Center;

            //            String[] Header = { "Name", "Date Taken From", "Date Taken To", "Contact Number" };

            //            List<String[]> data = new List<string[]>();

            //            foreach (Reservation rE in tempList1)
            //            {
            //                data.Add(new String[] { rE.Name, rE.TakenFrom.ToString(), rE.TakenTo.ToString(), rE.Number });
            //            }

            //            table.ResetCells(data.Count + 1, Header.Length);
            //            //Header Row
            //            TableRow FRow = table.Rows[0];
            //            FRow.IsHeader = true;
            //            //Row Height
            //            FRow.Height = 23;
            //            //Header Format
            //            FRow.RowFormat.BackColor = Color.AliceBlue;
            //            for (int i = 0; i < Header.Length; i++)
            //            {
            //                //Cell Alignment
            //                Paragraph p = FRow.Cells[i].AddParagraph();
            //                FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //                p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            //                //Data Format
            //                TextRange TR = p.AppendText(Header[i]);
            //                TR.CharacterFormat.FontName = "Calibri";
            //                TR.CharacterFormat.FontSize = 14;
            //                TR.CharacterFormat.TextColor = Color.Teal;
            //                TR.CharacterFormat.Bold = true;
            //            }

            //            //Data Row
            //            for (int rE = 0; rE < data.Count; rE++)
            //            {
            //                TableRow DataRow = table.Rows[rE + 1];

            //                //Row Height
            //                DataRow.Height = 20;

            //                //C Represents Column.
            //                for (int cE = 0; cE < data[rE].Length; cE++)
            //                {
            //                    //Cell Alignment
            //                    DataRow.Cells[cE].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //                    //Fill Data in Rows
            //                    Paragraph p3 = DataRow.Cells[cE].AddParagraph();
            //                    TextRange TR2 = p3.AppendText(data[rE][cE]);
            //                    //Format Cells
            //                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            //                    TR2.CharacterFormat.FontName = "Calibri";
            //                    TR2.CharacterFormat.FontSize = 12;
            //                    TR2.CharacterFormat.TextColor = Color.Black;
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    Paragraph empty = section.AddParagraph();
            //    empty.AppendText("There are currently no reservations that expired in the current session");
            //    empty.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            //    empty.Format.BeforeSpacing = 5;
            //    empty.Format.AfterSpacing = 5;
            //}

            //////Admin Users displayed

            ////Paragraph p8 = section.AddParagraph();
            ////p8.AppendText("Admin Users");
            ////p8.Format.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Double;
            ////p8.Format.BeforeSpacing = 20;
            ////p8.Format.AfterSpacing = 5;
            ////p8.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

            ////Table adminTable = section.AddTable(true);
            ////adminTable.TableFormat.HorizontalAlignment = RowAlignment.Center;

            ////String[] adminsHeader = { "Admin User", "Logins For This Session" };

            ////List<String[]> adminsData = new List<string[]>();
            ////for (int i = 0; i < OwnerStorage.ListOfAdmins.Count; i++)
            ////{
            ////    for (int inner = 0; inner < OwnerStorage.AdminLog.Count; inner++)
            ////    {
            ////        if (OwnerStorage.ListOfAdmins[i].objectId==OwnerStorage.AdminLog[inner][0])
            ////        {
            ////            adminTable.ResetCells(adminsData.Count + 1, adminsHeader.Length);
            ////            //Header Row
            ////            TableRow FRowLogA = adminTable.Rows[0];
            ////            FRowLogA.IsHeader = true;
            ////            //Row Height
            ////            FRowLogA.Height = 23;
            ////            //Header Format
            ////            FRowLogA.RowFormat.BackColor = Color.AliceBlue;
            ////            for (int iH = 0; iH < adminsHeader.Length; i++)
            ////            {
            ////                //Cell Alignment

            ////                FRowLogA.Cells[iH].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            ////                p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            ////                //Data Format
            ////                TextRange TR = p.AppendText(adminsHeader[iH]);
            ////                TR.CharacterFormat.FontName = "Calibri";
            ////                TR.CharacterFormat.FontSize = 14;
            ////                TR.CharacterFormat.TextColor = Color.Teal;
            ////                TR.CharacterFormat.Bold = true;
            ////            }

            ////            //Data Row
            ////            for (int r = 0; r < adminsData.Count; r++)
            ////            {
            ////                TableRow DataRow = adminTable.Rows[r + 1];

            ////                //Row Height
            ////                DataRow.Height = 20;

            ////                //C Represents Column.
            ////                for (int c = 0; c < adminsData[r].Length; c++)
            ////                {
            ////                    //Cell Alignment
            ////                    DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            ////                    //Fill Data in Rows
            ////                    Paragraph p3 = DataRow.Cells[c].AddParagraph();
            ////                    TextRange TR2 = p3.AppendText(adminsData[r][c]);
            ////                    //Format Cells
            ////                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            ////                    TR2.CharacterFormat.FontName = "Calibri";
            ////                    TR2.CharacterFormat.FontSize = 12;
            ////                    TR2.CharacterFormat.TextColor = Color.Black;
            ////                }
            ////            }
            ////        }
            ////    }

            ////}



            ////System Log displayed
            //Paragraph p7 = section.AddParagraph();
            //p7.AppendText("System Log");
            //p7.Format.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Double; 
            //p7.Format.BeforeSpacing = 20;
            //p7.Format.AfterSpacing = 5;
            //p7.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

            //Table logTable = section.AddTable(true);
            //logTable.TableFormat.HorizontalAlignment = RowAlignment.Center;

            //String[] logHeader = { "Event Recorded", "Recorded Time" };

            //List<String[]> logData = new List<string[]>();
            //for (int i = 0;i<OwnerStorage.LogInfo.Count;i++)
            //{
            //    if(OwnerStorage.LogTimes[i]=="blank")
            //    logData.Add(new String[] { OwnerStorage.LogInfo[i], "" });
            //    else
            //        logData.Add(new String[] { OwnerStorage.LogInfo[i], OwnerStorage.LogTimes[i] });
            //}

            //logTable.ResetCells(logData.Count + 1, logHeader.Length);
            ////Header Row
            //TableRow FRowLog = logTable.Rows[0];
            //FRowLog.IsHeader = true;
            ////Row Height
            //FRowLog.Height = 23;
            ////Header Format
            //FRowLog.RowFormat.BackColor = Color.AliceBlue;
            //for (int i = 0; i < logHeader.Length; i++)
            //{
            //    //Cell Alignment
            //    Paragraph p = FRowLog.Cells[i].AddParagraph();
            //    FRowLog.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //    p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            //    //Data Format
            //    TextRange TR = p.AppendText(logHeader[i]);
            //    TR.CharacterFormat.FontName = "Calibri";
            //    TR.CharacterFormat.FontSize = 14;
            //    TR.CharacterFormat.TextColor = Color.Teal;
            //    TR.CharacterFormat.Bold = true;
            //}

            ////Data Row
            //for (int rE = 0; rE < logData.Count; rE++)
            //{
            //    TableRow DataRow = logTable.Rows[rE + 1];

            //    //Row Height
            //    DataRow.Height = 20;

            //    //C Represents Column.
            //    for (int cE = 0; cE < logData[rE].Length; cE++)
            //    {
            //        //Cell Alignment
            //        DataRow.Cells[cE].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //        //Fill Data in Rows
            //        Paragraph p3 = DataRow.Cells[cE].AddParagraph();
            //        TextRange TR2 = p3.AppendText(logData[rE][cE]);
            //        //Format Cells
            //        p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            //        TR2.CharacterFormat.FontName = "Calibri";
            //        TR2.CharacterFormat.FontSize = 12;
            //        TR2.CharacterFormat.TextColor = Color.Black;
            //    }
            //}

            ////where document is saved to
            //string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), @"TableFindBackend\System Reports\" + OwnerStorage.ThisRestaurant.Name + @"\" + OwnerStorage.ThisRestaurant.objectId);


            //if (File.Exists(path) != true)
            //    Directory.CreateDirectory(path);

            ////Kills the word document if it is already open

            //FileInfo fInfo = new FileInfo(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx");

            //if (File.Exists(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx") != true)
            //{
            //        document.SaveToFile(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx", FileFormat.Docx);
            //}

            ////launches document
            //if (word == true)
            //{
            //    if (File.Exists(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx")==true)
            //    {
            //        if (IsFileLocked(fInfo) == true)//means file is still open
            //        {
            //            MessageBox.Show("The Document is already open in one instance of Word. Please close that document and try again", "Document already open", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        }
            //        else
            //        {
            //            try
            //            {
            //                System.Diagnostics.Process.Start(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx");
            //            }
            //            catch { }
            //            document.SaveToFile(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx", FileFormat.Docx);
            //        }
            //    }
            //    else
            //    {
            //        document.SaveToFile(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx", FileFormat.Docx);
            //        try
            //        {
            //            System.Diagnostics.Process.Start(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx");
            //        }
            //        catch { }

            //    }
            //}
            #endregion
        }
        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            ToggleLoading(true);
            var t = System.Threading.Tasks.Task.Run(() => GenerateWordDoc(true));
            t.Wait();
            
            ToggleLoading(false);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            ToggleLoading(true);
            var t = System.Threading.Tasks.Task.Run(() => GenerateWordDoc(false));
            t.Wait();            

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), @"TableFindBackend\System Reports\" + OwnerStorage.ThisRestaurant.Name + @"\" + OwnerStorage.ThisRestaurant.objectId);

            try
            {
                //load document
                Document document = new Document();
                document.LoadFromFileInReadMode(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx", FileFormat.Docx);

                //convert to PDF
                document.SaveToFile(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".pdf", FileFormat.PDF);

                //launch document
                System.Diagnostics.Process.Start(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".pdf");
                ToggleLoading(false);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
    }
}
