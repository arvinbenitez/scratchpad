using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var proxy = new HelloWorldServiceClient();
                Console.WriteLine(proxy.SayHello(null));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught a fault exception");
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }
}
