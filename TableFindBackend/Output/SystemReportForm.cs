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
using Spire.Doc;
using Spire.Doc.Documents;
using System.IO;

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
                    if (reservation.tableId == table.objectId)
                        tempList.Add(reservation);

                }
                if (tempList.Count!=0)
                {
                    AddRestaurantReservationTable(table, tempList);
                }
            }
            for(int i = 0;i<OwnerStorage.LogInfo.Count;i++)
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

            dgvTables.DataSource = OwnerStorage.RestaurantTables;
            //rtbLog.Lines = OwnerStorage.Log.ToArray();
        }

        private void AddRestaurantReservationTable(RestaurantTable table,List<Reservation> list)
        {

            Panel backPanel = new Panel();
            backPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            backPanel.Width = flpReservationTables.Width-10;
            backPanel.Height = 80;
            Label titleLabel = new Label();
            titleLabel.Font=new Font("Century Gothic", 10);
            titleLabel.Text = table.name;
            titleLabel.Location = new Point(10, 10);
            backPanel.Controls.Add(titleLabel);             
            DataGridView newView = new DataGridView();
            newView.Location = new Point(5, 40);
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
                newView.Rows.Add(reservation.name, reservation.takenFrom, reservation.takenTo, reservation.number);
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
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                app.SheetsInNewWorkbook = 4;
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet wsTables = null;
                Microsoft.Office.Interop.Excel.Range range;
                app.Visible = true;

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
                range = wsTables.get_Range("A1", "E100");
                range.Columns.AutoFit();


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
                range = wsTables.get_Range("A1", "E100");
                range.Columns.AutoFit();

                //setup for Sheet3
                wsTables = workbook.Sheets["Sheet3"];
                wsTables.Name = "Reservations";
                int rowHeadingIndex = 0;
                for (int i = 0; i < OwnerStorage.RestaurantTables.Count;i++) 
                {
                    List<Reservation> tempList = new List<Reservation>();
                    foreach (Reservation reservation in OwnerStorage.ActiveReservations)
                    {
                        if (reservation.tableId == OwnerStorage.RestaurantTables[i].objectId)
                            tempList.Add(reservation);
                    }
                    if (tempList.Count != 0)
                    {
                        rowHeadingIndex += 1;

                        wsTables.Range[wsTables.Cells[rowHeadingIndex , 1], wsTables.Cells[rowHeadingIndex , 4]].Merge();
                        wsTables.Cells[rowHeadingIndex , 1] = OwnerStorage.RestaurantTables[i].name;

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
                            wsTables.Cells[rowHeadingIndex + inner + 2, 1] = tempList[inner].name;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 2] = tempList[inner].takenFrom;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 3] = tempList[inner].takenTo;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 4] = tempList[inner].number;                            
                        }
                        rowHeadingIndex += tempList.Count+2;
                    }
                }
                range = wsTables.get_Range("A1", "E100");
                range.Columns.AutoFit();

                //setup for sheet4
                wsTables = workbook.Sheets["Sheet4"];
                wsTables.Name = "Expired Reservations";
                rowHeadingIndex = 0;
                for (int i = 0; i < OwnerStorage.RestaurantTables.Count; i++)
                {
                    List<Reservation> tempList = new List<Reservation>();
                    foreach (Reservation reservation in OwnerStorage.PastReservations)
                    {
                        if (reservation.tableId == OwnerStorage.RestaurantTables[i].objectId)
                            tempList.Add(reservation);
                    }
                    if (tempList.Count != 0)
                    {
                        rowHeadingIndex += 1;

                        wsTables.Range[wsTables.Cells[rowHeadingIndex, 1], wsTables.Cells[rowHeadingIndex, 4]].Merge();
                        wsTables.Cells[rowHeadingIndex, 1] = OwnerStorage.RestaurantTables[i].name;

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
                            wsTables.Cells[rowHeadingIndex + inner + 2, 1] = tempList[inner].name;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 2] = tempList[inner].takenFrom;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 3] = tempList[inner].takenTo;
                            wsTables.Cells[rowHeadingIndex + inner + 2, 4] = tempList[inner].number;

                        }
                        rowHeadingIndex += tempList.Count + 2;
                    }
                }
                range = wsTables.get_Range("A1", "E100");
                range.Columns.AutoFit();
                if (File.Exists(@"C:\Users\Johann\Documents\TableFindBackend\System Reports") != true)
                    Directory.CreateDirectory(@"C:\Users\Johann\Documents\TableFindBackend\System Reports");


                workbook.SaveAs("TableFindBackend\\System Reports\\excel_sr" + System.DateTime.Now.ToString("dd-MM-yyyy")+ ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                workbook.Save();

            }
            catch(Exception ex)
            {

            }
        }
        private void btnWord_Click(object sender, EventArgs e)
        {
            // test for generating a Word document
            Document document = new Document();

            Paragraph paragraph = document.AddSection().AddParagraph();
            paragraph.AppendText("Hello World!");
            paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

            //where does it save the document to?
            document.SaveToFile("Sample.docx", FileFormat.Docx);
            try
            {
                System.Diagnostics.Process.Start("Sample.docx");
            }
            catch { }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            // test for generating a PDF -- was getting ahead of myself, will leave this for after Word works completely

        }
    }
}
