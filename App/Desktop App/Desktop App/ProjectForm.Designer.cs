namespace Desktop_App
{
    partial class ProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.dataGridProjects = new System.Windows.Forms.DataGridView();
            this.ProjectLbl = new System.Windows.Forms.Label();
            this.projectSearchPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.projectSearchTxt = new System.Windows.Forms.TextBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.newProjectLbl = new System.Windows.Forms.Label();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProjects)).BeginInit();
            this.projectSearchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.dataGridProjects);
            resources.ApplyResources(this.panelBottom, "panelBottom");
            this.panelBottom.Name = "panelBottom";
            // 
            // dataGridProjects
            // 
            this.dataGridProjects.AllowUserToAddRows = false;
            this.dataGridProjects.AllowUserToDeleteRows = false;
            this.dataGridProjects.AllowUserToResizeColumns = false;
            this.dataGridProjects.AllowUserToResizeRows = false;
            this.dataGridProjects.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridProjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridProjects.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridProjects.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataGridProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridProjects.ColumnHeadersVisible = false;
            resources.ApplyResources(this.dataGridProjects, "dataGridProjects");
            this.dataGridProjects.MultiSelect = false;
            this.dataGridProjects.Name = "dataGridProjects";
            this.dataGridProjects.ReadOnly = true;
            this.dataGridProjects.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridProjects.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridProjects.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridProjects.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Open Sans SemiBold", 16F, System.Drawing.FontStyle.Bold);
            this.dataGridProjects.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridProjects.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.dataGridProjects.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridProjects.RowTemplate.Height = 69;
            this.dataGridProjects.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellMouseLeave);
            this.dataGridProjects.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseClick);
            this.dataGridProjects.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellMouseLeave);
            this.dataGridProjects.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseMove);
            this.dataGridProjects.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridProjects_Scroll);
            // 
            // ProjectLbl
            // 
            resources.ApplyResources(this.ProjectLbl, "ProjectLbl");
            this.ProjectLbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.ProjectLbl.Name = "ProjectLbl";
            // 
            // projectSearchPanel
            // 
            this.projectSearchPanel.BackColor = System.Drawing.Color.White;
            this.projectSearchPanel.Controls.Add(this.pictureBox1);
            this.projectSearchPanel.Controls.Add(this.projectSearchTxt);
            resources.ApplyResources(this.projectSearchPanel, "projectSearchPanel");
            this.projectSearchPanel.Name = "projectSearchPanel";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // projectSearchTxt
            // 
            this.projectSearchTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.projectSearchTxt, "projectSearchTxt");
            this.projectSearchTxt.Name = "projectSearchTxt";
            this.projectSearchTxt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.projectSearchTxt_MouseClick);
            this.projectSearchTxt.TextChanged += new System.EventHandler(this.projectSearchTxt_TextChanged);
            this.projectSearchTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.projectSearchTxt_KeyDown);
            this.projectSearchTxt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.projectSearchTxt_MouseDown);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Control;
            this.panelTop.Controls.Add(this.newProjectLbl);
            this.panelTop.Controls.Add(this.projectSearchPanel);
            this.panelTop.Controls.Add(this.ProjectLbl);
            resources.ApplyResources(this.panelTop, "panelTop");
            this.panelTop.Name = "panelTop";
            // 
            // newProjectLbl
            // 
            resources.ApplyResources(this.newProjectLbl, "newProjectLbl");
            this.newProjectLbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.newProjectLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.newProjectLbl.Name = "newProjectLbl";
            this.newProjectLbl.Click += new System.EventHandler(this.newProjectLbl_Click);
            // 
            // ProjectForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "ProjectForm";
            this.Load += new System.EventHandler(this.ProjectForm_Load);
            this.Shown += new System.EventHandler(this.ProjectForm_Shown);
            this.Resize += new System.EventHandler(this.ProjectForm_Resize);
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProjects)).EndInit();
            this.projectSearchPanel.ResumeLayout(false);
            this.projectSearchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label ProjectLbl;
        private System.Windows.Forms.Panel projectSearchPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox projectSearchTxt;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.DataGridView dataGridProjects;
        private System.Windows.Forms.Label newProjectLbl;
    }
}