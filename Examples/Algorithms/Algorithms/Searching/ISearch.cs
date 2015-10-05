namespace Algorithms.Searching
{
    interface ISearch
    {
        long Search(int[] arr, int searchValue, out int numberOfTries);
    }
}