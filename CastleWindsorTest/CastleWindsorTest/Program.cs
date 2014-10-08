using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace CastleWindsorTest
{
    class Program
    {
        private static WindsorContainer container = new WindsorContainer();

        static Program()
        {
            container.Register(Component.For<IFileBuffer>().ImplementedBy<FileBuffer>());
            container.Register(Component.For<IChecksumProvider>().ImplementedBy<ChecksumProvider>());
        }

        static void Main(string[] args)
        {
            var fileBuffer = container.Resolve<IFileBuffer>();
            Console.WriteLine(fileBuffer.Exists());
            Console.WriteLine(fileBuffer.Checksum());
            Console.ReadLine();
        }
    }
}
