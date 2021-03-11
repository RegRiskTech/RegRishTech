using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Desktop_App
{
    public partial class userAdd : Form
    {
        public userAdd(string email)
        {
            InitializeComponent();
            userNameTxt.Text = email;
            emailTxt.Text = email;
        }

        int maxFirst = 50;
        int maxLast = 50;
        int maxEmail = 100;

        private void userAdd_Load(object sender, EventArgs e)
        {
            CustomComboBox departmentCbo = new CustomComboBox();
            departmentCbo.Name = "departmentCbo";
            departmentCbo.Location = new Point(41, 317);

            departmentCbo.Size = new Size(422, 35);
            departmentCbo.DropDownStyle = ComboBoxStyle.DropDownList;
            departmentCbo.FlatStyle = FlatStyle.Popup;
            departmentCbo.Font = new Font("Open Sans", 11);
            departmentCbo.Items.Add("Banking");
            departmentCbo.Items.Add("Compliance");
            departmentCbo.Items.Add("C-suite");
            departmentCbo.Items.Add("IT");
            departmentCbo.Items.Add("Markets");
            departmentCbo.Items.Add("Other");
            departmentCbo.SelectedIndex = 0;
            departmentCbo.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            departmentCbo.SelectedIndex = -1;
            departmentCbo.TabIndex = 3;
            this.Controls.Add(departmentCbo);

            departmentCbo.BringToFront();
            departmentCbo.SelectedIndexChanged += new EventHandler(departmentCbo_SelectedIndexChanged);

            CustomComboBox userTypeCbo = new CustomComboBox();
            userTypeCbo.Name = "userTypeCbo";
            userTypeCbo.Location = new Point(41, 395);
            userTypeCbo.Size = new Size(422, 35);
            userTypeCbo.DropDownStyle = ComboBoxStyle.DropDownList;
            userTypeCbo.FlatStyle = FlatStyle.Popup;
            userTypeCbo.Font = new Font("Open Sans", 11);
            userTypeCbo.Items.Add("Internal");
            userTypeCbo.Items.Add("External");
            userTypeCbo.Items.Add("Domain");
            userTypeCbo.Items.Add("Other");
            userTypeCbo.SelectedIndex = 0;
            userTypeCbo.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            userTypeCbo.SelectedIndex = -1;
            userTypeCbo.TabIndex = 4;
            this.Controls.Add(userTypeCbo);

            userTypeCbo.BringToFront();
            userTypeCbo.SelectedIndexChanged += new EventHandler(userTypeCbo_SelectedIndexChanged);

            if (userNameTxt.Text != "")
            {
                string sqlquery = "SELECT MemberID, FirstName, LastName, Email, MemberTypeID, Department FROM [dbo].[Members] WHERE Email like '" + userNameTxt.Text + "%'";
                SQL_Data(sqlquery, dataGridInfo);
                dataGridInfo.ClearSelection();

                userIDTxt.Text = dataGridInfo.Rows[0].Cells[0].Value.ToString();
                firstNameTxt.Text = dataGridInfo.Rows[0].Cells[1].Value.ToString();
                lastNameTxt.Text = dataGridInfo.Rows[0].Cells[2].Value.ToString();
                foreach (Control item in this.Controls.OfType<ComboBox>())
                {
                    if (item.Name == "departmentCbo")
                    {
                        item.Text = dataGridInfo.Rows[0].Cells[5].Value.ToString();
                    }
                }

                foreach (Control item in this.Controls.OfType<ComboBox>())
                {
                    if (item.Name == "userTypeCbo")
                    {
                        item.Text = dataGridInfo.Rows[0].Cells[4].Value.ToString();
                    }
                }

                submitBtn.Visible = false;
                updateBtn.Visible = true;
                firstNameTxt.Enabled = false;
                lastNameTxt.Enabled = false;
                emailTxt.Enabled = false;
                uniqueEmailLbl.Visible = false;
            }
            else
            {
                submitBtn.Visible = true;
                updateBtn.Visible = false;
            }

            charRemainingFirstLbl.Text = (maxFirst - firstNameTxt.Text.Length) + " Characters remaining";
            charRemainingLastLbl.Text = (maxLast - lastNameTxt.Text.Length) + " Characters remaining";
            charRemainingEmailLbl.Text = (maxEmail - emailTxt.Text.Length) + " Characters remaining";

            firstNameTxt.MaxLength = maxFirst;
            lastNameTxt.MaxLength = maxLast;
            emailTxt.MaxLength = maxEmail;
        }
        private void departmentCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            firstNameLbl.Focus();
        }
        private void userTypeCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            firstNameLbl.Focus();
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
            dataGridTable.ClearSelection();
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


        private void submitBtn_Click(object sender, EventArgs e)
        {
            if (dataGridUniqueEmail.Rows.Count == 0 && firstNameTxt.Text != "" && lastNameTxt.Text != "" && emailTxt.Text != "")
            {
                string typeValue = "";
                string departmentValue = "";
                foreach (Control item in this.Controls.OfType<ComboBox>())
                {
                    if (item.Name == "userTypeCbo")
                    {
                        typeValue = item.Text;
                    }
                }

                foreach (Control item in this.Controls.OfType<ComboBox>())
                {
                    if (item.Name == "departmentCbo")
                    {
                        departmentValue = item.Text;
                    }
                }

                string sqlquery = "SELECT MAX(MemberID) FROM Members";
                SQL_Data(sqlquery, dataGridMemberIDMax);
                dataGridMemberIDMax.ClearSelection();

                string fullName = firstNameTxt.Text.ToString() + " " + lastNameTxt.Text.ToString();

                newMemberIDLbl.Text = dataGridMemberIDMax.CurrentRow.Cells[0].Value.ToString();
                int memberIDMax = Convert.ToInt32(newMemberIDLbl.Text);
                int newMemberID = memberIDMax + 1;
                newMemberIDLbl.Text = newMemberID.ToString();

                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("INSERT INTO Members(MemberID,FirstName,LastName,Email,MemberTypeID,Department) VALUES (@memberID,@firstName,@lastName,@email,@memberTypeID,@department)", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@memberID", newMemberIDLbl.Text);
                    sqlcomm.Parameters.AddWithValue("@firstName", firstNameTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@lastName", lastNameTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@email", emailTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@memberTypeID", typeValue);
                    sqlcomm.Parameters.AddWithValue("@department", departmentValue);
                    sqlcomm.ExecuteNonQuery();

                    connection.Close();
                }
                openChildForm(new UserData(fullName, emailTxt.Text));
            }
            else
            {
                MessageBox.Show("Please fill out the required fields.", "", MessageBoxButtons.OK);
                //uniqueEmailLbl.Visible = false;

                if (emailTxt.Text == "")
                {
                    emailBlankLbl.Visible = true;
                    charRemainingFirstLbl.Visible = false;
                    charRemainingLastLbl.Visible = false;
                    charRemainingEmailLbl.Visible = true;
                    emailTxt.Focus();
                }
                if (lastNameTxt.Text == "")
                {
                    lastNameBlankLbl.Visible = true;
                    charRemainingFirstLbl.Visible = false;
                    charRemainingLastLbl.Visible = true;
                    charRemainingEmailLbl.Visible = false;
                    lastNameTxt.Focus();
                }
                if (firstNameTxt.Text == "")
                {
                    firstNameBlankLbl.Visible = true;
                    charRemainingFirstLbl.Visible = true;
                    charRemainingLastLbl.Visible = false;
                    charRemainingEmailLbl.Visible = false;
                    firstNameTxt.Focus();
                }
                /*
                if (dataGridUniqueEmail.Rows.Count != 0)
                {
                    uniqueEmailLbl.Visible = true;
                    charRemainingEmailLbl.Visible = false;
                    emailTxt.Focus();
                }*/

            }
        }

        private void updateBtn_Click_1(object sender, EventArgs e)
        {
            string typeValue = "";
            string departmentValue = "";
            foreach (Control item in this.Controls.OfType<ComboBox>())
            {
                if (item.Name == "userTypeCbo")
                {
                    typeValue = item.Text;
                }
            }
            foreach (Control item in this.Controls.OfType<ComboBox>())
            {
                if (item.Name == "departmentCbo")
                {
                    departmentValue = item.Text;
                }
            }

            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("UPDATE Members Set MemberID = @memberID, FirstName = @firstName, LastName = @lastName, Email = @email, MemberTypeID = @memberTypeID, Department = @department WHERE MemberID = '" + userIDTxt.Text + "'", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@memberID", userIDTxt.Text);
                sqlcomm.Parameters.AddWithValue("@firstName", firstNameTxt.Text);
                sqlcomm.Parameters.AddWithValue("@lastName", lastNameTxt.Text);
                sqlcomm.Parameters.AddWithValue("@email", emailTxt.Text);
                sqlcomm.Parameters.AddWithValue("@memberTypeID", typeValue);
                sqlcomm.Parameters.AddWithValue("@department", departmentValue);
                sqlcomm.ExecuteNonQuery();

                connection.Close();
            }
            string fullName = firstNameTxt.Text.ToString() + " " + lastNameTxt.Text.ToString();
            openChildForm(new UserData(fullName, emailTxt.Text.ToString()));
        }

        private void emailTxt_Enter(object sender, EventArgs e)
        {
            charRemainingEmailLbl.Visible = true;
        }

        private void emailTxt_Leave(object sender, EventArgs e)
        {
            charRemainingEmailLbl.Visible = false;
        }

        private void emailTxt_TextChanged(object sender, EventArgs e)
        {
            charRemainingEmailLbl.Text = (maxEmail - emailTxt.Text.Length) + " Characters remaining";
            emailBlankLbl.Visible = false;
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT 1 FROM Members WHERE Email = @email", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@email", emailTxt.Text);
                sqlcomm.ExecuteNonQuery();

                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridUniqueEmail.DataSource = dt;

                connection.Close();
            }
            if (dataGridUniqueEmail.Rows.Count == 0)
            {
                uniqueEmailLbl.Visible = false;
            }
            else
            {
                uniqueEmailLbl.Visible = true;
            }
        }

        private void firstNameTxt_Enter(object sender, EventArgs e)
        {
            charRemainingFirstLbl.Visible = true;
        }

        private void firstNameTxt_Leave(object sender, EventArgs e)
        {
            charRemainingFirstLbl.Visible = false;
        }

        private void firstNameTxt_TextChanged(object sender, EventArgs e)
        {
            charRemainingFirstLbl.Text = (maxFirst - firstNameTxt.Text.Length) + " Characters remaining";
            firstNameBlankLbl.Visible = false;
        }

        private void lastNameTxt_Enter(object sender, EventArgs e)
        {
            charRemainingLastLbl.Visible = true;
        }

        private void lastNameTxt_Leave(object sender, EventArgs e)
        {
            charRemainingLastLbl.Visible = false;
        }

        private void lastNameTxt_TextChanged(object sender, EventArgs e)
        {
            charRemainingLastLbl.Text = (maxLast - lastNameTxt.Text.Length) + " Characters remaining";
            lastNameBlankLbl.Visible = false;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            string fullName = firstNameTxt.Text.ToString() + " " + lastNameTxt.Text.ToString();
            if (userNameTxt.Text != "")
            {
                openChildForm(new UserData(fullName, emailTxt.Text));
            }
            else
            {
                openChildForm(new UserForm());
            }
        }
    }
}
