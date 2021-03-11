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
    public partial class ProjectForm : Form
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

        public ProjectForm()
        {
            InitializeComponent();
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {

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
            columnFont(dataGridTable);
            dataGridTable.ClearSelection();
        }

        private void divider(DataGridView dataGridTable)
        {
            int numRows = dataGridTable.Rows.GetRowCount(DataGridViewElementStates.Visible);
            int x = 46;
            int y = panelTop.Height + 34;

            if (numRows > 0)
            {
                for (int i = 0; i < numRows; i++)
                {
                    Label seperator = new Label();
                    seperator.Name = "seperatorLine";
                    this.Controls.Add(seperator);

                    seperator.Size = new Size(this.Width - 90, 1);
                    seperator.Location = new Point(x, y + 36);
                    seperator.BringToFront();
                    seperator.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    seperator.BackColor = Color.FromArgb(255, 150, 150, 150);

                    y += dataGridTable.Rows[0].Height;
                }
            }
            int allRows = dataGridTable.Rows.Count;

            for (int i = 0; i < allRows; i++)
            {
                if (dataGridTable.Rows[i].Cells[3].Value.ToString() == "0")
                {
                    ((DataGridViewImageCell)dataGridTable.Rows[i].Cells[0]).Value = Properties.Resources.filled_circle_green_12px;
                }
                else
                {
                    ((DataGridViewImageCell)dataGridTable.Rows[i].Cells[0]).Value = Properties.Resources.filled_circle_red_12px;
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
        
        private void ProjectForm_Shown(object sender, EventArgs e)
        {
            string sqlquery = "SELECT ProjectName, ProjectDescription, Closed FROM [dbo].[Projects] ORDER BY ProjectName ASC";
            SQL_Data(sqlquery, dataGridProjects);
            //populateList(dataGridProjects);

            dataGridProjects.Columns[2].Visible = false;

            DataGridViewImageColumn dgvImage = new DataGridViewImageColumn();
            dgvImage.ImageLayout = DataGridViewImageCellLayout.Normal;
            dgvImage.Width = 44;

            dataGridProjects.Columns.Insert(0, dgvImage);
            dataGridProjects.Columns[0].DefaultCellStyle.Padding = new Padding(0, 4, 0, 0);
            dataGridProjects.Columns[2].DefaultCellStyle.Padding = new Padding(0, 0, 30, 0);

            int numRows = dataGridProjects.Rows.Count;

            for (int i = 0; i < numRows; i++)
            {
                if (dataGridProjects.Rows[i].Cells[3].Value.ToString() == "0")
                {
                    ((DataGridViewImageCell)dataGridProjects.Rows[i].Cells[0]).Value = Properties.Resources.filled_circle_green_12px;
                }
                else
                {
                    ((DataGridViewImageCell)dataGridProjects.Rows[i].Cells[0]).Value = Properties.Resources.filled_circle_red_12px;
                }
            }

            dataGridProjects.ClearSelection();
            
            divider(dataGridProjects);

            dataGridProjects.Columns[1].Width = 380;
            dataGridProjects.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (numRows > 0)
            {
                dataGridProjects.Rows[numRows - 1].DividerHeight = 0;
            }

            columnFont(dataGridProjects);
            

            int ellipseSize = 20;
            projectSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, projectSearchPanel.Width, projectSearchPanel.Height, ellipseSize, ellipseSize));
            projectSearchTxt.Text = "Search";
            projectSearchTxt.ForeColor = Color.Gray;
            projectSearchPanel.Visible = true;

            CustomComboBox closedFilter = new CustomComboBox();
            closedFilter.Name = "closedFilterCbo";
            closedFilter.Location = new Point(50, 90);
            closedFilter.Size = new Size(181, 35);
            closedFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            closedFilter.FlatStyle = FlatStyle.Popup;
            closedFilter.Font = new Font("Open Sans", 10);
            closedFilter.Items.Add("All");
            closedFilter.Items.Add("Open");
            closedFilter.Items.Add("Closed");
            closedFilter.SelectedIndex = 0;
            closedFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.Controls.Add(closedFilter);

            closedFilter.BringToFront();
            closedFilter.SelectedIndexChanged += new EventHandler(closedFilter_TextChanged);

            dataGridProjects.Columns[2].Visible = false;
        }

        private void projectSearchTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.SuppressKeyPress = true;
        }

        private void columnFont(DataGridView dataGridTable)
        {
            int numRows = dataGridTable.Rows.Count;

            for (int i = 0; i < numRows; i++)
            {
                dataGridTable[2, i].Style.Font = new Font("Open Sans", 11);
                dataGridTable[2, i].Style.ForeColor = Color.FromArgb(255, 0, 71, 187);
                dataGridTable[2, i].Style.SelectionForeColor = Color.FromArgb(255, 0, 71, 187);
            }
        }

        private void projectSearchTxt_TextChanged(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;

            foreach (Control item in this.Controls.OfType<ComboBox>())
            {
                if (item.Name == "closedFilterCbo")
                {
                    if (projectSearchTxt.Text != "Search")
                    {
                        if (item.Text == "All")
                        {
                            using (SqlConnection connection = new SqlConnection(mainconn))
                            using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectName, ProjectDescription, Closed FROM [dbo].[Projects] WHERE ProjectName LIKE @projectName + '%' ORDER BY ProjectName ASC", connection))
                            {
                                connection.Open();
                                sqlcomm.Parameters.AddWithValue("@projectName", projectSearchTxt.Text);
                                sqlcomm.ExecuteNonQuery();
                                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                                DataTable dt = new DataTable();
                                sdr.Fill(dt);
                                dataGridProjects.DataSource = dt;
                                connection.Close();
                            }

                            columnFont(dataGridProjects);
                            removeDividerLabel();
                            divider(dataGridProjects);
                        }
                        if (item.Text == "Open")
                        {
                            using (SqlConnection connection = new SqlConnection(mainconn))
                            using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectName, ProjectDescription, Closed FROM [dbo].[Projects] WHERE ProjectName LIKE @projectName + '%' AND Closed = '0' ORDER BY ProjectName ASC", connection))
                            {
                                connection.Open();
                                sqlcomm.Parameters.AddWithValue("@projectName", projectSearchTxt.Text);
                                sqlcomm.ExecuteNonQuery();
                                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                                DataTable dt = new DataTable();
                                sdr.Fill(dt);
                                dataGridProjects.DataSource = dt;
                                connection.Close();
                            }
                            columnFont(dataGridProjects);
                            removeDividerLabel();
                            divider(dataGridProjects);
                        }
                        if (item.Text == "Closed")
                        {
                            using (SqlConnection connection = new SqlConnection(mainconn))
                            using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectName, ProjectDescription, Closed FROM [dbo].[Projects] WHERE ProjectName LIKE @projectName + '%' AND Closed = '1' ORDER BY ProjectName ASC", connection))
                            {
                                connection.Open();
                                sqlcomm.Parameters.AddWithValue("@projectName", projectSearchTxt.Text);
                                sqlcomm.ExecuteNonQuery();
                                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                                DataTable dt = new DataTable();
                                sdr.Fill(dt);
                                dataGridProjects.DataSource = dt;
                                connection.Close();
                            }
                            columnFont(dataGridProjects);
                            removeDividerLabel();
                            divider(dataGridProjects);
                        }
                    }
                }
            }
        }

        private void projectSearchTxt_MouseClick(object sender, MouseEventArgs e)
        {
            if(projectSearchTxt.Text == "Search")
            {
                projectSearchTxt.Text = string.Empty;
                projectSearchTxt.ForeColor = Color.Black;
            }
        }

        private void projectSearchTxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (projectSearchTxt.Text == "Search")
            {
                projectSearchTxt.Text = string.Empty;
                projectSearchTxt.ForeColor = Color.Black;
            }
        }

        private void dataGridView2_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex > -1) {
                dataGridProjects.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(255, 100, 100, 100);
            }
        }

        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridProjects.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string value = "";
            value = dataGridProjects.Rows[e.RowIndex].Cells["ProjectName"].Value.ToString();

            openChildForm(new ProjectData(value));
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

        private void newProjectLbl_Click(object sender, EventArgs e)
        {
            openChildForm(new ProjectAdd(""));
        }

        private void closedFilter_TextChanged(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;

            foreach (Control item in this.Controls.OfType<ComboBox>())
            {
                if (item.Name == "closedFilterCbo")
                {
                    if (projectSearchTxt.Text != "Search")
                    {
                        if (item.Text == "All")
                        {
                            using (SqlConnection connection = new SqlConnection(mainconn))
                            using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectName, ProjectDescription, Closed FROM [dbo].[Projects] WHERE ProjectName LIKE @projectName + '%' ORDER BY ProjectName ASC", connection))
                            {
                                connection.Open();
                                sqlcomm.Parameters.AddWithValue("@projectName", projectSearchTxt.Text);
                                sqlcomm.ExecuteNonQuery();
                                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                                DataTable dt = new DataTable();
                                sdr.Fill(dt);
                                dataGridProjects.DataSource = dt;
                                connection.Close();
                            }
                            columnFont(dataGridProjects);
                            removeDividerLabel();
                            divider(dataGridProjects);
                        }
                        if (item.Text == "Open")
                        {
                            using (SqlConnection connection = new SqlConnection(mainconn))
                            using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectName, ProjectDescription, Closed FROM [dbo].[Projects] WHERE ProjectName LIKE @projectName + '%' AND Closed = '0' ORDER BY ProjectName ASC", connection))
                            {
                                connection.Open();
                                sqlcomm.Parameters.AddWithValue("@projectName", projectSearchTxt.Text);
                                sqlcomm.ExecuteNonQuery();
                                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                                DataTable dt = new DataTable();
                                sdr.Fill(dt);
                                dataGridProjects.DataSource = dt;
                                connection.Close();
                            }
                            columnFont(dataGridProjects);
                            removeDividerLabel();
                            divider(dataGridProjects);
                        }
                        if (item.Text == "Closed")
                        {
                            using (SqlConnection connection = new SqlConnection(mainconn))
                            using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectName, ProjectDescription, Closed FROM [dbo].[Projects] WHERE ProjectName LIKE @projectName + '%' AND Closed = '1' ORDER BY ProjectName ASC", connection))
                            {
                                connection.Open();
                                sqlcomm.Parameters.AddWithValue("@projectName", projectSearchTxt.Text);
                                sqlcomm.ExecuteNonQuery();
                                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                                DataTable dt = new DataTable();
                                sdr.Fill(dt);
                                dataGridProjects.DataSource = dt;
                                connection.Close();
                            }
                            columnFont(dataGridProjects);
                            removeDividerLabel();
                            divider(dataGridProjects);
                        }
                    }
                    if (projectSearchTxt.Text == "Search")
                    {
                        if (item.Text == "All")
                        {
                            string sqlquery = "SELECT ProjectName, ProjectDescription, Closed FROM [dbo].[Projects] ORDER BY ProjectName ASC";
                            SQL_Data(sqlquery, dataGridProjects);
                            removeDividerLabel();
                            divider(dataGridProjects);
                        }
                        if (item.Text == "Open")
                        {
                            string sqlquery = "SELECT ProjectName, ProjectDescription, Closed FROM [dbo].[Projects] WHERE Closed = '0' ORDER BY ProjectName ASC";
                            SQL_Data(sqlquery, dataGridProjects);
                            removeDividerLabel();
                            divider(dataGridProjects);
                        }
                        if (item.Text == "Closed")
                        {
                            string sqlquery = "SELECT ProjectName, ProjectDescription, Closed FROM [dbo].[Projects] WHERE Closed = '1' ORDER BY ProjectName ASC";
                            SQL_Data(sqlquery, dataGridProjects);
                            removeDividerLabel();
                            divider(dataGridProjects);
                        }
                    }
                }
            }
            ProjectLbl.Focus();
        }

        private void ProjectForm_Resize(object sender, EventArgs e)
        {
            dataGridProjects.Width = this.Width;
            dataGridProjects.Height = this.Height - 136;

            foreach (Control item in this.Controls.OfType<Label>())
            {
                if (item.Name == "seperatorLine")
                {
                    item.Width = this.Width - 90;
                }
            }
            newProjectLbl.Location = new Point(this.Width - 174, 54);
            int ellipseSize = 20;

            projectSearchPanel.Location = new Point(this.Width / 2 + 55, 88);
            projectSearchPanel.Width = this.Width / 2 - 90;
            projectSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, projectSearchPanel.Width, projectSearchPanel.Height, ellipseSize, ellipseSize));

        }

        private void dataGridProjects_Scroll(object sender, ScrollEventArgs e)
        {
        }
    }
}
