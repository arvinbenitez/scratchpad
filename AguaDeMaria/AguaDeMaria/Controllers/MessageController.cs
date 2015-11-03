using System;
using System.Linq;
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
            var sms =
                smsMessageRepository.Get(
                    x =>
                        x.RemoteMessageId == message.RemoteMessageId &&
                        x.ReceivingPhoneNumber == message.ReceivingPhoneNumber).FirstOrDefault();
            if (sms != null)
            {
                sms.Updated = DateTime.UtcNow;
                sms.Message = message.Message;
                sms.ReceivingPhoneNumber = message.ReceivingPhoneNumber;
                sms.SourcePhoneNumber = message.SourcePhoneNumber;
                sms.Status = message.Status;
                sms.TimeStamp = message.TimeStamp;
                sms.ContactName = message.ContactName;
                smsMessageRepository.Update(sms);
            }
            else
            {
                message.Created = DateTime.UtcNow;
                message.Updated = message.Created;
                smsMessageRepository.Insert(message);
            }
            smsMessageRepository.Commit();

            return message;
        }
    }
}
