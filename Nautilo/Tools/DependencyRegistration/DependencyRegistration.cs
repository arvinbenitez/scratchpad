using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Nautilo.Core.IOC;
using Nautilo.Core.Logging;

namespace Tools.DependencyRegistration
{
    public class DependencyRegistration: DependencyRegistrationBase
    {
        public DependencyRegistration(IWindsorContainer container) : base(container)
        {
        }

        protected override void RegisterDependencies(IWindsorContainer container)
        {
            container.Register(Component.For<ILoggingTest>().ImplementedBy<LoggingTest>());
        }
    }

    public interface ILoggingTest : ILogMe
    {
        int Add(int firstNumber, int secondNumber);
        void ExceptionTest(string param);
    }

    public class TestDependencyDependencyRegistration : DependencyRegistrationBase
    {
        public TestDependencyDependencyRegistration(IWindsorContainer container)
            : base(container)
        {
        }

        protected override void RegisterDependencies(IWindsorContainer container)
        {
            container.Register(Component.For<ILoggingTest>().ImplementedBy<LoggingTest>());
        }
    }

    public class LoggingTest : ILoggingTest
    {
        public int Add(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }

        public void ExceptionTest(string param)
        {
            throw new Exception(string.Format("Test exception with parameter {0}", param));
        }
    }

}