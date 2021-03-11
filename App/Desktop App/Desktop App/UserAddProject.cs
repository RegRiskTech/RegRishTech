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
using System.Data.SqlClient;
using System.Configuration;

namespace Desktop_App
{
    public partial class UserAddProject : Form
    {
        public UserAddProject(string fullName, string email)
        {
            InitializeComponent();
            userNameLbl.Text = fullName;
            emailLbl.Text = email;

            panelSeachAvailable.BringToFront();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeft,
            int nTop,
            int nRight,
            int nBottom,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void SQL_Data(string sqlquery, DataGridView dataGridTable)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            SqlConnection sqlconnn = new SqlConnection(mainconn);
            sqlconnn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconnn);
            SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            sdr.Fill(dt);
            dataGridTable.DataSource = dt;
            sqlconnn.Close();
        }

        private void addImg_Click(object sender, EventArgs e)
        {
            string index = ((PictureBox)sender).Name.Substring(11);
            bool isConvertible = false;
            int myInt = 0;
            isConvertible = int.TryParse(index, out myInt);
            var firstDisplayedRowIndex = dataGridProjectsAvailable.FirstDisplayedCell.RowIndex;
            myInt = myInt + firstDisplayedRowIndex;

            string sqlquery = "SELECT MAX(ID) FROM ProjectMembers";
            SQL_Data(sqlquery, dataGridProjectIDMax);
            dataGridProjectIDMax.ClearSelection();

            newProjectIDLbl.Text = dataGridProjectIDMax.CurrentRow.Cells[0].Value.ToString();
            int projectIDMax = Convert.ToInt32(newProjectIDLbl.Text);
            int newProjectID = projectIDMax + 1;
            newProjectIDLbl.Text = newProjectID.ToString();

            string projectName = dataGridProjectsAvailable.Rows[myInt].Cells["ProjectName"].Value.ToString();

            sqlquery = "INSERT INTO dbo.ProjectMembers(ID, ProjectID, Member, Email) VALUES('" + newProjectIDLbl.Text + "', '" + projectName + "', '" + userNameLbl.Text + "', '" + emailLbl.Text + "');";
            ExecuteQuery(sqlquery);

            dataGridCurrentRefresh();
            dataGridAvailableRefresh();
        }

        private void ExecuteQuery(string sqlquery)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            SqlConnection sqlconnn = new SqlConnection(mainconn);
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconnn);
            sqlconnn.Open();
            sqlcomm.ExecuteNonQuery();
            sqlconnn.Close();
        }

        private void dataGridAvailableRefresh()
        {
            if (UserSearchAvailableTxt.Text != "Search")
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectName FROM Projects WHERE ProjectName NOT IN (Select ProjectID FROM [dbo].[ProjectMembers] WHERE EMAIL = '" + emailLbl.Text + "') AND ProjectName LIKE @projectName + '%' ORDER BY ProjectName ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectName", UserSearchAvailableTxt.Text);
                    sqlcomm.ExecuteNonQuery();
                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridProjectsAvailable.DataSource = dt;
                    connection.Close();
                }
            }
            else
            {
                string sqlquery = "SELECT ProjectName FROM Projects WHERE ProjectName NOT IN (Select ProjectID FROM [dbo].[ProjectMembers] WHERE EMAIL = '" + emailLbl.Text + "') ORDER BY ProjectID ASC";
                SQL_Data(sqlquery, dataGridProjectsAvailable);
            }

            dataGridProjectsAvailable.Columns[0].Visible = false;
            populateListTick(dataGridProjectsAvailable);
        }

        private void dataGridCurrentRefresh()
        {
            if (UserSearchTxt.Text != "Search")
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectID FROM [dbo].[ProjectMembers] WHERE Email = '" + emailLbl.Text + "' AND (ProjectID LIKE @projectID + '%') ORDER BY ProjectID ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectID", UserSearchTxt.Text);
                    sqlcomm.ExecuteNonQuery();
                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridProjects.DataSource = dt;
                    connection.Close();
                }
            }
            else
            {
                string sqlquery = "SELECT ProjectID FROM [dbo].[ProjectMembers] WHERE Email ='" + emailLbl.Text + "' ORDER BY ProjectID ASC";
                SQL_Data(sqlquery, dataGridProjects);
            }

            dataGridProjects.Columns[0].Visible = false;
            populateListCross(dataGridProjects);
        }

        public void populateListCross(DataGridView dataTable)
        {
            flowLayoutPanelProjects.Controls.Clear();
            ListItemProjectCross[] listItems = new ListItemProjectCross[dataTable.Rows.Count];
            flowLayoutPanelProjects.Visible = false;

            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new ListItemProjectCross();

                string project = dataTable.Rows[i].Cells[0].Value.ToString();
                listItems[i].Title = dataTable.Rows[i].Cells[0].Value.ToString();
                listItems[i].cross_Pressed += (sender, EventArgs) => { crossBtnPress(sender, EventArgs, project); };

                flowLayoutPanelProjects.Controls.Add(listItems[i]);
            }
            flowLayoutPanelProjects.Visible = true;
        }

        private void crossBtnPress(object sender, EventArgs e, string project)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("DELETE FROM dbo.ProjectMembers WHERE ProjectID = @projectName AND Email = @email", connection))
            {
                connection.Open();

                sqlcomm.Parameters.AddWithValue("@projectName", project);
                sqlcomm.Parameters.AddWithValue("@email", emailLbl.Text);
                sqlcomm.ExecuteNonQuery();

                connection.Close();
            }
            dataGridCurrentRefresh();
            dataGridAvailableRefresh();
        }

        private void populateListTick(DataGridView dataTable)
        {
            flowLayoutPanelAvailable.Controls.Clear();
            ListItemProjectTick[] listItems = new ListItemProjectTick[dataTable.Rows.Count];
            flowLayoutPanelAvailable.Visible = false;

            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new ListItemProjectTick();

                string project = dataTable.Rows[i].Cells[0].Value.ToString();
                listItems[i].Title = project;
                listItems[i].tick_Pressed += (sender, EventArgs) => { tickBtnPress(sender, EventArgs, project); };

                flowLayoutPanelAvailable.Controls.Add(listItems[i]);
            }
            flowLayoutPanelAvailable.Visible = true;
        }

        private void tickBtnPress(object sender, EventArgs e, string project)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT MAX(ID) FROM ProjectMembers", connection))
            {
                connection.Open();
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridProjectIDMax.DataSource = dt;
                connection.Close();
            }
            dataGridProjectIDMax.ClearSelection();

            newProjectIDLbl.Text = dataGridProjectIDMax.CurrentRow.Cells[0].Value.ToString();
            int projectIDMax = Convert.ToInt32(newProjectIDLbl.Text);
            int newProjectID = projectIDMax + 1;
            newProjectIDLbl.Text = newProjectID.ToString();

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("INSERT INTO dbo.ProjectMembers(ID, ProjectID, Member, Email) VALUES (@projectID, @projectName, @fullName, @email)", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@projectID", newProjectIDLbl.Text);
                sqlcomm.Parameters.AddWithValue("@projectName", project);
                sqlcomm.Parameters.AddWithValue("@fullName", userNameLbl.Text);
                sqlcomm.Parameters.AddWithValue("@email", emailLbl.Text);

                sqlcomm.ExecuteNonQuery();

                connection.Close();
            }
            dataGridCurrentRefresh();
            dataGridAvailableRefresh();
        }

        private void UserAddProject_Load(object sender, EventArgs e)
        {
            dataGridCurrentRefresh();
            dataGridAvailableRefresh();

            int ellipseSize = 20;
            userSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, userSearchPanel.Width, userSearchPanel.Height, ellipseSize, ellipseSize));
            UserSearchTxt.Text = "Search";
            UserSearchTxt.ForeColor = Color.Gray;
            userSearchPanel.Visible = true;

            panelSeachAvailable.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panelSeachAvailable.Width, panelSeachAvailable.Height, ellipseSize, ellipseSize));
            UserSearchAvailableTxt.Text = "Search";
            UserSearchAvailableTxt.ForeColor = Color.Gray;
            panelSeachAvailable.Visible = true;
        }

        private void UserAddProject_Resize(object sender, EventArgs e)
        {
            panelRight.Width = this.Width / 2 + 50;
            panelRight.Height = this.Height + 10;

            flowLayoutPanelProjects.Width = this.Width / 2 - 14;
            flowLayoutPanelProjects.Height = this.Height - 126;

            flowLayoutPanelAvailable.Width = this.Width / 2 - 38;
            flowLayoutPanelAvailable.Height = this.Height - 126;


            int ellipseSize = 20;
            userSearchPanel.Width = this.Width / 2 - 60;
            userSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, userSearchPanel.Width, userSearchPanel.Height, ellipseSize, ellipseSize));

            panelSeachAvailable.Width = this.Width / 2 - 68;
            panelSeachAvailable.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panelSeachAvailable.Width, panelSeachAvailable.Height, ellipseSize, ellipseSize));
        }

        private void UserSearchTxt_TextChanged(object sender, EventArgs e)
        {
            if (UserSearchTxt.Text != "Search")
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectID FROM [dbo].[ProjectMembers] WHERE Email = '" + emailLbl.Text + "' AND ProjectID LIKE @projectID + '%' ORDER BY ProjectID ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectID", UserSearchTxt.Text);
                    sqlcomm.ExecuteNonQuery();
                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridProjects.DataSource = dt;
                    connection.Close();
                }
                populateListCross(dataGridProjects);
            }
        }

        private void UserSearchTxt_MouseClick(object sender, MouseEventArgs e)
        {
            if (UserSearchTxt.Text == "Search")
            {
                UserSearchTxt.Text = string.Empty;
                UserSearchTxt.ForeColor = Color.Black;
            }
        }

        private void UserSearchTxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (UserSearchTxt.Text == "Search")
            {
                UserSearchTxt.Text = string.Empty;
                UserSearchTxt.ForeColor = Color.Black;
            }
        }

        private void UserSearchAvailableTxt_MouseClick(object sender, MouseEventArgs e)
        {
            if (UserSearchAvailableTxt.Text == "Search")
            {
                UserSearchAvailableTxt.Text = string.Empty;
                UserSearchAvailableTxt.ForeColor = Color.Black;
            }
        }
        private void UserSearchAvailableTxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (UserSearchAvailableTxt.Text == "Search")
            {
                UserSearchAvailableTxt.Text = string.Empty;
                UserSearchAvailableTxt.ForeColor = Color.Black;
            }
        }

        private void UserSearchAvailableTxt_TextChanged(object sender, EventArgs e)
        {
            if (UserSearchAvailableTxt.Text != "Search")
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectName FROM Projects WHERE ProjectName NOT IN (Select ProjectID FROM [dbo].[ProjectMembers] WHERE EMAIL = '" + emailLbl.Text + "') AND ProjectName LIKE @projectName + '%' ORDER BY ProjectName ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectName", UserSearchAvailableTxt.Text);
                    sqlcomm.ExecuteNonQuery();
                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridProjectsAvailable.DataSource = dt;
                    connection.Close();
                }
                populateListTick(dataGridProjectsAvailable);
            }
        }

        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            Program.homescreen.panelHomeForm.Controls.Add(childForm);
            Program.homescreen.panelHomeForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new UserData(userNameLbl.Text, emailLbl.Text));
        }

        private void flowLayoutPanelProjects_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutPanelProjects.SuspendLayout();
            foreach (Control ctrl in flowLayoutPanelProjects.Controls.OfType<Control>())
            {
                ctrl.Width = flowLayoutPanelProjects.Width - 40;
            }
            flowLayoutPanelProjects.ResumeLayout();
        }

        private void flowLayoutPanelAvailable_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutPanelAvailable.SuspendLayout();
            foreach (Control ctrl in flowLayoutPanelAvailable.Controls.OfType<Control>())
            {
                ctrl.Width = flowLayoutPanelAvailable.Width - 40;
            }
            flowLayoutPanelAvailable.ResumeLayout();
        }
    }
}
