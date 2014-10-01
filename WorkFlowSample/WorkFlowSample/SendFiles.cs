using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowSample
{
    public class SendFiles: NativeActivity<bool>
    {
        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        [RequiredArgument]
        public InArgument<string> Format { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            string format = context.GetValue(Format);
            Debug.WriteLine("{0:MM-dd-yyyy hh:mm:ss.fff} - Executing SendFiles " + format, DateTime.Now);
            System.Threading.Thread.Sleep(5000);
            Debug.WriteLine("{0:MM-dd-yyyy hh:mm:ss.fff} - Done SendFiles" + format, DateTime.Now);
            context.CreateBookmark(format, OnResume);
        }

        private void OnResume(NativeActivityContext context, Bookmark bookmark, object value)
        {
            string format = context.GetValue(Format);
            Debug.WriteLine("{0:MM-dd-yyyy hh:mm:ss.fff} - Resuming Sendfiles " + format, DateTime.Now);
            Result.Set(context, true);
        }
    }
}
