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
using System.Globalization;
using System.Text.RegularExpressions;

namespace Desktop_App
{
    public partial class Reporting : Form
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
        public Reporting()
        {
            InitializeComponent();
        }

        CustomComboBox reportFilter = new CustomComboBox();
        CustomComboBox actionFilter = new CustomComboBox();
        CustomComboBox projectFilter = new CustomComboBox();
        CustomComboBox sentFromFilter = new CustomComboBox();
        CustomComboBox sentToFilter = new CustomComboBox();

        CustomComboBox actionDocFilter = new CustomComboBox();
        CustomComboBox fileFilter = new CustomComboBox();
        CustomComboBox userFilter = new CustomComboBox();
        CustomComboBox newLabelFilter = new CustomComboBox();
        CustomComboBox prevLabelFilter = new CustomComboBox();

        // Email Queries
        string actionQuery = "TRIM(SUBSTRING(Message, CHARINDEX(@keywordAction1st, Message) + LEN(@keywordAction1st), CHARINDEX(@keywordAction2nd, Message) - (CHARINDEX(@keywordAction1st, Message) + LEN(@keywordAction1st))))";
        string projectQuery = "TRIM(SUBSTRING(Message, CHARINDEX(@keywordProject1st, Message) + LEN(@keywordProject1st), CHARINDEX(@keywordProject2nd,Message) - (CHARINDEX(@keywordProject1st, Message) + LEN(@keywordProject1st))))";
        string sentFromQuery = "TRIM(SUBSTRING(Message, CHARINDEX(@keywordUser1st, Message) + LEN(@keywordUser1st), 40))";
        string sentToQuery = "TRIM(SUBSTRING(Message, CHARINDEX(@keywordTo1st, Message) + LEN(@keywordTo1st), CHARINDEX(@keywordTo2nd, Message) - (CHARINDEX(@keywordTo1st, Message) + LEN(@keywordTo1st))))";
        string subjectQuery = "TRIM(SUBSTRING(Message, CHARINDEX(@keywordSubject1st, Message) + LEN(@keywordSubject1st), CHARINDEX(@keywordSubject2nd,Message) - (CHARINDEX(@keywordSubject1st, Message) + LEN(@keywordSubject1st))))";
        string attachmentQuery = "TRIM(SUBSTRING(Message, CHARINDEX(@keywordAttachment1st, Message) + LEN(@keywordAttachment1st), CHARINDEX(@keywordAttachment2nd,Message) - (CHARINDEX(@keywordAttachment1st, Message) + LEN(@keywordAttachment1st))))";
        string searchKeyQuery = "TRIM(SUBSTRING(Message, CHARINDEX(@keywordSearch1st, Message) + LEN(@keywordSearch1st), CHARINDEX(@keywordSearch2nd,Message) - (CHARINDEX(@keywordSearch1st, Message) + LEN(@keywordSearch1st))))";

        // Document Queries
        string actionDocQuery = "TRIM(SUBSTRING(Message, CHARINDEX(@keywordActionDoc1st, Message) + LEN(@keywordActionDoc1st), CHARINDEX(@keywordActionDoc2nd,Message) - (CHARINDEX(@keywordActionDoc1st, Message) + LEN(@keywordActionDoc1st))))";
        string fileNameQuery = "TRIM(SUBSTRING(Message, CHARINDEX(@keywordFile1st, Message) + LEN(@keywordFile1st), CHARINDEX(@keywordFile2nd,Message) - (CHARINDEX(@keywordFile1st, Message) + LEN(@keywordFile1st))))";
        string userQuery = "TRIM(SUBSTRING(Message, CHARINDEX(@keywordUser1st, Message) + LEN(@keywordUser1st), CHARINDEX(@keywordUser2nd,Message) - (CHARINDEX(@keywordUser1st, Message) + LEN(@keywordUser1st))))";
        string newLabelQuery = "TRIM('- ' FROM TRIM(SUBSTRING(Message, CHARINDEX(@keywordNewLabel1st, Message) + LEN(@keywordNewLabel1st), CHARINDEX(@keywordNewLabel2nd,Message) - (CHARINDEX(@keywordNewLabel1st, Message) + LEN(@keywordNewLabel1st)))))";
        string previousLabelQuery = "TRIM('- ' FROM TRIM(SUBSTRING(Message, CHARINDEX(@keywordPreviousLabel1st, Message) + LEN(@keywordPreviousLabel1st), CHARINDEX(@keywordPreviousLabel2nd,Message) - (CHARINDEX(@keywordPreviousLabel1st, Message) + LEN(@keywordPreviousLabel1st)))))";
        string filePathQuery = "TRIM(SUBSTRING(Message, CHARINDEX(@keywordFilePath1st, Message) + LEN(@keywordFilePath1st), CHARINDEX(@keywordFilePath2nd,Message) - (CHARINDEX(@keywordFilePath1st, Message) + LEN(@keywordFilePath1st))))";

        int reportWidth = 210;

        int dateWidth = 150;
        int actionWidth = 100;
        int projectWidth = 130;
        int sentFromWidth = 140;
        int sentToWidth = 220;

        int actionDocWidth = 100;
        int fileWidth = 190;
        int userWidth = 110;
        int newLabelWidth = 110;
        int prevLabelWidth = 140;

        private void Reporting_Load(object sender, EventArgs e)
        {
            int ellipseSize = 20;
            reportingSearchPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, reportingSearchPanel.Width, reportingSearchPanel.Height, ellipseSize, ellipseSize));
            reportingSearchTxt.Text = "Search";
            reportingSearchTxt.ForeColor = Color.Gray;
            reportingSearchPanel.Visible = true;

            reportFilter.Name = "reportFilterCbo";
            reportFilter.Location = new Point(260,42);
            reportFilter.Size = new Size(reportWidth - 20, 35);
            reportFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            reportFilter.FlatStyle = FlatStyle.Popup;
            reportFilter.Font = new Font("Open Sans", 10);
            reportFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            reportFilter.Items.Add("Emails");
            reportFilter.Items.Add("Documents");
            reportFilter.SelectedIndex = 1;
            
            this.Controls.Add(reportFilter);
            reportFilter.BringToFront();
            reportFilter.SelectedIndexChanged += new EventHandler(reportFilter_SelectedIndexChanged);

            #region Email Filters
            actionFilter.Name = "actionFilterCbo";
            actionFilter.Location = new Point(193,110);
            actionFilter.Size = new Size(actionWidth - 20, 35);
            actionFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            actionFilter.FlatStyle = FlatStyle.Popup;
            actionFilter.Font = new Font("Open Sans", 10);
            actionFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.Controls.Add(actionFilter);
            actionFilter.BringToFront();
            actionFilter.SelectedIndexChanged += new EventHandler(actionFilter_SelectedIndexChanged);

            projectFilter.Name = "projectFilterCbo";
            projectFilter.Location = new Point(293, 110);
            projectFilter.Size = new Size(projectWidth - 30, 35);
            projectFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            projectFilter.FlatStyle = FlatStyle.Popup;
            projectFilter.Font = new Font("Open Sans", 10);
            projectFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.Controls.Add(projectFilter);
            projectFilter.BringToFront();
            projectFilter.SelectedIndexChanged += new EventHandler(projectFilter_SelectedIndexChanged);

            sentFromFilter.Name = "sentFromFilterCbo";
            sentFromFilter.Location = new Point(423, 110);
            sentFromFilter.Size = new Size(sentFromWidth - 30, 35);
            sentFromFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            sentFromFilter.FlatStyle = FlatStyle.Popup;
            sentFromFilter.Font = new Font("Open Sans", 10);
            sentFromFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.Controls.Add(sentFromFilter);
            sentFromFilter.BringToFront();
            sentFromFilter.SelectedIndexChanged += new EventHandler(sentFromFilter_SelectedIndexChanged);

            sentToFilter.Name = "sentToFilterCbo";
            sentToFilter.Location = new Point(563, 110);
            sentToFilter.Size = new Size(sentToWidth + 25, 35);
            sentToFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            sentToFilter.FlatStyle = FlatStyle.Popup;
            sentToFilter.Font = new Font("Open Sans", 10);
            sentToFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.Controls.Add(sentToFilter);
            sentToFilter.BringToFront();
            sentToFilter.SelectedIndexChanged += new EventHandler(sentToFilter_SelectedIndexChanged);
            #endregion

            #region Document Filters
            actionDocFilter.Name = "actionDocFilterCbo";
            actionDocFilter.Location = new Point(190, 110);
            actionDocFilter.Size = new Size(actionDocWidth - 20, 35);
            actionDocFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            actionDocFilter.FlatStyle = FlatStyle.Popup;
            actionDocFilter.Font = new Font("Open Sans", 10);
            actionDocFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            actionDocFilter.Items.Add("Classified");
            actionDocFilter.Items.Add("Reclassified");
            actionDocFilter.Items.Add("Declassified");
            this.Controls.Add(actionDocFilter);
            actionDocFilter.BringToFront();
            actionDocFilter.SelectedIndexChanged += new EventHandler(actionDocFilter_SelectedIndexChanged);
            actionDocFilter.Visible = false;

            fileFilter.Name = "fileFilterCbo";
            fileFilter.Location = new Point(295, 110);
            fileFilter.Size = new Size(fileWidth - 20, 35);
            fileFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            fileFilter.FlatStyle = FlatStyle.Popup;
            fileFilter.Font = new Font("Open Sans", 10);
            fileFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            fileFilter.Items.Add("Excel");
            fileFilter.Items.Add("Image");
            fileFilter.Items.Add("PDF");
            fileFilter.Items.Add("Powerpoint");
            fileFilter.Items.Add("Text");
            fileFilter.Items.Add("Word");
            fileFilter.SelectedIndex = 0;
            this.Controls.Add(fileFilter);
            fileFilter.BringToFront();
            fileFilter.SelectedIndexChanged += new EventHandler(fileFilter_SelectedIndexChanged);
            fileFilter.Visible = false;

            userFilter.Name = "userFilterCbo";
            userFilter.Location = new Point(485, 110);
            userFilter.Size = new Size(userWidth - 30, 35);
            userFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            userFilter.FlatStyle = FlatStyle.Popup;
            userFilter.Font = new Font("Open Sans", 10);
            userFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.Controls.Add(userFilter);
            userFilter.BringToFront();
            userFilter.SelectedIndexChanged += new EventHandler(userFilter_SelectedIndexChanged);
            userFilter.Visible = false;

            newLabelFilter.Name = "newLabelFilterCbo";
            newLabelFilter.Location = new Point(595, 110);
            newLabelFilter.Size = new Size(newLabelWidth - 20, 35);
            newLabelFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            newLabelFilter.FlatStyle = FlatStyle.Popup;
            newLabelFilter.Font = new Font("Open Sans", 10);
            newLabelFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.Controls.Add(newLabelFilter);
            newLabelFilter.BringToFront();
            newLabelFilter.SelectedIndexChanged += new EventHandler(newLabelFilter_SelectedIndexChanged);
            newLabelFilter.Visible = false;

            prevLabelFilter.Name = "prevLabelFilterCbo";
            prevLabelFilter.Location = new Point(705, 110);
            prevLabelFilter.Size = new Size(prevLabelWidth - 40, 35);
            prevLabelFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            prevLabelFilter.FlatStyle = FlatStyle.Popup;
            prevLabelFilter.Font = new Font("Open Sans", 10);
            prevLabelFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.Controls.Add(prevLabelFilter);
            prevLabelFilter.BringToFront();
            prevLabelFilter.SelectedIndexChanged += new EventHandler(prevLabelFilter_SelectedIndexChanged);
            prevLabelFilter.Visible = false;
            #endregion

            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionDocQuery + " AS 'Action', " + fileNameQuery + "AS 'File', " + userQuery + " AS 'User', " + newLabelQuery + " AS 'New Label', " + previousLabelQuery + " AS 'Previous Label', " + filePathQuery + " AS 'File Path'  FROM [dbo].[EventViewer] WHERE ID = 2100 ORDER BY 'Date & Time' DESC", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@keywordActionDoc1st", "File Name: ");
                sqlcomm.Parameters.AddWithValue("@keywordActionDoc2nd", "Classifier-DocId:");

                sqlcomm.Parameters.AddWithValue("@keywordFile1st", "File Name: ");
                sqlcomm.Parameters.AddWithValue("@keywordFile2nd", "Classifier-DocId:");

                sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                sqlcomm.Parameters.AddWithValue("@keywordUser2nd", "File: ");

                sqlcomm.Parameters.AddWithValue("@keywordNewLabel1st", "Label: ");
                sqlcomm.Parameters.AddWithValue("@keywordNewLabel2nd", "Previous Label:");

                sqlcomm.Parameters.AddWithValue("@keywordPreviousLabel1st", "Previous Label: ");
                sqlcomm.Parameters.AddWithValue("@keywordPreviousLabel2nd", "Event Details:");

                sqlcomm.Parameters.AddWithValue("@keywordFilePath1st", "File: ");
                sqlcomm.Parameters.AddWithValue("@keywordFilePath2nd", "User Saver ID:");

                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                dataGridReporting.DataSource = dt;
                connection.Close();
            }

            // Action replacement
            for (int i = 0; i < dataGridReporting.Rows.Count; i++)
            {
                if (dataGridReporting.Rows[i].Cells[4].Value.ToString() != "No Marking" && dataGridReporting.Rows[i].Cells[5].Value.ToString() == "No Marking")
                {
                    dataGridReporting.Rows[i].Cells[1].Value = "Classified";
                }
                if (dataGridReporting.Rows[i].Cells[4].Value.ToString() != "No Marking" && dataGridReporting.Rows[i].Cells[5].Value.ToString() != "No Marking")
                {
                    dataGridReporting.Rows[i].Cells[1].Value = "Reclassified";
                }
                if (dataGridReporting.Rows[i].Cells[4].Value.ToString() == "No Marking" && dataGridReporting.Rows[i].Cells[5].Value.ToString() != "No Marking")
                {
                    dataGridReporting.Rows[i].Cells[1].Value = "Declassified";
                }
            }

            dataGridReporting.Columns[6].Visible = false;
            // Date & Time
            dataGridReporting.Columns[0].Width = dateWidth;
            // Action
            dataGridReporting.Columns[1].Width = actionDocWidth;
            // File Name
            dataGridReporting.Columns[2].MinimumWidth = fileWidth;
            dataGridReporting.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // User
            dataGridReporting.Columns[3].Width = userWidth;
            // New Label
            dataGridReporting.Columns[4].Width = newLabelWidth;
            // Previous Label
            dataGridReporting.Columns[5].Width = prevLabelWidth;

            //MessageBox.Show(dataGridReporting.Columns[3].HeaderText.ToString());

            //comboFill(dataGridReporting);
            foreach (DataGridViewRow dr in dataGridReporting.Rows)
            {
                if (!userFilter.Items.Contains(dr.Cells["User"].Value.ToString()))
                {
                    userFilter.Items.Add(dr.Cells["User"].Value.ToString());
                }
                if (!newLabelFilter.Items.Contains(dr.Cells["New Label"].Value.ToString()))
                {
                    newLabelFilter.Items.Add(dr.Cells["New Label"].Value.ToString());
                }
                if (!prevLabelFilter.Items.Contains(dr.Cells["Previous Label"].Value.ToString()))
                {
                    prevLabelFilter.Items.Add(dr.Cells["Previous Label"].Value.ToString());
                }
            }

            actionDocFilter.Sorted = true;
            actionDocFilter.Sorted = false;
            actionDocFilter.Items.Insert(0, "All");
            actionDocFilter.SelectedIndex = 0;

            fileFilter.Items.Insert(0, "All");
            fileFilter.SelectedIndex = 0;

            userFilter.Sorted = true;
            userFilter.Sorted = false;
            userFilter.Items.Insert(0, "All");
            userFilter.SelectedIndex = 0;

            newLabelFilter.Sorted = true;
            newLabelFilter.Sorted = false;
            newLabelFilter.Items.Insert(0, "All");
            newLabelFilter.SelectedIndex = 0;

            prevLabelFilter.Sorted = true;
            prevLabelFilter.Sorted = false;
            prevLabelFilter.Items.Insert(0, "All");
            prevLabelFilter.SelectedIndex = 0;
            ////////////////////////////////////////////

            reportFilter.SelectedIndex = 0;
            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("UPDATE [dbo].[EventViewer] SET Message = REPLACE(Message, char(9), '')", connection))
            {
                connection.Open();
                sqlcomm.ExecuteNonQuery();
                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("UPDATE [dbo].[EventViewer] SET Message = REPLACE(Message, CHAR(13) + CHAR(10), '')", connection))
            {
                connection.Open();
                sqlcomm.ExecuteNonQuery();
                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection(mainconn))
            using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + " AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To', " + sentToQuery + " AS 'Sent To Hidden', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 ORDER BY 'Date & Time' DESC", connection))
            {
                connection.Open();
                sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                sqlcomm.ExecuteNonQuery();
                SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);

                dataGridReporting.DataSource = dt;
                connection.Close();
            }

            dataGridReporting.ColumnHeadersDefaultCellStyle.Font = new Font("Open Sans Bold", 12);

            for (int i = 0; i < dataGridReporting.Rows.Count; i++)
            {
                if (dataGridReporting.Rows[i].Cells[4].Value.ToString().Contains(","))
                {
                    dataGridReporting.Rows[i].Cells[4].Value = "Multiple Users";
                }
            }

            dataGridReporting.Columns[5].Visible = false;
            dataGridReporting.Columns[6].Visible = false;
            dataGridReporting.Columns[7].Visible = false;
            dataGridReporting.Columns[8].Visible = false;

            comboFill(dataGridReporting);
            actionFilter.Sorted = true;
            actionFilter.Sorted = false;
            actionFilter.Items.Insert(0, "All");
            actionFilter.SelectedIndex = 0;

            projectFilter.Sorted = true;
            projectFilter.Sorted = false;
            projectFilter.Items.Insert(0, "All");
            projectFilter.SelectedIndex = 0;

            sentFromFilter.Sorted = true;
            sentFromFilter.Sorted = false;
            sentFromFilter.Items.Insert(0, "All");
            sentFromFilter.SelectedIndex = 0;

            sentToFilter.Sorted = true;
            sentToFilter.Sorted = false;
            sentToFilter.Items.Insert(0, "All");
            sentToFilter.SelectedIndex = 0;

            dataGridReporting.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridReporting.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridReporting.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridReporting.Columns[0].Width = dateWidth;
            dataGridReporting.Columns[1].Width = actionWidth;
            dataGridReporting.Columns[2].Width = projectWidth;
            dataGridReporting.Columns[3].Width = sentFromWidth;
            dataGridReporting.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            removeDividerLabel();
            divider(dataGridReporting);

            /////


        }

        private void comboFill(DataGridView dt)
        {
            if (reportFilter.Text == "Emails")
            {
                swapFilters("Emails");

                foreach (DataGridViewRow dr in dt.Rows)
                {
                    if (!actionFilter.Items.Contains(dr.Cells["Action"].Value.ToString()))
                    {
                        actionFilter.Items.Add(dr.Cells["Action"].Value.ToString());
                    }
                    if (!projectFilter.Items.Contains(dr.Cells["Project"].Value.ToString()))
                    {
                        projectFilter.Items.Add(dr.Cells["Project"].Value.ToString());
                    }
                    if (!sentFromFilter.Items.Contains(dr.Cells["Sent From"].Value.ToString()))
                    {
                        sentFromFilter.Items.Add(dr.Cells["Sent From"].Value.ToString());
                    }
                    if (!sentToFilter.Items.Contains(dr.Cells["Sent To"].Value.ToString()) && !dr.Cells["Sent To"].Value.ToString().Contains(','))
                    {
                        sentToFilter.Items.Add(dr.Cells["Sent To"].Value.ToString());
                    }
                }
            }
            else
            {
                swapFilters("Documents");

                foreach (DataGridViewRow dr in dt.Rows)
                {
                    if (!userFilter.Items.Contains(dr.Cells["User"].Value.ToString()))
                    {
                        userFilter.Items.Add(dr.Cells["User"].Value.ToString());
                    }
                    if (!newLabelFilter.Items.Contains(dr.Cells["New Label"].Value.ToString()))
                    {
                        newLabelFilter.Items.Add(dr.Cells["New Label"].Value.ToString());
                    }
                    if (!prevLabelFilter.Items.Contains(dr.Cells["Previous Label"].Value.ToString()))
                    {
                        prevLabelFilter.Items.Add(dr.Cells["Previous Label"].Value.ToString());
                    }
                }
            }
        }

        private void swapFilters(string report)
        {
            if (report == "Emails")
            {
                actionDocFilter.Visible = false;
                fileFilter.Visible = false;
                userFilter.Visible = false;
                newLabelFilter.Visible = false;
                prevLabelFilter.Visible = false;

                actionFilter.Visible = true;
                projectFilter.Visible = true;
                sentFromFilter.Visible = true;
                sentToFilter.Visible = true;
            }
            else
            {
                actionFilter.Visible = false;
                projectFilter.Visible = false;
                sentFromFilter.Visible = false;
                sentToFilter.Visible = false;

                actionDocFilter.Visible = true;
                fileFilter.Visible = true;
                userFilter.Visible = true;
                newLabelFilter.Visible = true;
                prevLabelFilter.Visible = true;
            }
        }

        private void reportQuery()
        {
            // 0 means all, 1 means selected value
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            if (reportFilter.Text == "Emails")
            {
                // 0,0,0,0
                if (actionFilter.Text == "All" && projectFilter.Text == "All" && sentFromFilter.Text == "All" && sentToFilter.Text == "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    dataGridReporting.Columns[5].Visible = false;
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 0,0,0,1
                if (actionFilter.Text == "All" && projectFilter.Text == "All" && sentFromFilter.Text == "All" && sentToFilter.Text != "All")
                    {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + sentToQuery + " = @sentToValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 0,0,1,0
                if (actionFilter.Text == "All" && projectFilter.Text == "All" && sentFromFilter.Text != "All" && sentToFilter.Text == "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + sentFromQuery + " = @sentFromValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 0,1,0,0
                if (actionFilter.Text == "All" && projectFilter.Text != "All" && sentFromFilter.Text == "All" && sentToFilter.Text == "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + projectQuery + " = @projectValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 1,0,0,0
                if (actionFilter.Text != "All" && projectFilter.Text == "All" && sentFromFilter.Text == "All" && sentToFilter.Text == "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + actionQuery + " = @actionValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 0,0,1,1
                if (actionFilter.Text == "All" && projectFilter.Text == "All" && sentFromFilter.Text != "All" && sentToFilter.Text != "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + sentFromQuery + " = @sentFromValue AND " + sentToQuery + " = @sentToValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 0,1,0,1
                if (actionFilter.Text == "All" && projectFilter.Text != "All" && sentFromFilter.Text == "All" && sentToFilter.Text != "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + projectQuery + " = @projectValue AND " + sentToQuery + " = @sentToValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 1,0,0,1
                if (actionFilter.Text != "All" && projectFilter.Text == "All" && sentFromFilter.Text == "All" && sentToFilter.Text != "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + actionQuery + " = @actionValue AND " + sentToQuery + " = @sentToValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 0,1,1,0
                if (actionFilter.Text == "All" && projectFilter.Text != "All" && sentFromFilter.Text != "All" && sentToFilter.Text == "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + projectQuery + " = @projectValue AND " + sentFromQuery + " = @sentFromValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 1,0,1,0
                if (actionFilter.Text != "All" && projectFilter.Text == "All" && sentFromFilter.Text != "All" && sentToFilter.Text == "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + actionQuery + " = @actionValue AND " + sentFromQuery + " = @sentFromValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 1,1,0,0
                if (actionFilter.Text != "All" && projectFilter.Text != "All" && sentFromFilter.Text == "All" && sentToFilter.Text == "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + actionQuery + " = @actionValue AND " + projectQuery + " = @projectValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 0,1,1,1
                if (actionFilter.Text == "All" && projectFilter.Text != "All" && sentFromFilter.Text != "All" && sentToFilter.Text != "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + projectQuery + " = @projectValue AND " + sentFromQuery + " = @sentFromValue AND " + sentToQuery + " = @sentToValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 1,0,1,1
                if (actionFilter.Text != "All" && projectFilter.Text == "All" && sentFromFilter.Text != "All" && sentToFilter.Text != "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + actionQuery + " = @actionValue AND " + sentFromQuery + " = @sentFromValue AND " + sentToQuery + " = @sentToValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 1,1,0,1
                if (actionFilter.Text != "All" && projectFilter.Text != "All" && sentFromFilter.Text == "All" && sentToFilter.Text != "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + actionQuery + " = @actionValue AND " + projectQuery + " = @projectValue AND " + sentToQuery + " = @sentToValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 1,1,1,0
                if (actionFilter.Text != "All" && projectFilter.Text != "All" && sentFromFilter.Text != "All" && sentToFilter.Text == "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + actionQuery + " = @actionValue AND " + projectQuery + " = @projectValue AND " + sentFromQuery + " = @sentFromValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);
                }

                // 1,1,1,1
                if (actionFilter.Text != "All" && projectFilter.Text != "All" && sentFromFilter.Text != "All" && sentToFilter.Text != "All")
                {
                    using (SqlConnection connection = new SqlConnection(mainconn))
                    using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionQuery + " AS Action, " + projectQuery + "AS Project, " + sentFromQuery + " AS 'Sent From', " + sentToQuery + " AS 'Sent To ', " + sentToQuery + " AS 'Sent To', " + subjectQuery + " AS 'Subject', " + attachmentQuery + " AS 'Attachments', " + searchKeyQuery + " AS 'Search Key' FROM [dbo].[EventViewer] WHERE ID = 1112 AND " + actionQuery + " = @actionValue AND " + projectQuery + " = @projectValue AND " + sentFromQuery + " = @sentFromValue AND " + sentToQuery + " = @sentToValue ORDER BY 'Date & Time' DESC", connection))
                    {
                        connection.Open();
                        sqlcomm.Parameters.AddWithValue("@keywordAction1st", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAction2nd", "Reason: -");
                        sqlcomm.Parameters.AddWithValue("@keywordProject1st", "Label:  - ");
                        sqlcomm.Parameters.AddWithValue("@keywordProject2nd", "Event Details:");
                        sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                        sqlcomm.Parameters.AddWithValue("@keywordTo1st", "Failed Recipients: ");
                        sqlcomm.Parameters.AddWithValue("@keywordTo2nd", "Advisory: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject1st", "Subject: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSubject2nd", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment1st", "Attachments: ");
                        sqlcomm.Parameters.AddWithValue("@keywordAttachment2nd", "Rule: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch1st", "Search Key: ");
                        sqlcomm.Parameters.AddWithValue("@keywordSearch2nd", "Classifier-DocId: ");

                        sqlcomm.Parameters.AddWithValue("@actionValue", actionFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@projectValue", projectFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentFromValue", sentFromFilter.Text);
                        sqlcomm.Parameters.AddWithValue("@sentToValue", sentToFilter.Text);

                        sqlcomm.ExecuteNonQuery();
                        SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                        DataTable dt = new DataTable();
                        sdr.Fill(dt);
                        dataGridReporting.DataSource = dt;
                        connection.Close();
                    }
                    removeDividerLabel();
                    divider(dataGridReporting);

                }
                for (int i = 0; i < dataGridReporting.Rows.Count; i++)
                {
                    if (dataGridReporting.Rows[i].Cells[4].Value.ToString().Contains(","))
                    {
                        dataGridReporting.Rows[i].Cells[4].Value = "Multiple Users";
                    }
                }
                
                dataGridReporting.Columns[5].Visible = false;
                dataGridReporting.Columns[6].Visible = false;
                dataGridReporting.Columns[7].Visible = false;
                dataGridReporting.Columns[8].Visible = false;
                

                dataGridReporting.Columns[0].Width = dateWidth;
                dataGridReporting.Columns[1].Width = actionWidth;
                dataGridReporting.Columns[2].Width = projectWidth;
                dataGridReporting.Columns[3].Width = sentFromWidth;

                //dataGridReporting.Columns[4].Width = sentToWidth;
                dataGridReporting.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            
            // Documents Queries
            if (reportFilter.Text == "Documents")
            {
                using (SqlConnection connection = new SqlConnection(mainconn))
                using (SqlCommand sqlcomm = new SqlCommand("SELECT TimeCreated AS 'Date & Time', " + actionDocQuery + " AS 'Action', " + fileNameQuery + "AS 'File', " + userQuery + " AS 'User', " + newLabelQuery + " AS 'New Label', " + previousLabelQuery + " AS 'Previous Label', " + filePathQuery + " AS 'File Path'  FROM [dbo].[EventViewer] WHERE ID = 2100 ORDER BY 'Date & Time' DESC", connection))
                {
                    connection.Open();
                    sqlcomm.Parameters.AddWithValue("@keywordActionDoc1st", "File Name: ");
                    sqlcomm.Parameters.AddWithValue("@keywordActionDoc2nd", "Classifier-DocId:");

                    sqlcomm.Parameters.AddWithValue("@keywordFile1st", "File Name: ");
                    sqlcomm.Parameters.AddWithValue("@keywordFile2nd", "Classifier-DocId:");

                    sqlcomm.Parameters.AddWithValue("@keywordUser1st", "Username: REGRISKTECH\\");
                    sqlcomm.Parameters.AddWithValue("@keywordUser2nd", "File: ");

                    sqlcomm.Parameters.AddWithValue("@keywordNewLabel1st", "Label: ");
                    sqlcomm.Parameters.AddWithValue("@keywordNewLabel2nd", "Previous Label:");

                    sqlcomm.Parameters.AddWithValue("@keywordPreviousLabel1st", "Previous Label: ");
                    sqlcomm.Parameters.AddWithValue("@keywordPreviousLabel2nd", "Event Details:");

                    sqlcomm.Parameters.AddWithValue("@keywordFilePath1st", "File: ");
                    sqlcomm.Parameters.AddWithValue("@keywordFilePath2nd", "User Saver ID:");

                    sqlcomm.ExecuteNonQuery();
                    SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    dataGridReporting.DataSource = dt;
                    connection.Close();
                }

                // Action replacement
                for (int i = 0; i < dataGridReporting.Rows.Count; i++)
                {
                    if (dataGridReporting.Rows[i].Cells[4].Value.ToString() != "No Marking" && dataGridReporting.Rows[i].Cells[5].Value.ToString() == "No Marking")
                    {
                        dataGridReporting.Rows[i].Cells[1].Value = "Classified";
                    }
                    if (dataGridReporting.Rows[i].Cells[4].Value.ToString() != "No Marking" && dataGridReporting.Rows[i].Cells[5].Value.ToString() != "No Marking")
                    {
                        dataGridReporting.Rows[i].Cells[1].Value = "Reclassified";
                    }
                    if (dataGridReporting.Rows[i].Cells[4].Value.ToString() == "No Marking" && dataGridReporting.Rows[i].Cells[5].Value.ToString() != "No Marking")
                    {
                        dataGridReporting.Rows[i].Cells[1].Value = "Declassified";
                    }
                }

                dataGridReporting.Columns[6].Visible = false;
                // Date & Time
                dataGridReporting.Columns[0].Width = dateWidth;
                // Action
                dataGridReporting.Columns[1].Width = actionDocWidth;
                // File Name
                dataGridReporting.Columns[2].MinimumWidth = fileWidth;
                dataGridReporting.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                // User
                dataGridReporting.Columns[3].Width = userWidth;
                // New Label
                dataGridReporting.Columns[4].Width = newLabelWidth;
                // Previous Label
                dataGridReporting.Columns[5].Width = prevLabelWidth;

                Regex reg = new Regex("[*'\",_&#^@%]");

                string actionfilterVal = reg.Replace(actionDocFilter.Text, string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                string filefilterVal = reg.Replace(fileFilter.Text, string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                string userfilterVal = reg.Replace(userFilter.Text, string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                string newLabelfilterVal = reg.Replace(newLabelFilter.Text, string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                string prevLabelfilterVal = reg.Replace(prevLabelFilter.Text, string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);

                BindingSource bs = new BindingSource();
                bs.DataSource = dataGridReporting.DataSource;
                

                #region File Extensions
                if (filefilterVal == "Excel")
                {
                    filefilterVal = ".xlsx";
                }

                if (filefilterVal == "Image")
                {
                    filefilterVal = ".png";
                }

                if (filefilterVal == "PDF")
                {
                    filefilterVal = ".pdf";
                }

                if (filefilterVal == "Powerpoint")
                {
                    filefilterVal = ".pptx";
                }

                if (filefilterVal == "Text")
                {
                    filefilterVal = ".txt";
                }

                if (filefilterVal == "Word")
                {
                    filefilterVal = ".docx";
                }

                #endregion

                // 0,0,0,0,0
                if (actionDocFilter.Text == "All" && fileFilter.Text == "All" && userFilter.Text == "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text == "All")
                {
                    // No filter required
                }

                // 0,0,0,0,1
                if (actionDocFilter.Text == "All" && fileFilter.Text == "All" && userFilter.Text == "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 0,0,0,1,0
                if (actionDocFilter.Text == "All" && fileFilter.Text == "All" && userFilter.Text == "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[New Label] = '" + newLabelfilterVal + "'";
                }

                // 0,0,1,0,0
                if (actionDocFilter.Text == "All" && fileFilter.Text == "All" && userFilter.Text != "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[User] = '" + userfilterVal + "'";
                }

                // 0,1,0,0,0
                if (actionDocFilter.Text == "All" && fileFilter.Text != "All" && userFilter.Text == "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[File] LIKE '%" + filefilterVal + "'";
                }

                // 1,0,0,0,0
                if (actionDocFilter.Text != "All" && fileFilter.Text == "All" && userFilter.Text == "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text == "All")
                {
                    if (actionfilterVal == "Declassified")
                    {
                        //bs.Filter = "[New Label] = 'No Marking'";
                        bs.Filter = "[Action] = '" + actionfilterVal + "'";
                    }
                    else 
                    {
                        bs.Filter = "[Action] = '" + actionfilterVal + "'";
                    }
                }

                // 0,0,0,1,1
                if (actionDocFilter.Text == "All" && fileFilter.Text == "All" && userFilter.Text == "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[New Label] = '" + newLabelfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 0,0,1,0,1
                if (actionDocFilter.Text == "All" && fileFilter.Text == "All" && userFilter.Text != "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[User] = '" + userfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 0,1,0,0,1
                if (actionDocFilter.Text == "All" && fileFilter.Text != "All" && userFilter.Text == "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[File] LIKE '%" + filefilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 1,0,0,0,1
                if (actionDocFilter.Text != "All" && fileFilter.Text == "All" && userFilter.Text == "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 0,0,1,1,0
                if (actionDocFilter.Text == "All" && fileFilter.Text == "All" && userFilter.Text != "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[User] = '" + userfilterVal + "' AND [New Label] = '" + newLabelfilterVal + "'";
                }

                // 0,1,0,1,0
                if (actionDocFilter.Text == "All" && fileFilter.Text != "All" && userFilter.Text == "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[File] LIKE '%" + filefilterVal + "' AND [New Label] = '" + newLabelfilterVal + "'";
                }

                // 1,0,0,1,0
                if (actionDocFilter.Text != "All" && fileFilter.Text == "All" && userFilter.Text == "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [New Label] = '" + newLabelfilterVal + "'";
                }

                // 0,1,1,0,0
                if (actionDocFilter.Text == "All" && fileFilter.Text != "All" && userFilter.Text != "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[File] LIKE '%" + filefilterVal + "' AND [User] = '" + userfilterVal + "'";
                }

                // 1,0,1,0,0
                if (actionDocFilter.Text != "All" && fileFilter.Text == "All" && userFilter.Text != "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [User] = '" + userfilterVal + "'";
                }

                // 1,1,0,0,0
                if (actionDocFilter.Text != "All" && fileFilter.Text != "All" && userFilter.Text == "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [File] LIKE '%" + filefilterVal + "'";
                }

                // 0,0,1,1,1
                if (actionDocFilter.Text == "All" && fileFilter.Text == "All" && userFilter.Text != "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[User] = '" + userfilterVal + "' AND [New Label] = '" + newLabelfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 0,1,0,1,1
                if (actionDocFilter.Text == "All" && fileFilter.Text != "All" && userFilter.Text == "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[File] LIKE '%" + filefilterVal + "' AND [New Label] = '" + newLabelfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 1,0,0,1,1
                if (actionDocFilter.Text != "All" && fileFilter.Text == "All" && userFilter.Text == "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [New Label] = '" + newLabelfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 0,1,1,0,1
                if (actionDocFilter.Text == "All" && fileFilter.Text != "All" && userFilter.Text != "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[File] LIKE '%" + filefilterVal + "' AND [User] = '" + userfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 1,0,1,0,1
                if (actionDocFilter.Text != "All" && fileFilter.Text == "All" && userFilter.Text != "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [User] = '" + userfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 1,1,0,0,1
                if (actionDocFilter.Text != "All" && fileFilter.Text != "All" && userFilter.Text == "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [File] LIKE '%" + filefilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 0,1,1,1,0
                if (actionDocFilter.Text == "All" && fileFilter.Text != "All" && userFilter.Text != "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[File] LIKE '%" + filefilterVal + "' AND [User] = '" + userfilterVal + "' AND [New Label] = '" + newLabelfilterVal + "'";
                }

                // 1,1,0,1,0
                if (actionDocFilter.Text != "All" && fileFilter.Text != "All" && userFilter.Text == "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [File] LIKE '%" + filefilterVal + "' AND [New Label] = '" + newLabelfilterVal + "'";
                }

                // 1,0,1,1,0
                if (actionDocFilter.Text != "All" && fileFilter.Text == "All" && userFilter.Text != "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [User] = '" + userfilterVal + "' AND [New Label] = '" + newLabelfilterVal + "'";
                }

                // 1,1,1,0,0
                if (actionDocFilter.Text != "All" && fileFilter.Text != "All" && userFilter.Text != "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [File] LIKE '%" + filefilterVal + "' AND [User] = '" + userfilterVal + "'";
                }

                // 0,1,1,1,1
                if (actionDocFilter.Text == "All" && fileFilter.Text != "All" && userFilter.Text != "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[File] LIKE '%" + filefilterVal + "' AND [User] = '" + userfilterVal + "' AND [New Label] = '" + newLabelfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 1,0,1,1,1
                if (actionDocFilter.Text != "All" && fileFilter.Text == "All" && userFilter.Text != "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [User] = '" + userfilterVal + "' AND [New Label] = '" + newLabelfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 1,1,0,1,1
                if (actionDocFilter.Text != "All" && fileFilter.Text == "All" && userFilter.Text == "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [File] LIKE '%" + filefilterVal + "'  AND [New Label] = '" + newLabelfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 1,1,1,0,1
                if (actionDocFilter.Text != "All" && fileFilter.Text != "All" && userFilter.Text != "All" && newLabelFilter.Text == "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [File] LIKE '%" + filefilterVal + "'  AND [User] = '" + userfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                // 1,1,1,1,0
                if (actionDocFilter.Text != "All" && fileFilter.Text != "All" && userFilter.Text != "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text == "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [File] LIKE '%" + filefilterVal + "'  AND [User] = '" + userfilterVal + "' AND [New Label] = '" + newLabelfilterVal + "'";
                }

                // 1,1,1,1,1
                if (actionDocFilter.Text != "All" && fileFilter.Text != "All" && userFilter.Text != "All" && newLabelFilter.Text != "All" && prevLabelFilter.Text != "All")
                {
                    bs.Filter = "[Action] = '" + actionfilterVal + "' AND [File] LIKE '%" + filefilterVal + "'  AND [User] = '" + userfilterVal + "' AND [New Label] = '" + newLabelfilterVal + "' AND [Previous Label] = '" + prevLabelfilterVal + "'";
                }

                dataGridReporting.DataSource = bs;

                removeDividerLabel();
                divider(dataGridReporting);

                ///////////////////////////////////////////////////////
            }

            filterDataGrid();
        }

        private void reportFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reportFilter.IsHandleCreated && reportFilter.Focused)
            {
                swapFilters(reportFilter.Text);

                reportQuery();
                comboFill(dataGridReporting);
            }
        }

        private void actionFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (actionFilter.IsHandleCreated && actionFilter.Focused)
            {
                reportQuery();
                comboFill(dataGridReporting);
            }
        }

        private void projectFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (projectFilter.IsHandleCreated && projectFilter.Focused)
            {
                reportQuery();
                comboFill(dataGridReporting);
            }
        }

        private void sentFromFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sentFromFilter.IsHandleCreated && sentFromFilter.Focused)
            {
                reportQuery();
                comboFill(dataGridReporting);
            }
        }

        private void sentToFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sentToFilter.IsHandleCreated && sentToFilter.Focused)
            {
                reportQuery();
                comboFill(dataGridReporting);
            }

        }

        /// ///////////////////////////////////////////////////////////////////////////////////////////

        private void actionDocFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (actionDocFilter.IsHandleCreated && actionDocFilter.Focused)
            {
                reportQuery();
                comboFill(dataGridReporting);
            }
        }

        private void fileFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileFilter.IsHandleCreated && fileFilter.Focused)
            {
                reportQuery();
                comboFill(dataGridReporting);
            }
        }

        private void userFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userFilter.IsHandleCreated && userFilter.Focused)
            {
                reportQuery();
                comboFill(dataGridReporting);
            }
        }

        private void newLabelFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newLabelFilter.IsHandleCreated && newLabelFilter.Focused)
            {
                reportQuery();
                comboFill(dataGridReporting);
            }
        }

        private void prevLabelFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prevLabelFilter.IsHandleCreated && prevLabelFilter.Focused)
            {
                reportQuery();
                comboFill(dataGridReporting);
            }
        }

        private void divider(DataGridView dataGridTable)
        {
            int numRows = dataGridTable.Rows.GetRowCount(DataGridViewElementStates.Visible);
            int x = 46;
            int y = panelTop.Height + 33;

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

                    y += dataGridTable.Rows[i].Height;
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

        private void Reporting_Resize(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls.OfType<Label>())
            {
                if (item.Name == "seperatorLine")
                {
                    item.Width = this.Width - 90;
                }
            }

            dataGridReporting.Width = this.Width - 10;
            dataGridReporting.Height = this.Height - 186;

            if (dataGridReporting.Columns.Contains("User"))
            {
                int user_x = dataGridReporting.Location.X + dataGridReporting.Columns[0].Width + dataGridReporting.Columns[1].Width + dataGridReporting.Columns[2].Width + 5;
                userFilter.Location = new Point(user_x, 110);

                int newLabel_x = user_x + dataGridReporting.Columns[3].Width;
                newLabelFilter.Location = new Point(newLabel_x, 110);

                int prevLabel_x = newLabel_x + dataGridReporting.Columns[4].Width;
                prevLabelFilter.Location = new Point(prevLabel_x, 110);

                clearAllBtn.Location = new Point(prevLabel_x-17, 77);

            }


        }

        private void reportingSearchPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (reportingSearchTxt.Text == "Search")
            {
                reportingSearchTxt.Text = string.Empty;
                reportingSearchTxt.ForeColor = Color.Black;
            }
        }

        private void reportingSearchTxt_MouseClick(object sender, MouseEventArgs e)
        {
            if (reportingSearchTxt.Text == "Search")
            {
                reportingSearchTxt.Text = string.Empty;
                reportingSearchTxt.ForeColor = Color.Black;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            int rowCount = dataGridReporting.Rows.Count;
            int colCount = dataGridReporting.Columns.Count;
            if (rowCount > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);
                if(reportFilter.Text == "Emails")
                {
                    for (int i = 1; i < colCount + 1; i++)
                    {
                        if (i != 5)
                        {
                            xcelApp.Cells[1, i] = dataGridReporting.Columns[i - 1].HeaderText;
                        }
                    }
                    for (int i = 0; i < rowCount; i++)
                    {
                        for (int j = 0; j < colCount; j++)
                        {
                            if (j != 4)
                            {
                                xcelApp.Cells[i + 2, j + 1] = dataGridReporting.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                    }
                    xcelApp.Columns["E"].Delete();

                    xcelApp.get_Range("A1", "E" + rowCount + 1).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    xcelApp.get_Range("A1", "E" + rowCount + 1).Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;

                    xcelApp.Columns.AutoFit();
                    xcelApp.Columns[5].ColumnWidth = 33.33;
                    xcelApp.Rows.AutoFit();
                    xcelApp.Visible = true;
                }
                else
                {
                    for (int i = 1; i < colCount + 1; i++)
                    {
                        xcelApp.Cells[1, i] = dataGridReporting.Columns[i - 1].HeaderText;
                    }
                    for (int i = 0; i < rowCount; i++)
                    {
                        for (int j = 0; j < colCount; j++)
                        {
                            xcelApp.Cells[i + 2, j + 1] = dataGridReporting.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    xcelApp.get_Range("A1", "F" + rowCount + 1).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    xcelApp.get_Range("A1", "F" + rowCount + 1).Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;

                    xcelApp.Columns.AutoFit();
                    //xcelApp.Columns[5].ColumnWidth = 33.33;
                    xcelApp.Rows.AutoFit();
                    xcelApp.Visible = true;
                }

            }
        }

        private void clearAllBtn_Click(object sender, EventArgs e)
        {
            ReportingLbl.Focus();

            reportingSearchTxt.Text = "";
            actionFilter.SelectedIndex = 0;
            projectFilter.SelectedIndex = 0;
            sentFromFilter.SelectedIndex = 0;
            sentToFilter.SelectedIndex = 0;

            actionDocFilter.SelectedIndex = 0;
            fileFilter.SelectedIndex = 0;
            userFilter.SelectedIndex = 0;
            newLabelFilter.SelectedIndex = 0;
            prevLabelFilter.SelectedIndex = 0;

            reportQuery();
        }

        private void reportingSearchTxt_TextChanged(object sender, EventArgs e)
        {
            filterDataGrid();
        }

        private void filterDataGrid()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridReporting.DataSource;

            Regex reg = new Regex("[*'\",_&#^@%]");
            string filterVal = reg.Replace(reportingSearchTxt.Text, string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);


            if (reportingSearchTxt.Text != "" && reportingSearchTxt.Text != "Search")
            {
                if (reportFilter.Text == "Emails")
                {
                    bs.Filter = "Action LIKE '%" + filterVal + "%' OR Project LIKE '%" + filterVal + "%' OR [Sent From] LIKE '%" + filterVal + "%' OR [Sent To] LIKE '%" + filterVal + "%'";
                }
                else
                {
                    bs.Filter = "Action LIKE '%" + filterVal + "%' OR File LIKE '%" + filterVal + "%' OR User LIKE '%" + filterVal + "%' OR [New Label] LIKE '%" + filterVal + "%' OR [Previous Label] LIKE '%" + filterVal + "%'";
                }
                dataGridReporting.DataSource = bs;

                removeDividerLabel();

                divider(dataGridReporting);
            }
        }
    }
}
