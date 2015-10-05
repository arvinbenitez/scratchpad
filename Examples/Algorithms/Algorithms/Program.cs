using System;
using System.Collections;
using Algorithms.DataStructure.BinaryTree;
using Algorithms.Searching;
using Algorithms.Sorting;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceLocator.Init();
            const int count = 5000;
            var random = new Random(count);

            var list = new int[count];
            for (var i = 0; i < count; i++)
            {
                list[i] = random.Next();
            }

            foreach (var sortAlgorithm in ServiceLocator.GetAllInstance<ISort>())
            {
                RunSort(list.Clone() as int[], sortAlgorithm);
            }

            //RunSort(list.Clone() as int[], ServiceLocator.GetNamedInstance<ISort>(SortAlgorithm.Radix.ToString()));
            //RunSort(list.Clone() as int[], new InsertionSort());
            //RunSort(list.Clone() as int[], new SelectionSort());
            //RunSort(list.Clone() as int[], new BubbleSort());
            //RunSort(list.Clone() as int[], new ShellSort());
            var searchValue = list[0];
            var sortedList = list.Clone() as int[];
            (new InsertionSort()).Sort(sortedList);

            RunSearch(sortedList, new LinearSearch(), searchValue);
            RunSearch(sortedList, new BinarySearch(), searchValue);

            RunBinarySearchTree(list.Clone() as int[]);
            Console.ReadLine();

        }

        private static void RunBinarySearchTree(int[] ints)
        {
            Console.WriteLine("BinarySearchTree");
            var timer = Timer.StartTimer();
            var bst = new BinarySearchTree();
            foreach (var number in ints)
            {
                bst.Insert(number);
            }
            timer.Stop();
            Console.WriteLine("Took {0}. Min/Max = {1}/{2}", timer, bst.MinValue(), bst.MaxValue());
            //bst.InOrder(bst.Root);
        }

        private static void RunSort(int[] list, ISort algorithm)
        {
            Console.WriteLine(algorithm);
            var timer = Timer.StartTimer();
            var iterations = algorithm.Sort(list);
            timer.Stop();
            Console.WriteLine("{0:##,###} iterations took {1}. First/Last: {2}/{3}", iterations, timer, list[0], list[list.GetUpperBound(0)]);
            
        }
        private static void RunSearch(int[] list, ISearch algorithm, int searchValue)
        {
            Console.WriteLine(algorithm);
            var timer = Timer.StartTimer();
            int iterations;
            var index = algorithm.Search(list, searchValue, out iterations);
            timer.Stop();
            Console.WriteLine("{0:##,###} iterations took {1}. Found at {2}", iterations, timer, index);
            
        }
    }
}
