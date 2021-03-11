namespace Desktop_App
{
    partial class testForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanelTest = new System.Windows.Forms.FlowLayoutPanel();
            this.panelEmail = new System.Windows.Forms.Panel();
            this.dataGridViewTest = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTest)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Test Form Not for Production";
            // 
            // flowLayoutPanelTest
            // 
            this.flowLayoutPanelTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.flowLayoutPanelTest.AutoScroll = true;
            this.flowLayoutPanelTest.Location = new System.Drawing.Point(539, 37);
            this.flowLayoutPanelTest.Name = "flowLayoutPanelTest";
            this.flowLayoutPanelTest.Size = new System.Drawing.Size(258, 361);
            this.flowLayoutPanelTest.TabIndex = 1;
            this.flowLayoutPanelTest.Visible = false;
            // 
            // panelEmail
            // 
            this.panelEmail.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelEmail.AutoScroll = true;
            this.panelEmail.Location = new System.Drawing.Point(211, 37);
            this.panelEmail.Name = "panelEmail";
            this.panelEmail.Size = new System.Drawing.Size(293, 361);
            this.panelEmail.TabIndex = 0;
            // 
            // dataGridViewTest
            // 
            this.dataGridViewTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTest.Location = new System.Drawing.Point(280, 150);
            this.dataGridViewTest.Name = "dataGridViewTest";
            this.dataGridViewTest.RowHeadersWidth = 51;
            this.dataGridViewTest.Size = new System.Drawing.Size(240, 150);
            this.dataGridViewTest.TabIndex = 3;
            // 
            // testForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelEmail);
            this.Controls.Add(this.flowLayoutPanelTest);
            this.Controls.Add(this.dataGridViewTest);
            this.Controls.Add(this.label1);
            this.Name = "testForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.testForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.testForm_Paint);
            this.Resize += new System.EventHandler(this.testForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTest;
        private System.Windows.Forms.Panel panelEmail;
        private System.Windows.Forms.DataGridView dataGridViewTest;
    }
}