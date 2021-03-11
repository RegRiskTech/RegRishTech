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

namespace Desktop_App
{
    public partial class testForm : Form
    {
        public testForm()
        {
            InitializeComponent();
        }

        private void SQL_Data(string sqlquery, DataGridView dataGridTable)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["projectCONN"].ConnectionString;
            SqlConnection sqlconnn = new SqlConnection(mainconn);
            sqlconnn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconnn);
            SqlDataAdapter sdr = new SqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            sdr.Fill(dt);
            dataGridTable.DataSource = dt;
            sqlconnn.Close();
        }

        private void testForm_Load(object sender, EventArgs e)
        {
            string sqlquery = "SELECT Member, Email FROM [dbo].[ProjectMembers] WHERE ProjectID = 'Alpha' ORDER BY Member ASC";
            SQL_Data(sqlquery, dataGridViewTest);

            // Flow Panel Code
            populateList(dataGridViewTest);
        }

        private void populateList(DataGridView dataTable)
        {
            //panelEmail.Controls.Clear();
            ListItem[] listItems = new ListItem[dataTable.Rows.Count - 1];

            // loop through each item
            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new ListItem();
                listItems[i].Title = dataTable.Rows[i].Cells[0].Value.ToString();
                listItems[i].Email = dataTable.Rows[i].Cells[1].Value.ToString();

                //flowLayoutPanelTest.Controls.Add(listItems[i]);

                panelEmail.Controls.Add(listItems[i]);
            }

        }

        /*
        Pen red = new Pen(Color.Red);
        Pen green = new Pen(Color.Green);

        Rectangle rect = new Rectangle(200, 200, 220, 90);
        Rectangle circl = new Rectangle(200, 200, 220, 90);

        System.Drawing.SolidBrush fillRed = new System.Drawing.SolidBrush(Color.Red);
        */

        private void testForm_Paint(object sender, PaintEventArgs e)
        {
            /*
            Graphics g = e.Graphics;

            g.DrawRectangle(red, rect);
            g.DrawEllipse(green, circl);
            */
        }

        private void testForm_Resize(object sender, EventArgs e)
        {
            //panelEmail.Width = this.Width / 2 - 69;
        }
    }
}
