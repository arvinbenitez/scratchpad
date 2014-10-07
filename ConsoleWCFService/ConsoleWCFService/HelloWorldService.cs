using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWCFService
{
    [ServiceContract]
    public interface IHelloWorldService
    {
        [OperationContract]
        string SayHello(string name);

    }

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    class HelloWorldService: IHelloWorldService
    {
        public string SayHello(string name)
        {
            
            return "Hello " + name.Length;
        }
    }
}
