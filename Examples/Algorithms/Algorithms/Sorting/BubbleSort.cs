namespace Algorithms.Sorting
{
    class BubbleSort : ISort
    {
        public long Sort(int[] arr)
        {
            long numberOfIterations = 0;
            var upper = arr.GetUpperBound(0);
            for (int outer = upper; outer >= 1; outer--)
            {
                for (int inner = 0; inner <= outer - 1; inner++)
                    if (arr[inner] > arr[inner + 1])
                    {
                        int temp = arr[inner];
                        arr[inner] = arr[inner + 1];
                        arr[inner + 1] = temp;
                        numberOfIterations++;
                    }
            }
            return numberOfIterations;
        }
    }
}