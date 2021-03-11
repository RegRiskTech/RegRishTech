namespace Desktop_App
{
    partial class UserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.newUserLbl = new System.Windows.Forms.Label();
            this.userSearchPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.userSearchTxt = new System.Windows.Forms.TextBox();
            this.UsersLbl = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridUsers = new System.Windows.Forms.DataGridView();
            this.userSearchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.topPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // newUserLbl
            // 
            this.newUserLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newUserLbl.AutoSize = true;
            this.newUserLbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.newUserLbl.Font = new System.Drawing.Font("Open Sans", 13.8F);
            this.newUserLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.newUserLbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.newUserLbl.Location = new System.Drawing.Point(499, 39);
            this.newUserLbl.Name = "newUserLbl";
            this.newUserLbl.Size = new System.Drawing.Size(155, 26);
            this.newUserLbl.TabIndex = 11;
            this.newUserLbl.Text = "New Member  +";
            this.newUserLbl.Click += new System.EventHandler(this.newUserLbl_Click);
            // 
            // userSearchPanel
            // 
            this.userSearchPanel.BackColor = System.Drawing.Color.White;
            this.userSearchPanel.Controls.Add(this.pictureBox1);
            this.userSearchPanel.Controls.Add(this.userSearchTxt);
            this.userSearchPanel.Font = new System.Drawing.Font("Open Sans SemiBold", 7.2F, System.Drawing.FontStyle.Bold);
            this.userSearchPanel.Location = new System.Drawing.Point(362, 68);
            this.userSearchPanel.Name = "userSearchPanel";
            this.userSearchPanel.Padding = new System.Windows.Forms.Padding(26, 4, 10, 3);
            this.userSearchPanel.Size = new System.Drawing.Size(292, 25);
            this.userSearchPanel.TabIndex = 8;
            this.userSearchPanel.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(7, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // userSearchTxt
            // 
            this.userSearchTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userSearchTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userSearchTxt.Font = new System.Drawing.Font("Open Sans", 11.8F);
            this.userSearchTxt.Location = new System.Drawing.Point(26, 4);
            this.userSearchTxt.Margin = new System.Windows.Forms.Padding(50, 3, 3, 3);
            this.userSearchTxt.Multiline = true;
            this.userSearchTxt.Name = "userSearchTxt";
            this.userSearchTxt.Size = new System.Drawing.Size(256, 18);
            this.userSearchTxt.TabIndex = 7;
            this.userSearchTxt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.userSearchTxt_MouseClick);
            this.userSearchTxt.TextChanged += new System.EventHandler(this.userSearchTxt_TextChanged);
            this.userSearchTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userSearchTxt_KeyDown);
            this.userSearchTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.userSearchTxt_MouseDown);
            // 
            // UsersLbl
            // 
            this.UsersLbl.AutoSize = true;
            this.UsersLbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.UsersLbl.Font = new System.Drawing.Font("Open Sans", 24F);
            this.UsersLbl.ForeColor = System.Drawing.Color.Black;
            this.UsersLbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UsersLbl.Location = new System.Drawing.Point(31, 22);
            this.UsersLbl.Name = "UsersLbl";
            this.UsersLbl.Size = new System.Drawing.Size(162, 43);
            this.UsersLbl.TabIndex = 6;
            this.UsersLbl.Text = "Members";
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.Control;
            this.topPanel.Controls.Add(this.newUserLbl);
            this.topPanel.Controls.Add(this.userSearchPanel);
            this.topPanel.Controls.Add(this.UsersLbl);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(946, 104);
            this.topPanel.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridUsers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Open Sans SemiBold", 13.8F, System.Drawing.FontStyle.Bold);
            this.panel1.Location = new System.Drawing.Point(0, 104);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.panel1.Size = new System.Drawing.Size(946, 581);
            this.panel1.TabIndex = 9;
            // 
            // dataGridUsers
            // 
            this.dataGridUsers.AllowUserToAddRows = false;
            this.dataGridUsers.AllowUserToDeleteRows = false;
            this.dataGridUsers.AllowUserToResizeColumns = false;
            this.dataGridUsers.AllowUserToResizeRows = false;
            this.dataGridUsers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridUsers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataGridUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridUsers.ColumnHeadersVisible = false;
            this.dataGridUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridUsers.Location = new System.Drawing.Point(30, 0);
            this.dataGridUsers.MultiSelect = false;
            this.dataGridUsers.Name = "dataGridUsers";
            this.dataGridUsers.ReadOnly = true;
            this.dataGridUsers.RowHeadersVisible = false;
            this.dataGridUsers.RowHeadersWidth = 51;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridUsers.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridUsers.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridUsers.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Open Sans SemiBold", 16F, System.Drawing.FontStyle.Bold);
            this.dataGridUsers.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridUsers.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.dataGridUsers.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridUsers.RowTemplate.Height = 69;
            this.dataGridUsers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridUsers.Size = new System.Drawing.Size(916, 581);
            this.dataGridUsers.TabIndex = 9;
            this.dataGridUsers.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridUsers_CellMouseClick_1);
            this.dataGridUsers.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridUsers_CellMouseLeave_1);
            this.dataGridUsers.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridUsers_CellMouseMove_1);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 685);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UserForm";
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.Shown += new System.EventHandler(this.UserForm_Shown);
            this.Resize += new System.EventHandler(this.UserForm_Resize);
            this.userSearchPanel.ResumeLayout(false);
            this.userSearchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label newUserLbl;
        private System.Windows.Forms.Panel userSearchPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox userSearchTxt;
        private System.Windows.Forms.Label UsersLbl;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridUsers;
    }
}