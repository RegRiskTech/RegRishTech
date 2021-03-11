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
    public partial class ProjectAddUsers : Form
    {
        public ProjectAddUsers(string projectName)
        {
            InitializeComponent();
            projectNameLbl.Text = projectName;
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

        private void dataGridAvailableRefresh()
        {
            if (projectUserSearchAvailableTxt.Text != "Search")
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT CONCAT(FirstName, ' ', LastName) AS FullName, Email FROM Members WHERE Email NOT IN(SELECT Email FROM[dbo].[ProjectMembers] WHERE ProjectID = @projectName) AND(FirstName LIKE @firstName + '%' OR LastName LIKE @lastName + '%' OR Email LIKE @email + '%') ORDER BY FullName ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                    sqlcomm.Parameters.AddWithValue("@firstName", projectUserSearchAvailableTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@lastName", projectUserSearchAvailableTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@email", projectUserSearchAvailableTxt.Text);
                    sqlcomm.ExecuteNonQuery();

                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridProjectUsersAvailable.DataSource = dt;

                    connection.Close();
                }
            }
            else
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT CONCAT(FirstName, ' ', LastName) AS FullName, Email FROM Members WHERE Email NOT IN(SELECT Email FROM[dbo].[ProjectMembers] WHERE ProjectID = @projectName) ORDER BY FullName ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);

                    sqlcomm.ExecuteNonQuery();
                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridProjectUsersAvailable.DataSource = dt;
                    connection.Close();
                }
            }
            dataGridProjectUsersAvailable.Columns[1].Visible = false;
            populateListTick(dataGridProjectUsersAvailable);
        }

        private void dataGridCurrentRefresh()
        {
            if (projectUserSearchTxt.Text != "Search")
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT Member, Email FROM [dbo].[ProjectMembers] WHERE ProjectID = @projectName AND (Member LIKE @firstName + '%' OR SUBSTRING(Member, CHARINDEX(' ', Member) + 1, LEN(Member)) LIKE @lastName + '%' OR Email LIKE @email + '%') ORDER BY Member ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                    sqlcomm.Parameters.AddWithValue("@firstName", projectUserSearchTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@lastName", projectUserSearchTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@email", projectUserSearchTxt.Text);
                    sqlcomm.ExecuteNonQuery();

                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridProjectUsers.DataSource = dt;

                    connection.Close();
                }
            }
            else
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT Member, Email FROM [dbo].[ProjectMembers] WHERE ProjectID = @projectName ORDER BY Member ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                    sqlcomm.ExecuteNonQuery();
                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridProjectUsers.DataSource = dt;

                    connection.Close();
                }
            }
            dataGridProjectUsers.Columns[1].Visible = false;
            populateListCross(dataGridProjectUsers);
        }

        public void populateListCross(DataGridView dataTable)
        {
            flowLayoutPanelEmail.Controls.Clear();
            ListItemCross[] listItems = new ListItemCross[dataTable.Rows.Count];
            flowLayoutPanelEmail.Visible = false;

            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new ListItemCross();

                string email = dataTable.Rows[i].Cells[1].Value.ToString();
                listItems[i].Title = dataTable.Rows[i].Cells[0].Value.ToString();
                listItems[i].Email = email;
                listItems[i].cross_Pressed += (sender, EventArgs) => { crossBtnPress(sender, EventArgs, email); };

                flowLayoutPanelEmail.Controls.Add(listItems[i]);
            }
            flowLayoutPanelEmail.Visible = true;
        }

        private void crossBtnPress(object sender, EventArgs e, string email)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("DELETE FROM dbo.ProjectMembers WHERE ProjectID = @projectName AND Email = @email", connection))
            {
                connection.Open();
                
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                sqlcomm.Parameters.AddWithValue("@email", email);
                sqlcomm.ExecuteNonQuery();

                connection.Close();
            }
            dataGridCurrentRefresh();
            dataGridAvailableRefresh();
        }

        private void populateListTick(DataGridView dataTable)
        {
            flowLayoutPanelAvailable.Controls.Clear();
            ListItemTick[] listItems = new ListItemTick[dataTable.Rows.Count];
            flowLayoutPanelAvailable.Visible = false;

            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new ListItemTick();

                string fullName = dataTable.Rows[i].Cells[0].Value.ToString();
                string email = dataTable.Rows[i].Cells[1].Value.ToString();
                listItems[i].Title = fullName;
                listItems[i].Email = email;
                listItems[i].tick_Pressed += (sender, EventArgs) => { tickBtnPress(sender, EventArgs, fullName, email); };

                flowLayoutPanelAvailable.Controls.Add(listItems[i]);
            }
            flowLayoutPanelAvailable.Visible = true;
        }

        private void tickBtnPress(object sender, EventArgs e, string fullName, string email)
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
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                sqlcomm.Parameters.AddWithValue("@fullName", fullName);
                sqlcomm.Parameters.AddWithValue("@email", email);

                sqlcomm.ExecuteNonQuery();

                connection.Close();
            }
            dataGridCurrentRefresh();
            dataGridAvailableRefresh();
        }

        private void ProjectAddUsers_Load(object sender, EventArgs e)
        {
            dataGridCurrentRefresh();
            dataGridAvailableRefresh();

            int ellipseSize = 20;
            projectSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, projectSearchPanel.Width, projectSearchPanel.Height, ellipseSize, ellipseSize));
            projectUserSearchTxt.Text = "Search";
            projectUserSearchTxt.ForeColor = Color.Gray;
            projectSearchPanel.Visible = true;

            panelSeachAvailable.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panelSeachAvailable.Width, panelSeachAvailable.Height, ellipseSize, ellipseSize));
            projectUserSearchAvailableTxt.Text = "Search";
            projectUserSearchAvailableTxt.ForeColor = Color.Gray;
            panelSeachAvailable.Visible = true;

            projectNameInsidersLbl.Text = projectNameLbl.Text + " Members";
        }

        private void ProjectAddUsers_Resize(object sender, EventArgs e)
        {
            panelRight.Width = this.Width / 2 + 50;
            panelRight.Height = this.Height + 10;

            flowLayoutPanelEmail.Width = this.Width / 2 - 14;
            flowLayoutPanelEmail.Height = this.Height - 126;

            flowLayoutPanelAvailable.Width = this.Width / 2 - 38;
            flowLayoutPanelAvailable.Height = this.Height - 126;

            int ellipseSize = 20;
            projectSearchPanel.Width = this.Width / 2 - 60;
            projectSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, projectSearchPanel.Width, projectSearchPanel.Height, ellipseSize, ellipseSize));

            panelSeachAvailable.Width = this.Width / 2 - 68;
            panelSeachAvailable.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panelSeachAvailable.Width, panelSeachAvailable.Height, ellipseSize, ellipseSize));

        }

        private void projectUserSearchTxt_TextChanged(object sender, EventArgs e)
        {
            if (projectUserSearchTxt.Text != "Search")
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT Member, Email FROM [dbo].[ProjectMembers] WHERE ProjectID = @projectName AND (Member LIKE @firstName + '%' OR SUBSTRING(Member, CHARINDEX(' ', Member) + 1, LEN(Member)) LIKE @lastName + '%' OR Email LIKE @email + '%') ORDER BY Member ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                    sqlcomm.Parameters.AddWithValue("@firstName", projectUserSearchTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@lastName", projectUserSearchTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@email", projectUserSearchTxt.Text);
                    sqlcomm.ExecuteNonQuery();

                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridProjectUsers.DataSource = dt;

                    connection.Close();
                }

                dataGridProjectUsers.Columns[1].Visible = false;
                populateListCross(dataGridProjectUsers);
            }
        }

        private void projectUserSearchTxt_MouseClick(object sender, MouseEventArgs e)
        {
            if (projectUserSearchTxt.Text == "Search")
            {
                projectUserSearchTxt.Text = string.Empty;
                projectUserSearchTxt.ForeColor = Color.Black;
            }
        }

        private void projectUserSearchTxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (projectUserSearchTxt.Text == "Search")
            {
                projectUserSearchTxt.Text = string.Empty;
                projectUserSearchTxt.ForeColor = Color.Black;
            }
        }

        private void projectUserSearchAvailableTxt_TextChanged(object sender, EventArgs e)
        {
            if (projectUserSearchAvailableTxt.Text != "Search")
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT CONCAT(FirstName, ' ', LastName) AS FullName, Email FROM Members WHERE Email NOT IN(SELECT Email FROM[dbo].[ProjectMembers] WHERE ProjectID = '" + projectNameLbl.Text + "') AND (FirstName LIKE @firstName + '%' OR LastName LIKE @lastName + '%' OR Email LIKE @email + '%') ORDER BY FullName ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@firstName", projectUserSearchAvailableTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@lastName", projectUserSearchAvailableTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@email", projectUserSearchAvailableTxt.Text);
                    sqlcomm.ExecuteNonQuery();

                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridProjectUsersAvailable.DataSource = dt;

                    connection.Close();
                }
                dataGridProjectUsersAvailable.Columns[1].Visible = false;
                populateListTick(dataGridProjectUsersAvailable);
            }
        }

        private void projectUserSearchAvailableTxt_MouseClick(object sender, MouseEventArgs e)
        {
            if (projectUserSearchAvailableTxt.Text == "Search")
            {
                projectUserSearchAvailableTxt.Text = string.Empty;
                projectUserSearchAvailableTxt.ForeColor = Color.Black;
            }
        }
        private void projectUserSearchAvailableTxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (projectUserSearchAvailableTxt.Text == "Search")
            {
                projectUserSearchAvailableTxt.Text = string.Empty;
                projectUserSearchAvailableTxt.ForeColor = Color.Black;
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
            openChildForm(new ProjectData(projectNameLbl.Text));
        }

        private void flowLayoutPanelEmail_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutPanelEmail.SuspendLayout();
            foreach (Control ctrl in flowLayoutPanelEmail.Controls.OfType<Control>())
            {
                ctrl.Width = flowLayoutPanelEmail.Width - 40;
            }
            flowLayoutPanelEmail.ResumeLayout();
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
