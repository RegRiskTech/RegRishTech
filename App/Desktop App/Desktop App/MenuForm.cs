using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_App
{
    public partial class HomeScreen : Form
    {
        private static Form activeForm = null;
        public HomeScreen()
        {
            InitializeComponent();
            openChildForm(new HomeForm());
        }

        private void HomeScreen_Load(object sender, EventArgs e)
        {
        
        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {
            //...#
            //Your Code
            //..
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnHome.Height;
            pnlNav.Top = btnHome.Top;
            pnlNav.Left = btnHome.Left;
            openChildForm(new HomeForm());
        }

        private void btnProjects_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnProjects.Height;
            pnlNav.Top = btnProjects.Top;
            pnlNav.Left = btnProjects.Left;
            openChildForm(new ProjectForm());
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnUsers.Height;
            pnlNav.Top = btnUsers.Top;
            pnlNav.Left = btnUsers.Left;
            openChildForm(new UserForm());
        }


        private void btnDashboard_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            openChildForm(new Reporting());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnSettings.Height;
            pnlNav.Top = btnSettings.Top;
            pnlNav.Left = btnSettings.Left;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelHomeForm.Controls.Add(childForm);
            panelHomeForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

    }
}
