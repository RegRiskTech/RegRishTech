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
    public partial class ListItemProjectTick : UserControl
    {
        public ListItemProjectTick()
        {
            InitializeComponent();
        }

        #region Properties
        private string _name;
        private Image _tick;


        [Category("Custom Props")]
        public string Title
        {
            get { return _name; }
            set { _name = value; projectLbl.Text = value; }
        }

        [Category("Custom Props")]
        public Image Tick
        {
            get { return _tick; }
            set { _tick = value; tickImg.Image = value; }
        }

        #endregion

        public EventHandler tick_Pressed;

        private void tickImg_Click(object sender, EventArgs e)
        {
            if (tick_Pressed != null)
            {
                tick_Pressed(this, e);
            }
        }

        private void tickImg_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.tickImg, "Add Project");
        }
    }
}
