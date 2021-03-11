namespace Desktop_App
{
    partial class ListItemTick
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListItemTick));
            this.nameLbl = new System.Windows.Forms.Label();
            this.emailLbl = new System.Windows.Forms.Label();
            this.divider = new System.Windows.Forms.Panel();
            this.tickImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tickImg)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLbl
            // 
            this.nameLbl.AutoEllipsis = true;
            this.nameLbl.Font = new System.Drawing.Font("Open Sans", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLbl.Location = new System.Drawing.Point(3, -3);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(213, 33);
            this.nameLbl.TabIndex = 6;
            this.nameLbl.Text = "Name";
            // 
            // emailLbl
            // 
            this.emailLbl.AutoEllipsis = true;
            this.emailLbl.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(187)))));
            this.emailLbl.Location = new System.Drawing.Point(4, 16);
            this.emailLbl.Name = "emailLbl";
            this.emailLbl.Size = new System.Drawing.Size(212, 28);
            this.emailLbl.TabIndex = 7;
            this.emailLbl.Text = "email@address.com";
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
            // tickImg
            // 
            this.tickImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tickImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tickImg.Image = ((System.Drawing.Image)(resources.GetObject("tickImg.Image")));
            this.tickImg.Location = new System.Drawing.Point(234, 4);
            this.tickImg.Name = "tickImg";
            this.tickImg.Size = new System.Drawing.Size(20, 20);
            this.tickImg.TabIndex = 9;
            this.tickImg.TabStop = false;
            this.tickImg.Click += new System.EventHandler(this.tickImg_Click);
            this.tickImg.MouseHover += new System.EventHandler(this.tickImg_MouseHover);
            // 
            // ListItemTick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tickImg);
            this.Controls.Add(this.divider);
            this.Controls.Add(this.emailLbl);
            this.Controls.Add(this.nameLbl);
            this.Name = "ListItemTick";
            this.Size = new System.Drawing.Size(260, 39);
            ((System.ComponentModel.ISupportInitialize)(this.tickImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.Label emailLbl;
        private System.Windows.Forms.Panel divider;
        private System.Windows.Forms.PictureBox tickImg;
    }
}
