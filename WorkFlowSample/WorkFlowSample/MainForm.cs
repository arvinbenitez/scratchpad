using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Threading;
using System.Windows.Forms;

namespace WorkFlowSample
{
    public partial class MainForm : Form
    {
        //IDictionary<string,object> input = new Dictionary<string, object> { { "Formats", new[] { "Arvin1", "Arvin2", "Arvin3" } } };
        IList<IDictionary<string, object>> inputs = new List<IDictionary<string, object>>();

        private ConcurrentDictionary<Guid, WorkflowApplication> runningWorkflows = new ConcurrentDictionary<Guid, WorkflowApplication>();
        private IList<WorkflowApplication> unloadedWorkflows = new List<WorkflowApplication>();
        public MainForm()
        {
            InitializeComponent();

            SqlWorkflowInstanceStore store = CreateStore();

            inputs.Add(new Dictionary<string, object> { { "Formats", new[] { "Arvin 1" } } });
            //inputs.Add(new Dictionary<string, object> { { "Formats", new[] { "Arvin 2" } } });
            foreach (var input in inputs)
            {
                var application = CreateWorkflowApplication(input, store);

                runningWorkflows.TryAdd(application.Id, application);
            }
        }

        private WorkflowApplication CreateWorkflowApplication(IDictionary<string, object> input, SqlWorkflowInstanceStore store)
        {
            var transcodeWorkflow = new Transcode();
            var application = input == null ? new WorkflowApplication(transcodeWorkflow) :
                new WorkflowApplication(transcodeWorkflow, input);
            application.InstanceStore = store;
            application.Completed = WorkflowCompleted;
            application.PersistableIdle = OnPersistableIdle;
            application.Unloaded = OnWorkflowUnloaded;
            return application;
        }

        private static SqlWorkflowInstanceStore CreateStore()
        {
            return new SqlWorkflowInstanceStore(@"data source=ARVIN-DELL-PC\SQLEXPRESS;initial catalog=WorkflowDB;persist security info=True;user id=sa;password=password;");
        }

        private PersistableIdleAction OnPersistableIdle(WorkflowApplicationIdleEventArgs arg)
        {
            return PersistableIdleAction.Unload;
        }

        private void OnWorkflowUnloaded(WorkflowApplicationEventArgs obj)
        {
            Debug.WriteLine("Workflow '{0}' unloaded.", obj.InstanceId);
            if (runningWorkflows.ContainsKey(obj.InstanceId))
            {
                WorkflowApplication workflowApp;
                runningWorkflows.TryRemove(obj.InstanceId, out workflowApp);
                if (!unloadedWorkflows.Contains(workflowApp))
                {
                    unloadedWorkflows.Add(workflowApp);
                }
            }
        }

        private void WorkflowCompleted(WorkflowApplicationCompletedEventArgs args)
        {
            Debug.WriteLine("Workflow '{0}' completed.", args.InstanceId);
            if (runningWorkflows.ContainsKey(args.InstanceId))
            {
                WorkflowApplication workflowApp;
                runningWorkflows.TryRemove(args.InstanceId, out workflowApp);
            }

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            foreach (var application in runningWorkflows.Values)
            {
                //application.SynchronizationContext = new SyncContext();
                application.Run();
            }
            buttonEnd.Enabled = true;
        }

        private void buttonResumeFromDB_Click(object sender, EventArgs e)
        {

            SqlWorkflowInstanceStore store = CreateStore();
            for (int i = unloadedWorkflows.Count - 1; i >= 0; i--)
            {
                var app = unloadedWorkflows[i];
                unloadedWorkflows.RemoveAt(i);

                var resumedApp = CreateWorkflowApplication(null, store);
                Debug.WriteLine("Resuming workflow from DB with ID '{0}'", app.Id);
                resumedApp.Load(app.Id);

                var bookmarks = inputs[i]["Formats"] as string[];
                if (bookmarks != null)
                {
                    foreach (var bookmark in bookmarks)
                    {
                        Debug.WriteLine("Resuming bookmark '{0}'", bookmark);
                        resumedApp.ResumeBookmark(bookmark, true);
                    }
                }
            }
        }

        private void buttonPersist_Click(object sender, EventArgs e)
        {
            //foreach (var application in applications)
            //{
            //    Debug.WriteLine("Persisting Workflow '{0}'", application.Id);
            //    application.Persist();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

