namespace Desktop_App
{
    partial class ProjectAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectAdd));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.projectNameLbl = new System.Windows.Forms.Label();
            this.projectNameTxt = new System.Windows.Forms.TextBox();
            this.projectTypeLbl = new System.Windows.Forms.Label();
            this.projectDescTxt = new System.Windows.Forms.TextBox();
            this.projectDescLbl = new System.Windows.Forms.Label();
            this.projectSensLbl = new System.Windows.Forms.Label();
            this.dataGridProjectIDMax = new System.Windows.Forms.DataGridView();
            this.newProjectIDLbl = new System.Windows.Forms.TextBox();
            this.dataGridInfo = new System.Windows.Forms.DataGridView();
            this.updateBtn = new System.Windows.Forms.Button();
            this.submitBtn = new System.Windows.Forms.Button();
            this.projectIDTxt = new System.Windows.Forms.TextBox();
            this.projectOpenDateTxt = new System.Windows.Forms.TextBox();
            this.charRemainingDescLbl = new System.Windows.Forms.Label();
            this.uniqueProjectLbl = new System.Windows.Forms.Label();
            this.dataGridUniqueProject = new System.Windows.Forms.DataGridView();
            this.blankLbl = new System.Windows.Forms.Label();
            this.charRemainingNameLbl = new System.Windows.Forms.Label();
            this.backBtn = new System.Windows.Forms.Label();
            this.projectNameAsteriskLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProjectIDMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUniqueProject)).BeginInit();
            this.SuspendLayout();
            // 
            // projectNameLbl
            // 
            resources.ApplyResources(this.projectNameLbl, "projectNameLbl");
            this.projectNameLbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.projectNameLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.projectNameLbl.Name = "projectNameLbl";
            // 
            // projectNameTxt
            // 
            resources.ApplyResources(this.projectNameTxt, "projectNameTxt");
            this.projectNameTxt.Name = "projectNameTxt";
            this.projectNameTxt.TextChanged += new System.EventHandler(this.projectNameTxt_TextChanged);
            this.projectNameTxt.Enter += new System.EventHandler(this.projectNameTxt_Enter);
            this.projectNameTxt.Leave += new System.EventHandler(this.projectNameTxt_Leave);
            // 
            // projectTypeLbl
            // 
            resources.ApplyResources(this.projectTypeLbl, "projectTypeLbl");
            this.projectTypeLbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.projectTypeLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.projectTypeLbl.Name = "projectTypeLbl";
            // 
            // projectDescTxt
            // 
            resources.ApplyResources(this.projectDescTxt, "projectDescTxt");
            this.projectDescTxt.Name = "projectDescTxt";
            this.projectDescTxt.TextChanged += new System.EventHandler(this.projectDescTxt_TextChanged);
            this.projectDescTxt.Enter += new System.EventHandler(this.projectDescTxt_Enter);
            this.projectDescTxt.Leave += new System.EventHandler(this.projectDescTxt_Leave);
            // 
            // projectDescLbl
            // 
            resources.ApplyResources(this.projectDescLbl, "projectDescLbl");
            this.projectDescLbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.projectDescLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.projectDescLbl.Name = "projectDescLbl";
            // 
            // projectSensLbl
            // 
            resources.ApplyResources(this.projectSensLbl, "projectSensLbl");
            this.projectSensLbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.projectSensLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.projectSensLbl.Name = "projectSensLbl";
            // 
            // dataGridProjectIDMax
            // 
            this.dataGridProjectIDMax.AllowUserToAddRows = false;
            this.dataGridProjectIDMax.AllowUserToDeleteRows = false;
            this.dataGridProjectIDMax.AllowUserToResizeColumns = false;
            this.dataGridProjectIDMax.AllowUserToResizeRows = false;
            this.dataGridProjectIDMax.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridProjectIDMax.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridProjectIDMax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridProjectIDMax.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridProjectIDMax.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Open Sans", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(149)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridProjectIDMax.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridProjectIDMax.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridProjectIDMax.ColumnHeadersVisible = false;
            resources.ApplyResources(this.dataGridProjectIDMax, "dataGridProjectIDMax");
            this.dataGridProjectIDMax.MultiSelect = false;
            this.dataGridProjectIDMax.Name = "dataGridProjectIDMax";
            this.dataGridProjectIDMax.ReadOnly = true;
            this.dataGridProjectIDMax.RowHeadersVisible = false;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridProjectIDMax.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridProjectIDMax.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridProjectIDMax.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Open Sans", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridProjectIDMax.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridProjectIDMax.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.dataGridProjectIDMax.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridProjectIDMax.RowTemplate.Height = 67;
            // 
            // newProjectIDLbl
            // 
            this.newProjectIDLbl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.newProjectIDLbl, "newProjectIDLbl");
            this.newProjectIDLbl.ForeColor = System.Drawing.SystemColors.Control;
            this.newProjectIDLbl.Name = "newProjectIDLbl";
            this.newProjectIDLbl.ReadOnly = true;
            // 
            // dataGridInfo
            // 
            this.dataGridInfo.AllowUserToAddRows = false;
            this.dataGridInfo.AllowUserToDeleteRows = false;
            this.dataGridInfo.AllowUserToResizeColumns = false;
            this.dataGridInfo.AllowUserToResizeRows = false;
            this.dataGridInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridInfo.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridInfo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataGridInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridInfo.ColumnHeadersVisible = false;
            resources.ApplyResources(this.dataGridInfo, "dataGridInfo");
            this.dataGridInfo.MultiSelect = false;
            this.dataGridInfo.Name = "dataGridInfo";
            this.dataGridInfo.ReadOnly = true;
            this.dataGridInfo.RowHeadersVisible = false;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridInfo.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridInfo.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridInfo.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Open Sans SemiBold", 16F, System.Drawing.FontStyle.Bold);
            this.dataGridInfo.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridInfo.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.dataGridInfo.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridInfo.RowTemplate.DividerHeight = 1;
            this.dataGridInfo.RowTemplate.Height = 69;
            // 
            // updateBtn
            // 
            this.updateBtn.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.updateBtn, "updateBtn");
            this.updateBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.updateBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.TabStop = false;
            this.updateBtn.UseVisualStyleBackColor = false;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // submitBtn
            // 
            this.submitBtn.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.submitBtn, "submitBtn");
            this.submitBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.submitBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.TabStop = false;
            this.submitBtn.UseVisualStyleBackColor = false;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click_1);
            // 
            // projectIDTxt
            // 
            this.projectIDTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.projectIDTxt, "projectIDTxt");
            this.projectIDTxt.ForeColor = System.Drawing.SystemColors.Control;
            this.projectIDTxt.Name = "projectIDTxt";
            this.projectIDTxt.ReadOnly = true;
            // 
            // projectOpenDateTxt
            // 
            this.projectOpenDateTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.projectOpenDateTxt, "projectOpenDateTxt");
            this.projectOpenDateTxt.ForeColor = System.Drawing.SystemColors.Control;
            this.projectOpenDateTxt.Name = "projectOpenDateTxt";
            this.projectOpenDateTxt.ReadOnly = true;
            // 
            // charRemainingDescLbl
            // 
            resources.ApplyResources(this.charRemainingDescLbl, "charRemainingDescLbl");
            this.charRemainingDescLbl.Name = "charRemainingDescLbl";
            // 
            // uniqueProjectLbl
            // 
            resources.ApplyResources(this.uniqueProjectLbl, "uniqueProjectLbl");
            this.uniqueProjectLbl.ForeColor = System.Drawing.Color.Red;
            this.uniqueProjectLbl.Name = "uniqueProjectLbl";
            // 
            // dataGridUniqueProject
            // 
            this.dataGridUniqueProject.AllowUserToAddRows = false;
            this.dataGridUniqueProject.AllowUserToDeleteRows = false;
            this.dataGridUniqueProject.AllowUserToResizeColumns = false;
            this.dataGridUniqueProject.AllowUserToResizeRows = false;
            this.dataGridUniqueProject.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridUniqueProject.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridUniqueProject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridUniqueProject.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridUniqueProject.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Open Sans", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(149)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridUniqueProject.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridUniqueProject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridUniqueProject.ColumnHeadersVisible = false;
            resources.ApplyResources(this.dataGridUniqueProject, "dataGridUniqueProject");
            this.dataGridUniqueProject.MultiSelect = false;
            this.dataGridUniqueProject.Name = "dataGridUniqueProject";
            this.dataGridUniqueProject.ReadOnly = true;
            this.dataGridUniqueProject.RowHeadersVisible = false;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Britannic Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridUniqueProject.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridUniqueProject.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridUniqueProject.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Open Sans", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridUniqueProject.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridUniqueProject.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.dataGridUniqueProject.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridUniqueProject.RowTemplate.Height = 67;
            // 
            // blankLbl
            // 
            resources.ApplyResources(this.blankLbl, "blankLbl");
            this.blankLbl.ForeColor = System.Drawing.Color.Red;
            this.blankLbl.Name = "blankLbl";
            // 
            // charRemainingNameLbl
            // 
            resources.ApplyResources(this.charRemainingNameLbl, "charRemainingNameLbl");
            this.charRemainingNameLbl.Name = "charRemainingNameLbl";
            // 
            // backBtn
            // 
            resources.ApplyResources(this.backBtn, "backBtn");
            this.backBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.backBtn.Name = "backBtn";
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // projectNameAsteriskLbl
            // 
            resources.ApplyResources(this.projectNameAsteriskLbl, "projectNameAsteriskLbl");
            this.projectNameAsteriskLbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.projectNameAsteriskLbl.ForeColor = System.Drawing.Color.Black;
            this.projectNameAsteriskLbl.Name = "projectNameAsteriskLbl";
            // 
            // ProjectAdd
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.projectNameAsteriskLbl);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.charRemainingNameLbl);
            this.Controls.Add(this.blankLbl);
            this.Controls.Add(this.dataGridUniqueProject);
            this.Controls.Add(this.uniqueProjectLbl);
            this.Controls.Add(this.charRemainingDescLbl);
            this.Controls.Add(this.projectOpenDateTxt);
            this.Controls.Add(this.projectIDTxt);
            this.Controls.Add(this.dataGridInfo);
            this.Controls.Add(this.dataGridProjectIDMax);
            this.Controls.Add(this.newProjectIDLbl);
            this.Controls.Add(this.projectSensLbl);
            this.Controls.Add(this.projectDescTxt);
            this.Controls.Add(this.projectDescLbl);
            this.Controls.Add(this.projectTypeLbl);
            this.Controls.Add(this.projectNameTxt);
            this.Controls.Add(this.projectNameLbl);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.updateBtn);
            this.Name = "ProjectAdd";
            this.Load += new System.EventHandler(this.ProjectAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProjectIDMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUniqueProject)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label projectNameLbl;
        private System.Windows.Forms.TextBox projectNameTxt;
        private System.Windows.Forms.Label projectTypeLbl;
        private System.Windows.Forms.TextBox projectDescTxt;
        private System.Windows.Forms.Label projectDescLbl;
        private System.Windows.Forms.Label projectSensLbl;
        private System.Windows.Forms.DataGridView dataGridProjectIDMax;
        private System.Windows.Forms.TextBox newProjectIDLbl;
        private System.Windows.Forms.DataGridView dataGridInfo;
        private System.Windows.Forms.Button updateBtn;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.TextBox projectIDTxt;
        private System.Windows.Forms.TextBox projectOpenDateTxt;
        private System.Windows.Forms.Label charRemainingDescLbl;
        private System.Windows.Forms.Label uniqueProjectLbl;
        private System.Windows.Forms.DataGridView dataGridUniqueProject;
        private System.Windows.Forms.Label blankLbl;
        private System.Windows.Forms.Label charRemainingNameLbl;
        private System.Windows.Forms.Label backBtn;
        private System.Windows.Forms.Label projectNameAsteriskLbl;
    }
}