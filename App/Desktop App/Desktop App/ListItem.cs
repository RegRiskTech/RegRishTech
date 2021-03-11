using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_App
{
    public partial class ListItem : UserControl
    {
        public ListItem()
        {
            InitializeComponent();
        }

        #region Properties
        private string _name;
        private string _email;
        private Panel _divider;


        [Category("Custom Props")]
        public string Title
        {
            get { return _name; }
            set { _name = value; nameLbl.Text = value; }
        }

        [Category("Custom Props")]
        public string Email
        {
            get { return _email; }
            set { _email = value; emailLbl.Text = value; }
        }

        public Panel Divider
        {
            get { return _divider; }
            set { _divider = value; }
        }
        #endregion

        private void ListItem_Resize(object sender, EventArgs e)
        {
            //divider.Width = this.Width - 179;
        }

        private void ListItem_Load(object sender, EventArgs e)
        {
            //this.Dock = DockStyle.Top;
            this.Anchor = AnchorStyles.Right;/* | AnchorStyles.Left | AnchorStyles.Right;*/
        }
    }
}
