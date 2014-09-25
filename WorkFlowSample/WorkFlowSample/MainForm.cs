using System;
using System.Activities;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WorkFlowSample
{
    public partial class MainForm : Form
    {
        //IDictionary<string,object> input = new Dictionary<string, object> { { "Formats", new[] { "Arvin1", "Arvin2", "Arvin3" } } };
        IDictionary<string, object> input = new Dictionary<string, object> { { "Formats", new[] { "Arvin1" } } }; 
        private WorkflowApplication application;
        private Transcode transcodeWorkflow;
            public MainForm()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            transcodeWorkflow = new Transcode();
            application = new WorkflowApplication(transcodeWorkflow,input);
            application.Completed = delegate(WorkflowApplicationCompletedEventArgs args)
            {
                if (textResult.InvokeRequired)
                {
                    textResult.Invoke((MethodInvoker)(() => { textResult.Text = "Workflow Completed"; }));
                }
                else
                {
                    textResult.Text = "Workflow Completed";
                }
            };
            application.Run();
            buttonEnd.Enabled = true;
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            //resume the workflows
            string[] bookmarks = (string[]) input["Formats"];
            foreach (var bookmark in bookmarks)
            {
                application.ResumeBookmark(bookmark, true);
            }
        }

    }
}

