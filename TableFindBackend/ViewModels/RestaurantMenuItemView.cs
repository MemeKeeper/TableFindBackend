using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableFindBackend.ViewModels
{
    public partial class RestaurantMenuItemView : UserControl
    {
        public RestaurantMenuItemView()
        {
            InitializeComponent();
        }
        #region properties

        private string label;
        private bool enabled;
        private string type;
        private double price;

        [Category("Custom Props")]
        public string Label { get => label; set 
            {
                label = value;
                lblName.Text = value;
            } }
        [Category("Custom Props")]
        public bool Active { get => enabled; set 
            {
                enabled = value;
                this.BackColor = Color.FromName("ControlDark");
            } }
        [Category("Custom Props")]
        public string Type { get => type; set  
            {
                type = value; 
                switch(value)
                {
                    case ("Hot beverage"):
                        {
                            this.BackColor = Color.FromName("Chocolate");
                            lblName.ForeColor = Color.FromName("White");
                            break;
                        }
                    case ("Cold beverage"):
                        {
                            this.BackColor = Color.FromName("OceanBlue");
                            break;
                        }
                    case ("Alcoholic beverage"):
                        {
                            this.BackColor = Color.FromName("Black");
                            lblName.ForeColor = Color.FromName("White");
                            break;
                        }
                    case ("Light Meal"):
                        {
                            this.BackColor = Color.FromName("RosyBrown");
                            break;
                        }
                    case ("Kids Meal"):
                        {
                            this.BackColor = Color.FromName("Yellow");
                            break;
                        }
                    case ("Sandwich"):
                        {
                            this.BackColor = Color.FromName("DarkKhaki");
                            break;
                        }
                    case ("Dessert"):
                        {
                            this.BackColor = Color.FromName("LightCoral");
                            break;
                        }
                    case ("Breakfast"):
                        {
                            this.BackColor = Color.FromName("Khaki");
                            break;
                        }
                    case ("Grills"):
                        {
                            this.BackColor = Color.FromName("Sienna");
                            break;
                        }
                    case ("Burgers"):
                        {
                            this.BackColor = Color.FromName("Tan");
                            break;
                        }
                    case ("Salads"):
                        {
                            this.BackColor = Color.FromName("ForestGreen");
                            break;
                        }
                    case ("Seafood"):
                        {
                            this.BackColor = Color.FromName("Turquoise");
                            break;
                        }
                    default:
                        {
                            this.BackColor = Color.FromName("Control");
                            break;
                        }
                }
            } }

        public double Price { get => price; set => price = value; }

        public void resetColor()
        {
            this.Type = type;    //  <--- resets the color after the user selects it.
        }

        #endregion
    }
}
