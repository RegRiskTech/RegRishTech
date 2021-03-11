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
    public partial class ListItemProjectCross : UserControl
    {
        public ListItemProjectCross()
        {
            InitializeComponent();
        }

        #region Properties
        private string _name;
        private Image _cross;


        [Category("Custom Props")]
        public string Title
        {
            get { return _name; }
            set { _name = value; projectLbl.Text = value; }
        }

        [Category("Custom Props")]
        public Image Cross
        {
            get { return _cross; }
            set { _cross = value; crossImg.Image = value; }
        }

        #endregion

        public EventHandler cross_Pressed;

        private void crossImg_Click(object sender, EventArgs e)
        {
            if (cross_Pressed != null)
            {
                cross_Pressed(this, e);
            }
        }

        private void crossImg_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.crossImg, "Remove Project");
        }
    }
}
