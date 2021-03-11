namespace Desktop_App
{
    partial class Reporting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reporting));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.clearAllBtn = new System.Windows.Forms.Label();
            this.exportButton = new System.Windows.Forms.Label();
            this.reportingSearchPanel = new System.Windows.Forms.Panel();
            this.searchImg = new System.Windows.Forms.PictureBox();
            this.reportingSearchTxt = new System.Windows.Forms.TextBox();
            this.ReportingLbl = new System.Windows.Forms.Label();
            this.dataGridReporting = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.reportingSearchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReporting)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Control;
            this.panelTop.Controls.Add(this.clearAllBtn);
            this.panelTop.Controls.Add(this.exportButton);
            this.panelTop.Controls.Add(this.reportingSearchPanel);
            this.panelTop.Controls.Add(this.ReportingLbl);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(946, 109);
            this.panelTop.TabIndex = 1;
            // 
            // clearAllBtn
            // 
            this.clearAllBtn.AutoSize = true;
            this.clearAllBtn.BackColor = System.Drawing.Color.Transparent;
            this.clearAllBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clearAllBtn.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearAllBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.clearAllBtn.Image = ((System.Drawing.Image)(resources.GetObject("clearAllBtn.Image")));
            this.clearAllBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.clearAllBtn.Location = new System.Drawing.Point(516, 59);
            this.clearAllBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearAllBtn.Name = "clearAllBtn";
            this.clearAllBtn.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.clearAllBtn.Size = new System.Drawing.Size(114, 23);
            this.clearAllBtn.TabIndex = 14;
            this.clearAllBtn.Text = "Clear Filters      ";
            this.clearAllBtn.Click += new System.EventHandler(this.clearAllBtn_Click);
            // 
            // exportButton
            // 
            this.exportButton.AutoSize = true;
            this.exportButton.BackColor = System.Drawing.Color.Transparent;
            this.exportButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exportButton.Font = new System.Drawing.Font("Open Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.exportButton.Location = new System.Drawing.Point(35, 56);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(107, 19);
            this.exportButton.TabIndex = 13;
            this.exportButton.Text = "Export to Excel";
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // reportingSearchPanel
            // 
            this.reportingSearchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reportingSearchPanel.BackColor = System.Drawing.Color.White;
            this.reportingSearchPanel.Controls.Add(this.searchImg);
            this.reportingSearchPanel.Controls.Add(this.reportingSearchTxt);
            this.reportingSearchPanel.Font = new System.Drawing.Font("Open Sans SemiBold", 7.2F, System.Drawing.FontStyle.Bold);
            this.reportingSearchPanel.Location = new System.Drawing.Point(710, 29);
            this.reportingSearchPanel.Name = "reportingSearchPanel";
            this.reportingSearchPanel.Padding = new System.Windows.Forms.Padding(26, 4, 10, 3);
            this.reportingSearchPanel.Size = new System.Drawing.Size(206, 25);
            this.reportingSearchPanel.TabIndex = 8;
            this.reportingSearchPanel.Visible = false;
            this.reportingSearchPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.reportingSearchPanel_MouseClick);
            // 
            // searchImg
            // 
            this.searchImg.Image = ((System.Drawing.Image)(resources.GetObject("searchImg.Image")));
            this.searchImg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.searchImg.Location = new System.Drawing.Point(7, 5);
            this.searchImg.Name = "searchImg";
            this.searchImg.Size = new System.Drawing.Size(16, 16);
            this.searchImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.searchImg.TabIndex = 8;
            this.searchImg.TabStop = false;
            // 
            // reportingSearchTxt
            // 
            this.reportingSearchTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.reportingSearchTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportingSearchTxt.Font = new System.Drawing.Font("Open Sans", 11.8F);
            this.reportingSearchTxt.Location = new System.Drawing.Point(26, 4);
            this.reportingSearchTxt.Margin = new System.Windows.Forms.Padding(50, 3, 3, 3);
            this.reportingSearchTxt.Multiline = true;
            this.reportingSearchTxt.Name = "reportingSearchTxt";
            this.reportingSearchTxt.Size = new System.Drawing.Size(170, 18);
            this.reportingSearchTxt.TabIndex = 7;
            this.reportingSearchTxt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.reportingSearchTxt_MouseClick);
            this.reportingSearchTxt.TextChanged += new System.EventHandler(this.reportingSearchTxt_TextChanged);
            // 
            // ReportingLbl
            // 
            this.ReportingLbl.AutoSize = true;
            this.ReportingLbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReportingLbl.Font = new System.Drawing.Font("Open Sans", 24F);
            this.ReportingLbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ReportingLbl.Location = new System.Drawing.Point(31, 22);
            this.ReportingLbl.Name = "ReportingLbl";
            this.ReportingLbl.Size = new System.Drawing.Size(166, 43);
            this.ReportingLbl.TabIndex = 6;
            this.ReportingLbl.Text = "Reporting";
            // 
            // dataGridReporting
            // 
            this.dataGridReporting.AllowUserToAddRows = false;
            this.dataGridReporting.AllowUserToDeleteRows = false;
            this.dataGridReporting.AllowUserToResizeColumns = false;
            this.dataGridReporting.AllowUserToResizeRows = false;
            this.dataGridReporting.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridReporting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridReporting.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridReporting.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Open Sans SemiBold", 13.8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridReporting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridReporting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridReporting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridReporting.EnableHeadersVisualStyles = false;
            this.dataGridReporting.Location = new System.Drawing.Point(30, 0);
            this.dataGridReporting.MultiSelect = false;
            this.dataGridReporting.Name = "dataGridReporting";
            this.dataGridReporting.ReadOnly = true;
            this.dataGridReporting.RowHeadersVisible = false;
            this.dataGridReporting.RowHeadersWidth = 51;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Broadway", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Control;
            this.dataGridReporting.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridReporting.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dataGridReporting.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Open Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridReporting.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridReporting.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.dataGridReporting.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.dataGridReporting.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridReporting.RowTemplate.Height = 50;
            this.dataGridReporting.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridReporting.Size = new System.Drawing.Size(916, 576);
            this.dataGridReporting.TabIndex = 10;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.dataGridReporting);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Font = new System.Drawing.Font("Open Sans SemiBold", 13.8F, System.Drawing.FontStyle.Bold);
            this.panelBottom.Location = new System.Drawing.Point(0, 109);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.panelBottom.Size = new System.Drawing.Size(946, 576);
            this.panelBottom.TabIndex = 11;
            // 
            // Reporting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 685);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "Reporting";
            this.Text = "Reporting";
            this.Load += new System.EventHandler(this.Reporting_Load);
            this.Resize += new System.EventHandler(this.Reporting_Resize);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.reportingSearchPanel.ResumeLayout(false);
            this.reportingSearchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReporting)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel reportingSearchPanel;
        private System.Windows.Forms.PictureBox searchImg;
        private System.Windows.Forms.TextBox reportingSearchTxt;
        private System.Windows.Forms.Label ReportingLbl;
        private System.Windows.Forms.DataGridView dataGridReporting;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label exportButton;
        private System.Windows.Forms.Label clearAllBtn;
    }
}