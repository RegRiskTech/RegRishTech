using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Desktop_App
{
    public partial class UserForm : Form
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

        public UserForm()
        {
            InitializeComponent();
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
            columnFont(dataGridTable);
            sqlconnn.Close();
            dataGridTable.ClearSelection();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
           
        }

        private void UserForm_Shown(object sender, EventArgs e)
        {
            string sqlquery = "SELECT CONCAT(FirstName, ' ', LastName) AS FULLNAME, Email FROM [dbo].[Members] ORDER BY FirstName ASC";
            SQL_Data(sqlquery, dataGridUsers);
            dataGridUsers.ClearSelection();
            divider(dataGridUsers);

            int numRows = dataGridUsers.Rows.Count;
            if (numRows > 0)
            {
                dataGridUsers.Rows[numRows - 1].DividerHeight = 0;
            }

            int ellipseSize = 20;
            userSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, userSearchPanel.Width, userSearchPanel.Height, ellipseSize, ellipseSize));
            userSearchTxt.Text = "Search";
            userSearchTxt.ForeColor = Color.Gray;
            userSearchPanel.Visible = true;

            dataGridUsers.Columns[0].MinimumWidth = 436;
            dataGridUsers.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridUsers.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void newUserLbl_Click(object sender, EventArgs e)
        {
            openChildForm(new userAdd(""));
        }

        private void userSearchTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.SuppressKeyPress = true;
        }

        private void userSearchTxt_TextChanged(object sender, EventArgs e)
        {
            if (userSearchTxt.Text != "Search")
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT CONCAT(FirstName, ' ', LastName) AS FULLNAME, Email FROM [dbo].[Members] WHERE (FirstName LIKE @firstName + '%' OR LastName LIKE @lastName + '%' OR Email LIKE @email + '%') ORDER BY FirstName ASC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@firstName", userSearchTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@lastName", userSearchTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@email", userSearchTxt.Text);
                    sqlcomm.ExecuteNonQuery();
                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridUsers.DataSource = dt;
                    connection.Close();
                }
                columnFont(dataGridUsers);
                removeDividerLabel();
                divider(dataGridUsers);
            }

        }

        private void userSearchTxt_MouseClick(object sender, MouseEventArgs e)
        {
            if (userSearchTxt.Text == "Search")
            {
                userSearchTxt.Text = string.Empty;
                userSearchTxt.ForeColor = Color.Black;
            }
        }

        private void userSearchTxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (userSearchTxt.Text == "Search")
            {
                userSearchTxt.Text = string.Empty;
                userSearchTxt.ForeColor = Color.Black;
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

        private void dataGridUsers_CellMouseMove_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridUsers.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(255, 100, 100, 100);
            }
        }

        private void dataGridUsers_CellMouseLeave_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridUsers.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void columnFont(DataGridView dataGridTable)
        {
            int numRows = dataGridTable.Rows.Count;

            for (int i = 0; i < numRows; i++)
            {
                dataGridTable[1, i].Style.Font = new Font("Open Sans", 11);
                dataGridTable[1, i].Style.ForeColor = Color.FromArgb(255, 0, 71, 187);
                dataGridTable[1, i].Style.SelectionForeColor = Color.FromArgb(255, 0, 71, 187);
            }
        }

        private void divider(DataGridView dataGridTable)
        {
            int numRows = dataGridTable.Rows.GetRowCount(DataGridViewElementStates.Visible);
            int x = 46;
            int y = topPanel.Height + 34;

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

        private void dataGridUsers_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            string name = "";
            name = dataGridUsers.Rows[e.RowIndex].Cells["FULLNAME"].Value.ToString();
            string email = "";
            email = dataGridUsers.Rows[e.RowIndex].Cells["Email"].Value.ToString();

            openChildForm(new UserData(name, email));
        }

        private void UserForm_Resize(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls.OfType<Label>())
            {
                if (item.Name == "seperatorLine")
                {
                    item.Width = this.Width - 90;
                }
            }

            newUserLbl.Location = new Point(this.Width - 189, 54);
            int ellipseSize = 20;


            userSearchPanel.Location = new Point(this.Width / 2 + 55, 88);
            userSearchPanel.Width = this.Width / 2 - 90;

            userSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, userSearchPanel.Width, userSearchPanel.Height, ellipseSize, ellipseSize));


        }
    }
}
