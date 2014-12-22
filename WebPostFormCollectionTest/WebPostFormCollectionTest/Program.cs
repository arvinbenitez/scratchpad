using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ServiceStack;
using System.Net;
using System.Net.Http;


namespace WebPostFormCollectionTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var client = new ServiceStack.JsonServiceClient("http://localhost:88");

            var dictionary = new NameValueCollection
            {
                {"field1","arvin"},
                {"field2","benitez"}
            };

            var loginParameter = new
            {
                EmailAddress = "admin@rentthatbike.com",
                Password = "admin",
                Redirect = "/Home"
            };

            //perform login
            var result = client.Post<string>("/Login/LogIn", loginParameter);
            string jsonParameter = "{\"FormData\":[{\"Field1\":\"arvin\"}]}";
            var x = client.Post<string>("/Login/TestAction", jsonParameter);

            var responsex = "http://localhost:88/login/testaction".PostToUrl(dictionary.ToFormUrlEncoded());

            ////string formData = "field1=value1&field2=value2&field3=value3";
            ////string values = "[{\"field1\" : \"value1\"},{\"field2\" : \"value2\"}]";
            ////string values = "{Field1: \"value1\", Field2: \"value2\"}";

            ////var result = client.Post<string>("/login/testaction", dictionary.ToFormUrlEncoded());
            //var result = client.Get<string>("/login/testaction/0?" + dictionary.ToFormUrlEncoded());

        }

        public class CreateNodeDto : NameValueCollection, IReturn<string>
        {

        }
    }
}
