using System;
using System.Activities;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace WorkFlowSample
{
    public partial class MainForm : Form
    {
        //IDictionary<string,object> input = new Dictionary<string, object> { { "Formats", new[] { "Arvin1", "Arvin2", "Arvin3" } } };
        IList<IDictionary<string,object>> inputs = new List<IDictionary<string, object>>();

        
        private IList<WorkflowApplication> applications = new List<WorkflowApplication>();
            public MainForm()
        {
            InitializeComponent();
                inputs.Add(new Dictionary<string, object> {{"Formats", new[] {"Arvin 1"}}});
                inputs.Add(new Dictionary<string, object> { { "Formats", new[] { "Arvin 2" } } });
                foreach (var input in inputs)
                {
                    var transcodeWorkflow = new Transcode();
                    var application = new WorkflowApplication(transcodeWorkflow, input);
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
                    applications.Add(application);
                }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            foreach (var application in applications)
            {
                //application.SynchronizationContext = new SyncContext();
                application.Run();
            }
            buttonEnd.Enabled = true;
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                string[] bookmarks = (string[]) inputs[i]["Formats"];
                foreach (var bookmark in bookmarks)
                {
                    applications[i].ResumeBookmark(bookmark, true);
                }
            }
        }

    }

    public class SyncContext : SynchronizationContext
    {
        public override void Post(SendOrPostCallback d, object state)
        {
            d(state);
        }
    }
}

