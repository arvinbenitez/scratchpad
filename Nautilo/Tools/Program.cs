using System;
using Castle.Windsor;
using log4net.Config;
using Tools.DependencyRegistration;

namespace Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var container = new WindsorContainer();
            var registration = new DependencyRegistration.DependencyRegistration(container);
            registration.Initialize();
            var loggingTest = container.Resolve<ILoggingTest>();
            Console.WriteLine("Add");
            var result = loggingTest.Add(100, 600);

            Console.WriteLine("Exception");
            try
            {
                loggingTest.ExceptionTest("Hello");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception");
            }
            Console.ReadLine();
        }
    }
}
