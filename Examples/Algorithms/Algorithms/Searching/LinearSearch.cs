namespace Algorithms.Searching
{
    class LinearSearch : ISearch
    {
        public long Search(int[] arr, int searchValue, out int numberOfTries)
        {
            numberOfTries = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                numberOfTries ++;
                if (arr[i] == searchValue)
                    return i;
            }
            return -1;
        }
    }
}