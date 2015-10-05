namespace Algorithms.Searching
{
    class BinarySearch : ISearch
    {
        public long Search(int[] arr, int searchValue, out int numberOfTries)
        {
            var lowerbound = 0;
            var upperbound = arr.GetUpperBound(0);
            numberOfTries = 0;
            while (lowerbound <= upperbound)
            {
                numberOfTries++;
                var mid = (lowerbound + upperbound) / 2;
                if (arr[mid] == searchValue)
                {
                    return mid;
                }
                
                
                if (searchValue < arr[mid])
                {
                    upperbound = mid-1;
                }
                else
                {
                    lowerbound = mid+1;
                }
            }
            return -1;
        }
    }
}