namespace Algorithms.Sorting
{
    internal class InsertionSort : ISort
    {
        public long Sort(int[] arr)
        {
            long iterations = 0;
            var upper = arr.GetUpperBound(0);

            for (var outer = 1; outer <= upper; outer++)
            {
                var temp = arr[outer];
                var inner = outer;
                while (inner > 0 && arr[inner - 1] >= temp)
                {
                    arr[inner] = arr[inner - 1];
                    inner -= 1;
                    iterations++;
                }
                arr[inner] = temp;
            }
            return iterations;
        }

    }
}