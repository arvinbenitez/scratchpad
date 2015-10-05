namespace Algorithms.Sorting
{
    class SelectionSort : ISort
    {
        public long Sort(int[] arr)
        {
            long iterations = 0;
            var upper = arr.GetUpperBound(0);
            for (var outer = 0; outer <= upper; outer++)
            {
                var min = outer;
                for (var inner = outer + 1; inner <= upper; inner++)
                {
                    if (arr[inner] < arr[min]) min = inner;
                    iterations++;
                }
                var temp = arr[outer];
                arr[outer] = arr[min];
                arr[min] = temp;
            }
            return iterations;
        }
    }
}