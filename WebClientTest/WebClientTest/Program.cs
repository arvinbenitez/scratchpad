using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;

namespace WebClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:88");
                var details = new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string,string>("EmailAddress","admin@rentthatbike.com"),
                    new KeyValuePair<string,string>("Password","admin"),
                    new KeyValuePair<string,string>("Redirect","/Home")
                };
                var formContent = new FormUrlEncodedContent(details);
                var response = client.PostAsync(@"/login/login", formContent).Result;
                var asdjk = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    var another = new FormUrlEncodedContent(details);
                    var xesponse = client.PostAsync(@"/login/testaction", another).Result;
                    var resultxx = xesponse.Content.ReadAsStringAsync().Result;

                };
                Console.WriteLine("Done Posting");
                //var getResponse = client.GetStringAsync("/login/index").Result;
                //if (getResponse.IsSuccessStatusCode)
                //{
                //    Console.WriteLine("Response is: /n{0}", getResponse.ToString());
                //}
                //else
                //{
                //    Console.WriteLine("Not quite");
                //}
                Console.WriteLine("Done with post and get");
            }
            Console.WriteLine();
            Console.ReadLine();

        }
    }
}
