﻿using System;
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
using System.Data;
using System.Drawing;
using Spire.Doc.Fields;

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

                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), @"TableFindBackend\System Reports\"+OwnerStorage.ThisRestaurant.name+@"\"+OwnerStorage.ThisRestaurant.locationString);
                if (File.Exists(path) != true)
                    Directory.CreateDirectory(path);

                workbook.SaveAs("TableFindBackend\\System Reports\\"+OwnerStorage.ThisRestaurant.name+@"\"+OwnerStorage.ThisRestaurant.locationString+"\\SystemReport_"+System.DateTime.Now.ToString("dd-MM-yyyy")+ ".xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                workbook.Save();

            }
            catch(Exception ex)
            {

            }

        }
        private void GenerateWordDoc(Boolean word)
        {
            Document document = new Document();

            Section section = document.AddSection();
            HeaderFooter header = section.HeadersFooters.Header;
            Paragraph headerParagraph = header.AddParagraph();
            headerParagraph.Format.AfterSpacing = 10;
            headerParagraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            TextRange headerText = headerParagraph.AppendText("TableFindBackend System Report for " + OwnerStorage.ThisRestaurant.name); 
            headerText.CharacterFormat.Bold = true;


            Paragraph p2 = section.AddParagraph();
            p2.AppendText("System Report for " + System.DateTime.Now.ToString("D") + " as captured at " + System.DateTime.Now.ToString("t"));
            p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

            Paragraph p4 = section.AddParagraph();
            p4.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            DocPicture logo = p4.AppendPicture(TableFindBackend.Properties.Resources.Logo);
            logo.Width = 100;
            logo.Height = 100;

            //active reservations displayed
            Paragraph p5 = section.AddParagraph();
            p5.AppendText("Active Reservations");
            p5.Format.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Double;

            foreach (RestaurantTable t in OwnerStorage.RestaurantTables)
            {
                List<Reservation> tempList = new List<Reservation>();
                foreach (Reservation r in OwnerStorage.ActiveReservations)
                {
                    if (r.tableId == t.objectId)
                    {
                        tempList.Add(r);
                    }
                }


                if (tempList.Count != 0)
                {
                    Paragraph paragraph = section.AddParagraph();
                    paragraph.AppendText("Table Name: " + t.name);
                    paragraph.Format.BeforeSpacing = 5;
                    paragraph.Format.AfterSpacing = 5;


                    Table table = section.AddTable(true);

                    String[] Header = { "Name", "Date Taken From", "Date Taken To", "Contact Number" };

                    List<String[]> data = new List<string[]>();

                    foreach (Reservation r in tempList)
                    {
                        data.Add(new String[] { r.name, r.takenFrom.ToString(), r.takenTo.ToString(), r.number });
                    }

                    table.ResetCells(data.Count + 1, Header.Length);
                    //Header Row
                    TableRow FRow = table.Rows[0];
                    FRow.IsHeader = true;
                    //Row Height
                    FRow.Height = 23;
                    //Header Format
                    FRow.RowFormat.BackColor = Color.AliceBlue;
                    for (int i = 0; i < Header.Length; i++)
                    {
                        //Cell Alignment
                        Paragraph p = FRow.Cells[i].AddParagraph();
                        FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                        //Data Format
                        TextRange TR = p.AppendText(Header[i]);
                        TR.CharacterFormat.FontName = "Calibri";
                        TR.CharacterFormat.FontSize = 14;
                        TR.CharacterFormat.TextColor = Color.Teal;
                        TR.CharacterFormat.Bold = true;
                    }

                    //Data Row
                    for (int r = 0; r < data.Count; r++)
                    {
                        TableRow DataRow = table.Rows[r + 1];

                        //Row Height
                        DataRow.Height = 20;

                        //C Represents Column.
                        for (int c = 0; c < data[r].Length; c++)
                        {
                            //Cell Alignment
                            DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            //Fill Data in Rows
                            Paragraph p3 = DataRow.Cells[c].AddParagraph();
                            TextRange TR2 = p3.AppendText(data[r][c]);
                            //Format Cells
                            p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                            TR2.CharacterFormat.FontName = "Calibri";
                            TR2.CharacterFormat.FontSize = 12;
                            TR2.CharacterFormat.TextColor = Color.Black;
                        }
                    }
                }


            }

            //past reservations displayed


            Paragraph p6 = section.AddParagraph();
            p6.AppendText("Past Reservations");
            p6.Format.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Double;
            p6.Format.BeforeSpacing = 20;

            foreach (RestaurantTable t in OwnerStorage.RestaurantTables)
            {
                List<Reservation> tempList1 = new List<Reservation>();
                foreach (Reservation r in OwnerStorage.PastReservations)
                {
                    if (r.tableId == t.objectId)
                    {
                        tempList1.Add(r);
                    }
                }

                if (tempList1.Count != 0)
                {
                    Paragraph paragraph = section.AddParagraph();
                    paragraph.AppendText("Table Name: " + t.name);
                    paragraph.Format.BeforeSpacing = 5;
                    paragraph.Format.AfterSpacing = 5;


                    Table table = section.AddTable(true);

                    String[] Header = { "Name", "Date Taken From", "Date Taken To", "Contact Number" };

                    List<String[]> data = new List<string[]>();

                    foreach (Reservation r in tempList1)
                    {
                        data.Add(new String[] { r.name, r.takenFrom.ToString(), r.takenTo.ToString(), r.number });
                    }

                    table.ResetCells(data.Count + 1, Header.Length);
                    //Header Row
                    TableRow FRow = table.Rows[0];
                    FRow.IsHeader = true;
                    //Row Height
                    FRow.Height = 23;
                    //Header Format
                    FRow.RowFormat.BackColor = Color.AliceBlue;
                    for (int i = 0; i < Header.Length; i++)
                    {
                        //Cell Alignment
                        Paragraph p = FRow.Cells[i].AddParagraph();
                        FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                        //Data Format
                        TextRange TR = p.AppendText(Header[i]);
                        TR.CharacterFormat.FontName = "Calibri";
                        TR.CharacterFormat.FontSize = 14;
                        TR.CharacterFormat.TextColor = Color.Teal;
                        TR.CharacterFormat.Bold = true;
                    }

                    //Data Row
                    for (int r = 0; r < data.Count; r++)
                    {
                        TableRow DataRow = table.Rows[r + 1];

                        //Row Height
                        DataRow.Height = 20;

                        //C Represents Column.
                        for (int c = 0; c < data[r].Length; c++)
                        {
                            //Cell Alignment
                            DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            //Fill Data in Rows
                            Paragraph p3 = DataRow.Cells[c].AddParagraph();
                            TextRange TR2 = p3.AppendText(data[r][c]);
                            //Format Cells
                            p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                            TR2.CharacterFormat.FontName = "Calibri";
                            TR2.CharacterFormat.FontSize = 12;
                            TR2.CharacterFormat.TextColor = Color.Black;
                        }
                    }
                }
            }

            //System Log displayed
            Paragraph p7 = section.AddParagraph();
            p7.AppendText("System Log");
            p7.Format.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Double;
            p7.Format.BeforeSpacing = 20;

            Table logTable = section.AddTable(true);

            String[] logHeader = { "Event Recorded", "Recorded Time" };

            String[][] logData = { OwnerStorage.LogInfo.ToArray(), OwnerStorage.LogTimes.ToArray() };

            logTable.ResetCells(logData.Length + 1, logHeader.Length);
            //Header Row
            TableRow FLRow = logTable.Rows[0];
            FLRow.IsHeader = true;
            //Row Height
            FLRow.Height = 23;
            //Header Format
            FLRow.RowFormat.BackColor = Color.AliceBlue;
            for (int i = 0; i < logHeader.Length; i++)
            {
                //Cell Alignment
                Paragraph p = FLRow.Cells[i].AddParagraph();
                FLRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                //Data Format
                TextRange TR = p.AppendText(logHeader[i]);
                TR.CharacterFormat.FontName = "Calibri";
                TR.CharacterFormat.FontSize = 14;
                TR.CharacterFormat.TextColor = Color.Teal;
                TR.CharacterFormat.Bold = true;
            }

            for (int r = 0; r < logData.Length; r++)
            {
                TableRow DataRow = logTable.Rows[r + 1];

                //Row Height
                DataRow.Height = 20;

                //C Represents Column.
                for (int c = 0; c < logData[r].Length; c++)
                {
                    //Cell Alignment
                    DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    //Fill Data in Rows
                    Paragraph p3 = DataRow.Cells[c].AddParagraph();
                    TextRange TR2 = p3.AppendText(logData[c][r]);
                    //Format Cells
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    TR2.CharacterFormat.FontName = "Calibri";
                    TR2.CharacterFormat.FontSize = 12;
                    TR2.CharacterFormat.TextColor = Color.Black;
                }
            }

            //where document is saved to
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), @"TableFindBackend\System Reports\" + OwnerStorage.ThisRestaurant.name + @"\" + OwnerStorage.ThisRestaurant.locationString);
            if (File.Exists(path) != true)
                Directory.CreateDirectory(path);
            document.SaveToFile(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx", FileFormat.Docx);

            //launches document
            if (word == true)
            {
                try
                {
                    System.Diagnostics.Process.Start(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx");
                }
                catch { }
            }
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            GenerateWordDoc(true);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            GenerateWordDoc(false);

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), @"TableFindBackend\System Reports\" + OwnerStorage.ThisRestaurant.name + @"\" + OwnerStorage.ThisRestaurant.locationString);

            //load document
            Document document = new Document();
            document.LoadFromFileInReadMode(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".docx", FileFormat.Docx);

            //convert to PDF
            document.SaveToFile(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".pdf", FileFormat.PDF);

            //launch document
            System.Diagnostics.Process.Start(path + @"\SystemReport_" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".pdf");



        }
    }
}
