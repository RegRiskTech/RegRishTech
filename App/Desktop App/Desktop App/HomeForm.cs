using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Collections.Generic;
using System.Security;

namespace Desktop_App
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new ProjectAdd(""));
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            //eventLog();

            //getEventLogNames();

            //ReadEventData();

        }

        public static void ReadEventData()
        {
            //EVENT LOG READER
            string source = @"%SystemRoot%\System32\Winevt\Logs\Classifier.evtx";

            using (var reader = new EventLogReader(source, PathType.FilePath))
            {
                EventRecord record;
                while ((record = reader.ReadEvent()) != null)
                {
                    using (record)
                    {
                        Console.WriteLine("{0} {1}: {2}", record.TimeCreated, record.LevelDisplayName, record.FormatDescription());
                    }
                }
            }

            //EVENT LOG
            EventLog eventLog = new EventLog();
            eventLog.Source = "Classifier Addin - Outlook"; //name of an application

            foreach (EventLogEntry log in eventLog.Entries)
            {
                Console.WriteLine("{0}\n", log.Message);
            }
        }


        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            Program.homescreen.panelHomeForm.Controls.Add(childForm);
            Program.homescreen.panelHomeForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new userAdd(""));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openChildForm(new testForm());
        }

        private void powerShellBtn_Click(object sender, EventArgs e)
        {
            //RunScript(LoadScript(@"c:\Users\jbaldwin\Desktop\newtxtFile.ps1"));
        }

        private string RunScript(string scriptText)
        {
            // create Powershell runspace
            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it
            runspace.Open();

            // create a pipeline and feed it the script text
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(scriptText);

            // add an extra command to transform the script output objects into nicely formatted strings
            // remove this line to get the actual objects that the script returns. For example, the script
            // "Get-Process" returns a collection of System.Diagnostics.Process instances.
            pipeline.Commands.Add("Out-String");

            // execute the script
            Collection<PSObject> results = pipeline.Invoke();

            // close the runspace
            runspace.Close();

            // convert the script result into a single string
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }

            // return the results of the script that has
            // now been converted to text
            return stringBuilder.ToString();
        }

        private string LoadScript(string filename)
        {
            try
            {
                // Create an instance of StreamReader to read from our file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(filename))
                {

                    // use a string builder to get all our lines from the file
                    StringBuilder fileContents = new StringBuilder();

                    // string to hold the current line
                    string curLine;

                    // loop through our file and read each line into our
                    // stringbuilder as we go along
                    while ((curLine = sr.ReadLine()) != null)
                    {
                        // read each line and MAKE SURE YOU ADD BACK THE
                        // LINEFEED THAT IT THE ReadLine() METHOD STRIPS OFF
                        fileContents.Append(curLine + "\n");
                    }

                    // call RunScript and pass in our file contents
                    // converted to a string
                    return fileContents.ToString();
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                string errorText = "The file could not be read:";
                errorText += e.Message + "\n";
                return errorText;
            }

        }
    }
    
    public class CustomComboBox : ComboBox
    {
        private const int WM_PAINT = 0xF;
        private int buttonWidth = SystemInformation.HorizontalScrollBarArrowWidth;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                using (var g = Graphics.FromHwnd(Handle))
                {
                    // Uncomment this if you don't want the "highlight border".

                    using (var p = new Pen(this.BorderColor, 1))
                    {
                        g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
                    }
                    using (var p = new Pen(this.BorderColor, 2))
                    {
                        g.DrawRectangle(p, 0, 0, Width - buttonWidth, Height);
                    }
                }
            }
        }

        public CustomComboBox()
        {
            BorderColor = Color.FromArgb(255, 150, 150, 150);
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Grey")]
        public Color BorderColor { get; set; }
    }
}
