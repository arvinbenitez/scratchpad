using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sorting
{
    public class RadixSort: ISort
    {
        public long Sort(int[] arr)
        {
            long iterations = 0;
            long[] digits =
            {
                10,
                100,
                1000,
                10000,
                100000,
                1000000,
                10000000,
                100000000,
                1000000000,
                10000000000
            };
            var queues = Enumerable.Range(0, 10).Select(x => new Queue<int>()).ToArray();
            for (int j = 0; j <= digits.GetUpperBound(0); j++)
            {
                for (int i = 0; i <= arr.GetUpperBound(0); i++)
                {
                    var digit = GetBucket(arr[i],digits[j]);
                    queues[digit].Enqueue(arr[i]);
                    iterations ++;
                }
                Sort(arr, queues);
            }
            return iterations;
        }

        private long GetBucket(int number, long bucket)
        {
            var digit = (number%bucket) / (bucket/10) ; 
            return digit;
        }

        private void Sort(int[] arr, Queue<int>[] queues)
        {
            var index = 0;
            for (int i = 0; i <= queues.GetUpperBound(0); i++)
            {
                while (queues[i].Count > 0)
                {
                    arr[index] = queues[i].Dequeue();
                    index++;
                }
            }
        }
    }
}

