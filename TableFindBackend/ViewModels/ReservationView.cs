using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableFindBackend.ViewModels
{
    public partial class ReservationView : UserControl
    {
        #region properties
        private string uName;
        private string uContactNumber;
        private string tTableName;
        private string tDate;
        private string tTime;
        private string objectId;
        private string uObjectId;
        private bool blinking = true;

        [Category("Custom Prop")]
        public string UserName
        {
            get => uName; set
            {
                uName = value;
                lblName.Text = value;
            }
        }
        [Category("Custom Prop")]
        public string UserContactNumber
        {
            get => uContactNumber; set
            {
                uContactNumber = value;
                lblContact.Text = value;
            }
        }
        [Category("Custom Prop")]
        public string TableName
        {
            get => tTableName; set
            {
                tTableName = value;
                lblTable.Text = value;
            }
        }
        [Category("Custom Prop")]
        public string Date
        {
            get => tDate; set
            {
                tDate = value;
                lblDate.Text = value;
            }
        }
        [Category("Custom Prop")]
        public string FromToTime
        {
            get => tTime; set
            {
                tTime = value;
                lblTime.Text = value;
            }
        }
        [Category("Custom Prop")]
        public string ObjectId
        {
            get => objectId; set
            {
                objectId = value;
                lblId.Text = value;
            }
        }
        [Category("Custom Prop")]
        public string UObjectId { get => uObjectId; set => uObjectId = value; }


        #endregion

        public ReservationView()
        {
            InitializeComponent();
        }
        public string Selected()
        {
            this.BackColor = Color.FromArgb(187, 222, 251);
            return this.objectId;
        }
        public async void Deselected()
        {
            blinking = false;
            await Task.Delay(500);
            this.BackColor = Color.FromName("Control");

        }
        public void New()
        {
            Blink();
        }
        private async void Blink()
        {
            while (blinking)
            {
                await Task.Delay(500);
                this.BackColor = this.BackColor == Color.SandyBrown ? Color.PeachPuff : Color.SandyBrown;
            }
        }
        public void Removed()
        {
            this.BackColor = Color.FromArgb(192, 192, 192);
        }


    }
}
