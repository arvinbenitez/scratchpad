using System;
using System.Diagnostics;

namespace Algorithms
{
    public class Timer
    {

        private Stopwatch timer;

        public Timer()
        {
            timer = new Stopwatch();
        }

        public static Timer StartTimer()
        {
            var timer = new Timer();
            timer.Start();
            return timer;
        }

        public void Start()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public override string ToString()
        {
            return timer.ElapsedMilliseconds.ToString("N") + " ms";
        }
    }
}