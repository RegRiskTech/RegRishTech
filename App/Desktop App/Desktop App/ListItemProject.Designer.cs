namespace Desktop_App
{
    partial class ListItemProject
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListItemProject));
            this.divider = new System.Windows.Forms.Panel();
            this.descLbl = new System.Windows.Forms.Label();
            this.projectLbl = new System.Windows.Forms.Label();
            this.statusImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.statusImg)).BeginInit();
            this.SuspendLayout();
            // 
            // divider
            // 
            this.divider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.divider.Location = new System.Drawing.Point(6, 24);
            this.divider.Name = "divider";
            this.divider.Size = new System.Drawing.Size(480, 1);
            this.divider.TabIndex = 12;
            // 
            // descLbl
            // 
            this.descLbl.Font = new System.Drawing.Font("Open Sans", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(149)))));
            this.descLbl.Location = new System.Drawing.Point(181, 6);
            this.descLbl.Name = "descLbl";
            this.descLbl.Size = new System.Drawing.Size(262, 28);
            this.descLbl.TabIndex = 11;
            this.descLbl.Text = "Description";
            // 
            // projectLbl
            // 
            this.projectLbl.Font = new System.Drawing.Font("Open Sans", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectLbl.Location = new System.Drawing.Point(30, 1);
            this.projectLbl.Name = "projectLbl";
            this.projectLbl.Size = new System.Drawing.Size(313, 33);
            this.projectLbl.TabIndex = 10;
            this.projectLbl.Text = "Project";
            // 
            // statusImg
            // 
            this.statusImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.statusImg.Image = ((System.Drawing.Image)(resources.GetObject("statusImg.Image")));
            this.statusImg.Location = new System.Drawing.Point(12, 6);
            this.statusImg.Name = "statusImg";
            this.statusImg.Size = new System.Drawing.Size(12, 12);
            this.statusImg.TabIndex = 13;
            this.statusImg.TabStop = false;
            // 
            // ListItemProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusImg);
            this.Controls.Add(this.divider);
            this.Controls.Add(this.descLbl);
            this.Controls.Add(this.projectLbl);
            this.Name = "ListItemProject";
            this.Size = new System.Drawing.Size(699, 34);
            this.Load += new System.EventHandler(this.ListItemProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statusImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox statusImg;
        private System.Windows.Forms.Panel divider;
        private System.Windows.Forms.Label descLbl;
        private System.Windows.Forms.Label projectLbl;
    }
}
