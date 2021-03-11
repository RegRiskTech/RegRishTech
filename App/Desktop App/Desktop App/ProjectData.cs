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
    public partial class ProjectData : Form
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

        public ProjectData(string projectName)
        {
            InitializeComponent();
            projectNameLbl.Text = projectName;
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

        private void ProjectData_Load(object sender, EventArgs e)
        {
            this.SuspendLayout();

            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT OpenDate FROM [dbo].[Projects] WHERE ProjectName = @projectName", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridOpenDate.DataSource = dt;
                connection.Close();
            }

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT CloseDate FROM [dbo].[Projects] WHERE ProjectName = @projectName", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridCloseDate.DataSource = dt;
                connection.Close();
            }

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectTypeID FROM [dbo].[Projects] WHERE ProjectName = @projectName", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridProjectType.DataSource = dt;
                connection.Close();
            }

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT TypeBlock FROM [dbo].[Projects] WHERE ProjectName = @projectName", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridprojectBlock.DataSource = dt;
                connection.Close();
            }

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectDescription FROM [dbo].[Projects] WHERE ProjectName = @projectName", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridDescription.DataSource = dt;
                connection.Close();
            }

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
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

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectID FROM [dbo].[Projects] WHERE ProjectName = @projectName", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridProjectID.DataSource = dt;
                connection.Close();
            }

            mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT Closed FROM [dbo].[Projects] WHERE ProjectName = @projectName", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridClosed.DataSource = dt;
                connection.Close();
            }

            labelCountUsers.Text = dataGridProjectUsers.Rows.Count + " Members";

            int ellipseSize = 20;
            projectSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, projectSearchPanel.Width, projectSearchPanel.Height, ellipseSize, ellipseSize));
            projectUserSearchTxt.Text = "Search";
            projectUserSearchTxt.ForeColor = Color.Gray;
            projectSearchPanel.Visible = true;

            dataGridProjectUsers.Width = this.Width / 2 - 460;

            // Flow Panel Code
            populateList(dataGridProjectUsers);

            dataGridDescription.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dataGridDescription.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dataGridDescription.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridDescription.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            if (dataGridClosed.Rows[0].Cells[0].Value.ToString() == "0")
            {
                openLbl.Visible = true;
                closedLbl.Visible = false;
            }
            else
            {
                openLbl.Visible = false;
                closedLbl.Visible = true;
                panel1stRight.Visible = true;
            }

            this.ResumeLayout();
        }

        private void populateList(DataGridView dataTable)
        {
            flowLayoutPanelEmail.Controls.Clear();

            ListItem[] listItems = new ListItem[dataTable.Rows.Count];
            // loop through each item
            for (int i = 0; i < listItems.Length; i++)
            {


                listItems[i] = new ListItem();
                listItems[i].Name = "projectList";

                listItems[i].Title = dataTable.Rows[i].Cells[0].Value.ToString();
                listItems[i].Email = dataTable.Rows[i].Cells[1].Value.ToString();

                //listItems[i].Width = this.Width / 2 - 200;
                //listItems[i].Size = new Size(this.Width / 2 - 609, 60);

                flowLayoutPanelEmail.Controls.Add(listItems[i]);



                
                //listItems[i].Dock = DockStyle.Fill;

            }
            //removeDividerLabel();
            //divider();
        }

        private void divider()
        {
            int numRows = dataGridProjectUsers.Rows.Count;
            int x = this.Width / 2;
            int y = panelTopRight.Height + 34;

            if (numRows > 0)
            {
                for (int i = 0; i < numRows; i++)
                {
                    Label seperator = new Label();
                    seperator.Name = "seperatorLine";
                    this.Controls.Add(seperator);

                    seperator.Size = new Size(this.Width - 105, 1);
                    seperator.Location = new Point(x, y + 36);
                    seperator.BringToFront();
                    seperator.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    seperator.BackColor = Color.FromArgb(255, 150, 150, 150);

                    y += dataGridProjectUsers.Rows[0].Height;
                }
            }
        }

        private void removeDividerLabel()
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (Control item in this.Controls.OfType<Label>())
                {
                    if (item.Name != "projectNameLbl")
                    {
                        this.Controls.Remove(item);
                    }
                }
            }
        }


        private void projectUserSearchTxt_TextChanged(object sender, EventArgs e)
        {
            if (projectUserSearchTxt.Text != "Search")
            {
                // Select * from Members WHERE "";

                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT Member,Email FROM [dbo].[ProjectMembers] WHERE ProjectID = @projectID AND (Member LIKE @member + '%' OR Email LIKE @email + '%') ORDER BY Member ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectID", projectNameLbl.Text);
                    sqlcomm.Parameters.AddWithValue("@member", projectUserSearchTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@email", projectUserSearchTxt.Text);
                    sqlcomm.ExecuteNonQuery();

                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridProjectUsers.DataSource = dt;

                    connection.Close();
                }
                populateList(dataGridProjectUsers);
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
            /*if (projectUserSearchTxt.Text == "Search")
            {
                projectUserSearchTxt.Text = string.Empty;
                projectUserSearchTxt.ForeColor = Color.Black;
            }*/
        }

        private void ProjectData_Resize(object sender, EventArgs e)
        {
            //panelRight.Location
            panelRight.Width = this.Width / 2 + 50;
            panelRight.Height = this.Height + 10;

            flowLayoutPanelEmail.Width = this.Width / 2 - 14;
            flowLayoutPanelEmail.Height = this.Height - 126;

            projectNameLbl.Width = this.Width / 2 - 94;
            pane4th.Width = this.Width / 2 - 69;
            //dataGridDescription.Height = 200;

            editUsersLbl.Location = new Point(this.Width / 2 - 180, editUsersLbl.Height + 35);

            int ellipseSize = 20;
            projectSearchPanel.Width = this.Width / 2 - 60;
            projectSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, projectSearchPanel.Width, projectSearchPanel.Height, ellipseSize, ellipseSize));

        }

        private void deleteProjectLbl_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Are you sure you want to delete " + projectNameLbl.Text + "?", "", MessageBoxButtons.OKCancel))
            {
                case DialogResult.OK:
                    string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("DELETE FROM Projects WHERE ProjectID = @projectName", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@projectName", dataGridProjectID.Rows[0].Cells[0].Value.ToString());
                        sqlcomm.ExecuteNonQuery();
                        connection.Close();
                    }

                    mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("DELETE FROM ProjectMembers WHERE ProjectID = @projectName", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@projectName", projectNameLbl.Text);
                        sqlcomm.ExecuteNonQuery();
                        connection.Close();
                    }
                    openChildForm(new ProjectForm());
                    break;

                case DialogResult.Cancel: break;
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

        private void editUsers_Click(object sender, EventArgs e)
        {
            openChildForm(new ProjectAddUsers(projectNameLbl.Text));
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new ProjectForm());
        }

        private void editProjectLbl_Click(object sender, EventArgs e)
        {
            openChildForm(new ProjectAdd(projectNameLbl.Text));
        }

        private void closedLbl_MouseClick(object sender, MouseEventArgs e)
        {
            switch (MessageBox.Show("Are you sure you want open " + projectNameLbl.Text + "?", "", MessageBoxButtons.OKCancel))
            {
                case DialogResult.OK:
                    string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("UPDATE Projects Set Closed = '0' WHERE ProjectID = @projectID", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@projectID", dataGridProjectID.Rows[0].Cells[0].Value.ToString());

                        sqlcomm.ExecuteNonQuery();

                        connection.Close();
                    }

                    openLbl.Visible = true;
                    closedLbl.Visible = false;
                    
                    break;

                case DialogResult.Cancel: break;
            }
        }

        private void openLbl_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Are you sure you want close " + projectNameLbl.Text + "?", "", MessageBoxButtons.OKCancel))
            {
                case DialogResult.OK:
                    string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("UPDATE Projects Set Closed = '1' WHERE ProjectID = @projectID", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@projectID", dataGridProjectID.Rows[0].Cells[0].Value.ToString());

                        sqlcomm.ExecuteNonQuery();

                        connection.Close();
                    }

                    closedLbl.Visible = true;
                    openLbl.Visible = false;
                    break;

                case DialogResult.Cancel: break;
            }
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
    }
}
