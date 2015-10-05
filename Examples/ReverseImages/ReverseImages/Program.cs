using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ReverseImages
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Temp\Video\jpg-wrap";
            var reversed = @"C:\Temp\Video\Wrap-Reversed";

            //read all the files in the folder
            var files = Directory.GetFiles(path, "*.jpg");
            var fileCounter = 1;
            foreach (var file in files.OrderByDescending(x=> x))
            {
                var destinationFile = Path.Combine(reversed, string.Format("{0:000000}.jpg",fileCounter));
                Console.WriteLine("Copying {0} to {1}", file, destinationFile);
                File.Copy(file, destinationFile, true);
                fileCounter++;
            }
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
