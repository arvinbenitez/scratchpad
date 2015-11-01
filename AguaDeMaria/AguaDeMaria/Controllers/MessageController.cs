using System.Web.Http;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Configuration.Ioc;
using AguaDeMaria.Model;

namespace AguaDeMaria.Controllers
{
    public class MessageController : ApiController
    {
        [HttpPost]
        public SmsMessage Post(SmsMessage message)
        {
            var smsMessageRepository = ServiceResolver.Instance.Resolve<IRepository<SmsMessage>>();
            return message;
        }
    }
}
