using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Activities.XamlIntegration;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xaml;

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

        }

        private void WriteLine(string messageFormat, params object[] parameters)
        {
            var formattedMessage = string.Format("{0:yyyy-MM-dd hh:mm:ss.fff}-[{1:0000}] : {2}", DateTime.Now,
                Thread.CurrentThread.ManagedThreadId, string.Format(messageFormat, parameters)) + Environment.NewLine;
            if (textBoxResult.InvokeRequired)
            {
                textBoxResult.Invoke((MethodInvoker)(() => { textBoxResult.Text += formattedMessage; }));
            }
            else
            {
                textBoxResult.Text += formattedMessage;
            }
        }


        private WorkflowApplication CreateWorkflowApplication(string xaml, SqlWorkflowInstanceStore store, IDictionary<string,object> input = null)
        {
            Activity transcodeWorkflow;
            if (!string.IsNullOrEmpty(xaml))
                transcodeWorkflow= CreateActivityFrom(xaml);
            else
            {
                transcodeWorkflow = new Transcode();
            }
            var application = input == null
                ? new WorkflowApplication(transcodeWorkflow)
                : new WorkflowApplication(transcodeWorkflow, input);
            application.InstanceStore = store;
            application.Completed = WorkflowCompleted;
            application.PersistableIdle = OnPersistableIdle;
            application.Unloaded = OnWorkflowUnloaded;
            return application;
        }

        private static Activity CreateActivityFrom(string xaml)
        {
            var sr = new StringReader(xaml);

            //Change LocalAssembly to where the Activities reside
            var xamlSettings = new XamlXmlReaderSettings { LocalAssembly = Assembly.GetExecutingAssembly() };

            var xamlReader = ActivityXamlServices
                .CreateReader(new XamlXmlReader(sr, xamlSettings));

            var result = XamlServices.Load(xamlReader);

            var activity = result as Activity;

            return activity;
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
            WriteLine("Workflow '{0}' unloaded.", obj.InstanceId);
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
            WriteLine("Workflow '{0}' completed.", args.InstanceId);
            if (runningWorkflows.ContainsKey(args.InstanceId))
            {
                WorkflowApplication workflowApp;
                runningWorkflows.TryRemove(args.InstanceId, out workflowApp);
            }

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            SqlWorkflowInstanceStore store = CreateStore();

            inputs.Add(new Dictionary<string, object> { { "Formats", new[] { "Arvin 1" } } });
            //inputs.Add(new Dictionary<string, object> { { "Formats", new[] { "Arvin 2" } } });
            foreach (var input in inputs)
            {
                var stringFormats = input["Formats"] as string[];

                var application = CreateWorkflowApplication(textBoxXaml.Text, store, input);
                WriteLine("Created workflows with id {0} having a parameters of {1}", application.Id,
                    string.Join(",", stringFormats));
                runningWorkflows.TryAdd(application.Id, application);
            }

            foreach (var application in runningWorkflows.Values)
            {
                //application.SynchronizationContext = new SyncContext();
                WriteLine("Running Workflow {0}", application.Id);
                application.Run();
            }
            buttonEnd.Enabled = true;
        }

        private void buttonResumeTranscode_Click(object sender, EventArgs e)
        {

            SqlWorkflowInstanceStore store = CreateStore();
            for (int i = unloadedWorkflows.Count - 1; i >= 0; i--)
            {
                var app = unloadedWorkflows[i];
                unloadedWorkflows.RemoveAt(i);

                var resumedApp = CreateWorkflowApplication(textBoxXaml.Text, store);
                WriteLine("Resuming workflow from DB with ID '{0}'", app.Id);
                resumedApp.Load(app.Id);

                var bookmarks = inputs[i]["Formats"] as string[];
                if (bookmarks != null)
                {
                    foreach (var bookmark in bookmarks)
                    {
                        WriteLine("Resuming bookmark '{0}'", bookmark);
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

        private void buttonResumeFromDB_Click(object sender, EventArgs e)
        {
            SqlWorkflowInstanceStore store = CreateStore();
            var application = CreateWorkflowApplication(textBoxXaml.Text, store);
            application.Load(new Guid(textWorkflowId.Text));
            if (!string.IsNullOrEmpty(textBoxBookmark.Text))
            {
                application.ResumeBookmark(textBoxBookmark.Text, true);
            }
            application.Run();

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

