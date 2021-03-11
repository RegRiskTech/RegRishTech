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
    public partial class ListItemProject : UserControl
    {
        public ListItemProject()
        {
            InitializeComponent();
        }

        #region Properties
        private string _project;
        private string _desc;
        private Image _status;


        [Category("Custom Props")]
        public string Project
        {
            get { return _project; }
            set { _project = value; projectLbl.Text = value; }
        }

        [Category("Custom Props")]
        public string Description
        {
            get { return _desc; }
            set { _desc = value; descLbl.Text = value; }
        }

        [Category("Custom Props")]
        public Image Status
        {
            get { return _status; }
            set { _status = value; statusImg.Image = value; }
        }
        #endregion

        private void ListItemProject_Load(object sender, EventArgs e)
        {

        }
    }
}
