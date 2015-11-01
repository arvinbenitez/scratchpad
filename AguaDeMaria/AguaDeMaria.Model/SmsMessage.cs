namespace AguaDeMaria.Model
{
    public class SmsMessage
    {
        public int MessageId { get; set; }

        public string Message { get; set; }

        public string SourcePhoneNumber { get; set; }

        public string ReceivingPhoneNumber { get; set; }

        public int Status { get; set; }

        public string TimeStamp { get; set; }

    }
}
