using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("SmsMessage")]
    public class SmsMessage
    {
        [Key]
        public int MessageId { get; set; }

        public int RemoteMessageId { get; set; }

        public string SourcePhoneNumber { get; set; }

        public string ReceivingPhoneNumber { get; set; }
        public string Message { get; set; }

        public int Status { get; set; }

        public string TimeStamp { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
        public string ContactName { get; set; }
    }
}
