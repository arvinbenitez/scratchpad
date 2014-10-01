using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowSample
{
    public class BeginTranscodeV3: NativeActivity<bool>
    {
        private static int instanceCounter = 0;
        private static object counterLock = new object();
        public static int GetCounter()
        {
            lock (counterLock)
            {
                instanceCounter += 1;
            }
            return instanceCounter;
        }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        [RequiredArgument]
        public InArgument<string> Format { get; set; }


        protected override void Execute(NativeActivityContext context)
        {
            string format = context.GetValue(Format);
            Debug.WriteLine("{0:MM-dd-yyyy hh:mm:ss.fff} - Executing BeginTranscodeV3 - " + format, DateTime.Now);
            System.Threading.Thread.Sleep(5000);
            Debug.WriteLine("{0:MM-dd-yyyy hh:mm:ss.fff} - Ending BeginTranscodeV3 - " + format, DateTime.Now);
            Debug.WriteLine("Waiting for resume");
            context.CreateBookmark(format, OnResumeCallBack);
        }

        private void OnResumeCallBack(NativeActivityContext context, Bookmark bookmark, object value)
        {
            string format = context.GetValue(Format);
            Debug.WriteLine("{0:MM-dd-yyyy hh:mm:ss.fff} - Resuming BeginTranscodeV3 - " + format, DateTime.Now);
            Result.Set(context, value);
        }

    }
}
