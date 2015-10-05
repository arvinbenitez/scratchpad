namespace Algorithms.Sorting
{
    class ShellSort : ISort
    {
        public long Sort(int[] arr)
        {
            long iterations =0;
            var h = 1;
            var upper = arr.Length;

            while (h <= upper/3) h = h*3 + 1;

            while (h > 0)
            {
                for (int outer = 0; outer < upper; outer++)
                {
                    int temp = arr[outer];
                    int inner = outer;
                    while ((inner > h - 1) && arr[inner - h] >= temp)
                    {
                        arr[inner] = arr[inner - h];
                        inner -= h;
                        iterations++;
                    }
                    arr[inner] = temp;
                }
                h = (h - 1)/3;
            }
            return iterations;
        }
    }
}