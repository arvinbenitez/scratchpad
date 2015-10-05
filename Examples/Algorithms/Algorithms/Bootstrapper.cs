using System.Linq;
using Algorithms.Sorting;
using Ninject;
using Ninject.Modules;

namespace Algorithms
{
    public enum SortAlgorithm
    {
        Bubble,
        Insertion,
        Selection,
        Radix,
        Shell
    }

    class Bootstrapper: NinjectModule
    {
        public override void Load()
        {
            Bind<ISort>().To<BubbleSort>().Named(SortAlgorithm.Bubble.ToString());
            Bind<ISort>().To<InsertionSort>().Named(SortAlgorithm.Insertion.ToString());
            Bind<ISort>().To<SelectionSort>().Named(SortAlgorithm.Selection.ToString());
            Bind<ISort>().To<RadixSort>().Named(SortAlgorithm.Radix.ToString());
            Bind<ISort>().To<ShellSort>().Named(SortAlgorithm.Shell.ToString());
        }
    }

    class ServiceLocator
    {
        private static IKernel kernel; 
        public static void Init()
        {
            kernel = new StandardKernel(new Bootstrapper());
        }

        public static T GetInstance<T>()
        {
            return kernel.Get<T>();
        }
        public static T GetNamedInstance<T>(string name)
        {
            return kernel.Get<T>(name);
        }

        public static T[] GetAllInstance<T>()
        {
            return kernel.GetAll<T>().ToArray();
        }
    }
}
