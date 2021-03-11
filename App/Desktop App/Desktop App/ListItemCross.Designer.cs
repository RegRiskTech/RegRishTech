namespace Desktop_App
{
    partial class ListItemCross
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListItemCross));
            this.divider = new System.Windows.Forms.Panel();
            this.nameLbl = new System.Windows.Forms.Label();
            this.crossImg = new System.Windows.Forms.PictureBox();
            this.emailLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.crossImg)).BeginInit();
            this.SuspendLayout();
            // 
            // divider
            // 
            this.divider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.divider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.divider.Location = new System.Drawing.Point(6, 36);
            this.divider.Name = "divider";
            this.divider.Size = new System.Drawing.Size(480, 1);
            this.divider.TabIndex = 8;
            // 
            // nameLbl
            // 
            this.nameLbl.AutoEllipsis = true;
            this.nameLbl.Font = new System.Drawing.Font("Open Sans", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLbl.Location = new System.Drawing.Point(3, -3);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(222, 33);
            this.nameLbl.TabIndex = 6;
            this.nameLbl.Text = "Name";
            // 
            // crossImg
            // 
            this.crossImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.crossImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.crossImg.Image = ((System.Drawing.Image)(resources.GetObject("crossImg.Image")));
            this.crossImg.Location = new System.Drawing.Point(243, 3);
            this.crossImg.Name = "crossImg";
            this.crossImg.Size = new System.Drawing.Size(24, 24);
            this.crossImg.TabIndex = 10;
            this.crossImg.TabStop = false;
            this.crossImg.Click += new System.EventHandler(this.crossImg_Click);
            this.crossImg.MouseHover += new System.EventHandler(this.crossImg_MouseHover);
            // 
            // emailLbl
            // 
            this.emailLbl.AutoEllipsis = true;
            this.emailLbl.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.emailLbl.Location = new System.Drawing.Point(4, 16);
            this.emailLbl.Name = "emailLbl";
            this.emailLbl.Size = new System.Drawing.Size(221, 28);
            this.emailLbl.TabIndex = 7;
            this.emailLbl.Text = "email@address.com";
            // 
            // ListItemCross
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.crossImg);
            this.Controls.Add(this.divider);
            this.Controls.Add(this.emailLbl);
            this.Controls.Add(this.nameLbl);
            this.Name = "ListItemCross";
            this.Size = new System.Drawing.Size(275, 39);
            ((System.ComponentModel.ISupportInitialize)(this.crossImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox crossImg;
        private System.Windows.Forms.Panel divider;
        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.Label emailLbl;
    }
}
