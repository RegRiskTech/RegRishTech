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
    public partial class UserData : Form
    {
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
        public UserData(string userName, string userEmail)
        {
            InitializeComponent();
            userNameLbl.Text = userName;
            emailHiddenLbl.Text = userEmail;
        }

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

        private void divider(DataGridView dataGridTable)
        {
            int numRows = dataGridTable.Rows.GetRowCount(DataGridViewElementStates.Visible);
            int x = this.Width / 2 + 33;
            int y = panelTopRight.Height + 8;

            if (numRows > 0)
            {
                for (int i = 0; i < numRows; i++)
                {
                    Label seperator = new Label();
                    seperator.Name = "seperatorLine";
                    this.Controls.Add(seperator);

                    seperator.Size = new Size(this.Width / 2 - 15, 1);
                    seperator.Location = new Point(x, y + 36);
                    seperator.BringToFront();
                    seperator.Anchor = (AnchorStyles.Top);
                    seperator.BackColor = Color.FromArgb(255, 150, 150, 150);

                    y += dataGridTable.Rows[0].Height;
                }
            }
            panelTopRight.BringToFront();
            labelCountProjects.BringToFront();
            userSearchPanel.BringToFront();
        }

        private void removeDividerLabel()
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (Control item in this.Controls.OfType<Label>())
                {
                    if (item.Name == "seperatorLine")
                    {
                        this.Controls.Remove(item);
                    }
                }
            }
        }

        private void userData_Load(object sender, EventArgs e)
        {
            string email = emailHiddenLbl.Text;

            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT Email FROM [dbo].[Members] WHERE Email = @email", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@email", email);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridEmail.DataSource = dt;
                connection.Close();
            }

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT Department FROM [dbo].[Members] WHERE Email = @email", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@email", email);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridDepartment.DataSource = dt;
                connection.Close();
            }

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT MemberTypeID FROM [dbo].[Members] WHERE Email = @email", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@email", email);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridType.DataSource = dt;
                connection.Close();
            }

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectID FROM [dbo].[ProjectMembers] WHERE Email = @email ORDER BY ProjectID ASC", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@email", email);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridUserProjects.DataSource = dt;
                connection.Close();
            }
            divider(dataGridUserProjects);

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT MemberID FROM [dbo].[Members] WHERE Email = @email", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@email", email);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridUserID.DataSource = dt;
                connection.Close();
            }

            labelCountProjects.Text = dataGridUserProjects.Rows.Count + " Projects";

            int ellipseSize = 20;
            userSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, userSearchPanel.Width, userSearchPanel.Height, ellipseSize, ellipseSize));
            projectUserSearchTxt.Text = "Search";
            projectUserSearchTxt.ForeColor = Color.Gray;
            userSearchPanel.Visible = true;

            dataGridUserProjects.Width = this.Width / 2 - 460;

            panelTopRight.BringToFront();
            labelCountProjects.BringToFront();
            userSearchPanel.BringToFront();


            dataGridEmail.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
        }

        private void userData_Resize(object sender, EventArgs e)
        {
            panelRight.Width = this.Width / 2 + 50;
            panelRight.Height = this.Height + 10;

            dataGridUserProjects.Width = this.Width / 2 - 34;
            dataGridUserProjects.Height = this.Height - 116;

            userNameLbl.Width = this.Width / 2 - 69;

            panel1st.Width = this.Width / 2 - 69;

            editProjectsLbl.Location = new Point(this.Width / 2 - 180, editProjectsLbl.Height + 35);

            int ellipseSize = 20;
            userSearchPanel.Width = this.Width / 2 - 60;
            userSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, userSearchPanel.Width, userSearchPanel.Height, ellipseSize, ellipseSize));

            foreach (Control item in this.Controls.OfType<Label>())
            {
                if (item.Name == "seperatorLine")
                {
                    item.Width = this.Width / 2 - 75;
                }
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

        private void projectUserSearchTxt_TextChanged_1(object sender, EventArgs e)
        {
            if (projectUserSearchTxt.Text != "Search")
                {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectID FROM [dbo].[ProjectMembers] WHERE Email = @email AND ProjectID LIKE @projectID + '%' ORDER BY ProjectID ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@email", emailHiddenLbl.Text);
                    sqlcomm.Parameters.AddWithValue("@projectID", projectUserSearchTxt.Text);
                    sqlcomm.ExecuteNonQuery();
                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridUserProjects.DataSource = dt;
                    connection.Close();
                }

                dataGridUserProjects.ClearSelection();
                removeDividerLabel();
                divider(dataGridUserProjects);
            }
        }

        private void projectUserSearchTxt_MouseClick_1(object sender, MouseEventArgs e)
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
        private void deleteProjectLbl_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Are you sure you want to delete " + userNameLbl.Text + "?", "", MessageBoxButtons.OKCancel))
            {
                case DialogResult.OK:
                    string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("DELETE FROM Members WHERE MemberID = @userName", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@userName", dataGridUserID.Rows[0].Cells[0].Value.ToString());
                        sqlcomm.ExecuteNonQuery();
                        connection.Close();
                    }

                    mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("DELETE FROM ProjectMembers WHERE Email = @email", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@email", emailHiddenLbl.Text);
                        sqlcomm.ExecuteNonQuery();
                        connection.Close();
                    }

                    openChildForm(new UserForm()); 
                    break;

                case DialogResult.Cancel: break;
            }
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

        private void editLabel_Click(object sender, EventArgs e)
        {
            openChildForm(new userAdd(emailHiddenLbl.Text));
        }

        private void editProjectsLbl_Click(object sender, EventArgs e)
        {
            openChildForm(new UserAddProject(userNameLbl.Text, emailHiddenLbl.Text));
        }

        private void dataGridEmail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
        }
    }
}
