using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Activities.XamlIntegration;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.DurableInstancing;
using System.Xaml;

namespace WorkFlowSample
{

    /// <summary>
    /// From Will Beattie's blog
    /// http://blog.willbeattie.net/2011/02/managing-change-in-long-running.html
    /// </summary>
    public static class GenericWorkflowHost
    {
        private static ConcurrentDictionary<Guid, WorkflowApplication> runningWorkflows;

        #region Private Helper Methods

        private static Activity CreateActivityFrom(string xaml)
        {
            var sr = new StringReader(xaml);

            //Change LocalAssembly to where the Activities reside
            var xamlSettings = new XamlXmlReaderSettings {LocalAssembly = Assembly.GetExecutingAssembly()};

            var xamlReader = ActivityXamlServices
                .CreateReader(new XamlXmlReader(sr, xamlSettings));

            var result = XamlServices.Load(xamlReader);

            var activity = result as Activity;

            return activity;
        }

        private static InstanceStore CreateInstanceStore()
        {
            var conn =
                @"data source=ARVIN-DELL-PC\SQLEXPRESS;initial catalog=WorkflowDB;persist security info=True;user id=sa;password=password;";

            var store = new SqlWorkflowInstanceStore(conn)
            {
                InstanceLockedExceptionAction = InstanceLockedExceptionAction.AggressiveRetry,
                InstanceCompletionAction = InstanceCompletionAction.DeleteNothing,
                HostLockRenewalPeriod = TimeSpan.FromSeconds(20),
                RunnableInstancesDetectionPeriod = TimeSpan.FromSeconds(3)
            };

            var handle = store.CreateInstanceHandle();

            var view = store.Execute(handle, new CreateWorkflowOwnerCommand(),
                TimeSpan.FromSeconds(60));

            store.DefaultInstanceOwner = view.InstanceOwner;

            handle.Free();

            return store;
        }


        #endregion

        public static void InvokeInstance(object input, string xaml, Guid instanceId)
        {
            var inputs = new Dictionary<string, object>();

            if (input != null)
            {
                inputs.Add(input.GetType().Name, input);
            }

            var wf = CreateActivityFrom(xaml);

            var activity = wf;

            WorkflowInvoker.Invoke(activity, inputs);
        }

        public static Guid StartPersistableInstance(IDictionary<string, object> inputs, string xaml)
        {
            if (runningWorkflows == null)
            {
                runningWorkflows = new ConcurrentDictionary<Guid, WorkflowApplication>();
            }


            var activity = CreateActivityFrom(xaml);

            var workflowApp = new WorkflowApplication(activity, inputs)
            {
                InstanceStore = CreateInstanceStore(),
                PersistableIdle = OnIdleAndPersistable,
                Completed = OnWorkflowCompleted,
                Aborted = OnWorkflowAborted,
                Unloaded = OnWorkflowUnloaded,
                OnUnhandledException = OnWorkflowException
            };

            workflowApp.Persist();

            var instanceId = workflowApp.Id;

            workflowApp.Run();

            runningWorkflows.TryAdd(instanceId, workflowApp);

            return workflowApp.Id;
        }

        public static bool LoadInstanceWithBookmark(string bookmarkName,
            Guid instanceId,
            object input,
            string xaml)
        {
            if (runningWorkflows == null)
            {
                runningWorkflows = new ConcurrentDictionary<Guid, WorkflowApplication>();
            }

            BookmarkResumptionResult result;

            if (runningWorkflows.ContainsKey(instanceId))
            {
                var workflow = runningWorkflows[instanceId];
                workflow.Completed = OnWorkflowCompleted;
                workflow.PersistableIdle = OnIdleAndPersistable;

                result = workflow.ResumeBookmark(bookmarkName, input, TimeSpan.FromSeconds(60));

                Console.WriteLine(instanceId + " resumed @ " + bookmarkName);
            }
            else
            {
                // Setup the persistance
                var store = CreateInstanceStore();

                var activity = CreateActivityFrom(xaml);

                var application = new WorkflowApplication(activity)
                {
                    InstanceStore = store,
                    Completed = OnWorkflowCompleted,
                    Unloaded = OnWorkflowUnloaded,
                    PersistableIdle = OnIdleAndPersistable,
                };

                application.Load(instanceId, TimeSpan.FromSeconds(60));

                result = application.ResumeBookmark(bookmarkName, input, TimeSpan.FromSeconds(60));

                runningWorkflows.TryAdd(instanceId, application);

                Console.WriteLine(instanceId + " resumed @ " + bookmarkName);
            }

            return result == BookmarkResumptionResult.Success;
        }

        public static void UnloadInstance(Guid instanceId)
        {
            if (!runningWorkflows.ContainsKey(instanceId))
            {
                return;
            }

            var workflow = runningWorkflows[instanceId];
            workflow.Unload();

            runningWorkflows.TryRemove(instanceId, out workflow);
        }

        #region Events

        public static void OnWorkflowCompleted(WorkflowApplicationCompletedEventArgs e)
        {
            if (runningWorkflows != null && runningWorkflows.ContainsKey(e.InstanceId))
            {
                WorkflowApplication workflowApp;
                runningWorkflows.TryRemove(e.InstanceId, out workflowApp);
            }

            Console.WriteLine(e.CompletionState);
        }

        public static PersistableIdleAction OnIdleAndPersistable(WorkflowApplicationIdleEventArgs e)
        {
            return PersistableIdleAction.Unload;
        }

        public static void OnWorkflowAborted(WorkflowApplicationAbortedEventArgs e)
        {
            Console.WriteLine(e.Reason);
        }

        public static void OnWorkflowUnloaded(WorkflowApplicationEventArgs e)
        {
            if (runningWorkflows != null && runningWorkflows.ContainsKey(e.InstanceId))
            {
                WorkflowApplication workflowApp;
                runningWorkflows.TryRemove(e.InstanceId, out workflowApp);
            }

            Console.WriteLine(e.InstanceId + " unloaded");
        }

        public static UnhandledExceptionAction OnWorkflowException(WorkflowApplicationUnhandledExceptionEventArgs e)
        {
            //log the exception here using e.UnhandledException 
            return UnhandledExceptionAction.Terminate;
        }

        #endregion
    }
}