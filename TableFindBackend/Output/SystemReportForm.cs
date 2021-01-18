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
                foreach (Reservation reservation in OwnerStorage.AllReservations)
                {
                    if (reservation.tableId == table.objectId)
                        tempList.Add(reservation);

                }
                if (tempList.Count!=0)
                {
                    AddRestaurantReservationTable(table, tempList);
                }
            }
            dgvTables.DataSource = OwnerStorage.RestaurantTables;
            rtbLog.Lines = OwnerStorage.Log.ToArray();
        }

        private void AddRestaurantReservationTable(RestaurantTable table,List<Reservation> list)
        {

            Panel backPanel = new Panel();
            backPanel.BorderStyle = BorderStyle.FixedSingle;
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
                // creating Excel Application  
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                // creating new WorkBook within Excel application  
                app.SheetsInNewWorkbook = 3;
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel._Worksheet wsTables = null;

                app.Visible = true;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                wsTables = workbook.Sheets["Sheet1"];
                wsTables = workbook.ActiveSheet;
                wsTables.Name = "Restaurant Tables";
                for (int i = 1; i < dgvTables.Columns.Count + 1; i++)
                {                    
                    wsTables.Cells[1, i] = dgvTables.Columns[i - 1].HeaderText;                   
                }
                for (int i = 0; i < dgvTables.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvTables.Columns.Count; j++)
                    {
                        wsTables.Cells[i + 2, j + 1] = dgvTables.Rows[i].Cells[j].Value.ToString();
                    }
                }
                wsTables = workbook.Sheets["Sheet2"];
                wsTables.Name = "System Log";     
                wsTables.Cells[1, 1] = "System Events";
                int index = 1;
                foreach (String item in OwnerStorage.Log)
                {
                    index++;
                    wsTables.Cells[index, 1] = item;

                }
            }
            catch(Exception ex)
            {

            }

        }
    }
}
