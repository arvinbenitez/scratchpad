using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = @"C:\temp\root";
            var file = @"tobedeleted\tobedeleted.txt";
            var fullPath = Path.Combine(root, file);

            fullPath.Clean();
        }
    }

    static class StringExtensions
    {
        public static void Clean(this string fullPath)
        {
            var parentDirectory = Directory.GetParent(fullPath);
            Directory.Delete(parentDirectory.FullName,true);
        }


    }
}
