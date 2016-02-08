using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Nautilo.Core.IOC;
using Nautilo.Core.Logging;
using NUnit.Framework;

namespace Nautilo.Core.Test
{
    [TestFixture]
    public class InterceptorTest
    {
        private IWindsorContainer container;
        private TestDependencyDependencyRegistration ioc;


        [SetUp]
        public void Init()
        {
            container = new WindsorContainer();
            ioc = new TestDependencyDependencyRegistration(container);
            ioc.Initialize();
        }

        [Test]
        public void When_IMustBeLogged_Should_CallInterceptor()
        {
            var loggingTest = container.Resolve<ILoggingTest>();
            var result = loggingTest.Add(100, 200);
            Assert.That(result, Is.EqualTo(300));
        }

        public class LoggingTest : ILoggingTest
        {
            public int Add(int firstNumber, int secondNumber)
            {
                return firstNumber + secondNumber;
            }

            public void ExceptionTest(string param)
            {
                throw new Exception(string.Format("Test exception with parameter {0}",param));
            }
        }

        public interface ILoggingTest : ILogMe
        {
            int Add(int firstNumber, int secondNumber);
            void ExceptionTest(string param);
        }

        public class TestDependencyDependencyRegistration : DependencyRegistrationBase
        {
            public TestDependencyDependencyRegistration(IWindsorContainer container) : base(container)
            {
            }

            protected override void RegisterDependencies(IWindsorContainer container)
            {
                container.Register(Component.For<ILoggingTest>().ImplementedBy<LoggingTest>());
            }
        }
    }
}
