using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableFindBackend
{
    public partial class RestaurantTableView : UserControl
    {
        public RestaurantTableView()
        {
            InitializeComponent();
        }

        #region properties

        private int seating;
        private string name;
        private bool availability;
        private bool removable;



        [Category("Custom Prop")]
        public string Label
        {
            get { return name; }
            set { name = value ; lblName.Text = value; }
        }
        [Category("Custom Props")]
        public bool Availability
        {
            get { return availability; }
            set 
            { 
                availability = value; 

                if (value == true)
                {
                    this.BackColor = Color.FromName("ActiveCaption");
                }
                else
                    this.BackColor = Color.FromName("Info");
            }
        }
        [Category("Custom Props")]
        public bool Removable
        {
            get { return removable; }
            set
            {
                removable = value;

                if (value == true && availability==true)
                {
                    this.BackColor = Color.FromName("InactiveCaption");
                }
                else
                {
                    if (availability==false)
                        this.BackColor = Color.FromName("Info");
                    else
                        this.BackColor = Color.FromName("ActiveCaption");
                }
            }
        }
        [Category("Custom Props")]
        public int Seating
        {
            get { return seating; }
            set { seating = value; lblMax.Text = "Max Seating: "+Convert.ToString(value); }
        }
        #endregion

        private void TableItem_Load(object sender, EventArgs e)
        {

        }
    }
}
