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
    public partial class ProjectAdd : Form
    {
        public ProjectAdd(string projectName)
        {
            InitializeComponent();
            projectNameTxt.Text = projectName;
        }

        private void ProjectAdd_Load(object sender, EventArgs e)
        {
            CustomComboBox projectTypeCbo = new CustomComboBox();
            projectTypeCbo.Name = "projectTypeCbo";
            projectTypeCbo.Location = new Point(41,157);
            projectTypeCbo.Size = new Size(422, 35);
            projectTypeCbo.DropDownStyle = ComboBoxStyle.DropDownList;
            projectTypeCbo.FlatStyle = FlatStyle.Popup;
            projectTypeCbo.Font = new Font("Open Sans", 11);
            projectTypeCbo.Items.Add("IB Project");
            projectTypeCbo.Items.Add("Investigation");
            projectTypeCbo.Items.Add("Other");
            projectTypeCbo.SelectedIndex = 0;
            projectTypeCbo.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            projectTypeCbo.SelectedIndex = -1;
            projectTypeCbo.TabIndex = 1;
            projectTypeCbo.BringToFront();
            projectTypeCbo.SelectedIndexChanged += new EventHandler(projectTypeCbo_SelectedIndexChanged);
            this.Controls.Add(projectTypeCbo);


            CustomComboBox projectSensCbo = new CustomComboBox();
            projectSensCbo.Name = "projectSensCbo";
            projectSensCbo.Location = new Point(41, 235);
            projectSensCbo.Size = new Size(422, 35);
            projectSensCbo.DropDownStyle = ComboBoxStyle.DropDownList;
            projectSensCbo.FlatStyle = FlatStyle.Popup;
            projectSensCbo.Font = new Font("Open Sans", 11);
            projectSensCbo.Items.Add("Hard");
            projectSensCbo.Items.Add("Soft");
            projectSensCbo.SelectedIndex = 0;
            projectSensCbo.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            projectSensCbo.SelectedIndex = -1;
            projectSensCbo.TabIndex = 2;
            this.Controls.Add(projectSensCbo);

            projectSensCbo.BringToFront();
            projectSensCbo.SelectedIndexChanged += new EventHandler(projectSensCbo_SelectedIndexChanged);


            if (projectNameTxt.Text != "")
            {
                string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT ProjectID, ProjectName, ProjectTypeID, OpenDate, TypeBlock, ProjectDescription FROM[dbo].[Projects] WHERE ProjectName LIKE @projectName", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectName", projectNameTxt.Text);
                    sqlcomm.ExecuteNonQuery();

                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridInfo.DataSource = dt;

                    connection.Close();
                }
                dataGridInfo.ClearSelection();

                projectIDTxt.Text = dataGridInfo.Rows[0].Cells[0].Value.ToString();
                foreach (Control item in this.Controls.OfType<ComboBox>())
                {
                    if (item.Name == "projectTypeCbo")
                    {
                        item.Text = dataGridInfo.Rows[0].Cells[2].Value.ToString();
                    }
                }
                projectOpenDateTxt.Text = dataGridInfo.Rows[0].Cells[3].Value.ToString();
                foreach (Control item in this.Controls.OfType<ComboBox>())
                {
                    if (item.Name == "projectSensCbo")
                    {
                        item.Text = dataGridInfo.Rows[0].Cells[4].Value.ToString();
                    }
                }
                projectDescTxt.Text = dataGridInfo.Rows[0].Cells[5].Value.ToString();

                submitBtn.Visible = false;
                updateBtn.Visible = true;
                projectNameTxt.Enabled = false;
                uniqueProjectLbl.Visible = false;
            }
            else
            {
                submitBtn.Visible = true;
                updateBtn.Visible = false;
            }

            charRemainingNameLbl.Text = (maxName - projectNameTxt.Text.Length) + " Characters remaining";
            charRemainingDescLbl.Text = (maxDesc - projectDescTxt.Text.Length) + " Characters remaining";
        }

        private void projectTypeCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            projectSensLbl.Focus();
        }

        private void projectSensCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            projectNameLbl.Focus();
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

        private void updateBtn_Click(object sender, EventArgs e)
        {
            string typeValue = "";
            string sensitivityValue = "";
            foreach (Control item in this.Controls.OfType<ComboBox>())
            {
                if (item.Name == "projectTypeCbo")
                {
                    typeValue = item.Text;
                }
            }
            foreach (Control item in this.Controls.OfType<ComboBox>())
            {
                if (item.Name == "projectSensCbo")
                {
                    sensitivityValue = item.Text;
                }
            }

            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("UPDATE Projects Set ProjectID = @projectID , ProjectName = @projectName, ProjectTypeID = @projectTypeID, OpenDate = @openDate, TypeBlock = @sensitivity, ProjectDescription = @projectDesc WHERE ProjectID = @projectID", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@projectID", projectIDTxt.Text);
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameTxt.Text);
                sqlcomm.Parameters.AddWithValue("@projectTypeID", typeValue);
                sqlcomm.Parameters.AddWithValue("@openDate", projectOpenDateTxt.Text);
                sqlcomm.Parameters.AddWithValue("@sensitivity", sensitivityValue);
                sqlcomm.Parameters.AddWithValue("@projectDesc", projectDescTxt.Text);
                sqlcomm.ExecuteNonQuery();

                connection.Close();
            }
            openChildForm(new ProjectData(projectNameTxt.Text));
        }

        private void submitBtn_Click_1(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT 1 FROM Projects WHERE ProjectName = @projectName", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameTxt.Text);
                sqlcomm.ExecuteNonQuery();

                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridUniqueProject.DataSource = dt;
                connection.Close();
            }
            if(dataGridUniqueProject.Rows.Count == 0 && projectNameTxt.Text != "")
            {
                uniqueProjectLbl.Visible = false;
                blankLbl.Visible = false;
                
                string typeValue = "";
                string sensitivityValue = "";
                foreach (Control item in this.Controls.OfType<ComboBox>())
                {
                    if (item.Name == "projectTypeCbo")
                    {
                        typeValue = item.Text;
                    }
                }
                foreach (Control item in this.Controls.OfType<ComboBox>())
                {
                    if (item.Name == "projectSensCbo")
                    {
                        sensitivityValue = item.Text;
                    }
                }

                mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT MAX(ProjectID) FROM Projects", connection))
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

                string todayDate = DateTime.Now.ToString("dd/MM/yyyy");

                mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("INSERT INTO Projects (ProjectID,ProjectName,ProjectTypeID,OpenDate,TypeBlock,ProjectDescription,Closed) VALUES (@projectID, @projectName, @projectTypeID, @openDate, @sensitivity, @projectDesc,'0')", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@projectID", newProjectIDLbl.Text);
                    sqlcomm.Parameters.AddWithValue("@projectName", projectNameTxt.Text);
                    sqlcomm.Parameters.AddWithValue("@projectTypeID", typeValue);
                    sqlcomm.Parameters.AddWithValue("@openDate", todayDate);
                    sqlcomm.Parameters.AddWithValue("@sensitivity", sensitivityValue);
                    sqlcomm.Parameters.AddWithValue("@projectDesc", projectDescTxt.Text);
                    sqlcomm.ExecuteNonQuery();

                    connection.Close();
                }
                openChildForm(new ProjectData(projectNameTxt.Text));
            }
            else
            {
                MessageBox.Show("Please fill out the required fields.", "", MessageBoxButtons.OK);
                if(projectNameTxt.Text == "")
                {
                    uniqueProjectLbl.Visible = false;
                    blankLbl.Visible = true;
                }
                else
                {
                    blankLbl.Visible = false;
                    uniqueProjectLbl.Visible = true;
                }
                projectNameTxt.Focus();
                charRemainingNameLbl.Visible = true;
            }
        }

        int maxDesc = 200;
        private void projectDescTxt_TextChanged(object sender, EventArgs e)
        {
            charRemainingDescLbl.Text = (maxDesc - projectDescTxt.Text.Length) + " Characters remaining";
        }


        int maxName = 20;
        private void projectNameTxt_TextChanged(object sender, EventArgs e)
        {
            charRemainingNameLbl.Text = (maxName - projectNameTxt.Text.Length) + " Characters remaining";
            blankLbl.Visible = false;
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT 1 FROM Projects WHERE ProjectName = @projectName", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@projectName", projectNameTxt.Text);
                sqlcomm.ExecuteNonQuery();

                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridUniqueProject.DataSource = dt;

                connection.Close();
            }
            if (dataGridUniqueProject.Rows.Count == 0)
            {
                uniqueProjectLbl.Visible = false;
            }
            else
            {
                uniqueProjectLbl.Visible = true;
            }
        }

        private void projectNameTxt_Enter(object sender, EventArgs e)
        {
            charRemainingNameLbl.Visible = true;
        }

        private void projectNameTxt_Leave(object sender, EventArgs e)
        {
            charRemainingNameLbl.Visible = false;
        }

        private void projectDescTxt_Enter(object sender, EventArgs e)
        {
            charRemainingDescLbl.Visible = true;
        }

        private void projectDescTxt_Leave(object sender, EventArgs e)
        {
            charRemainingDescLbl.Visible = false;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            if(projectNameTxt.Text != "")
            {
                openChildForm(new ProjectData(projectNameTxt.Text));
            }
            else
            {
                openChildForm(new ProjectForm());
            }
        }
    }
}
