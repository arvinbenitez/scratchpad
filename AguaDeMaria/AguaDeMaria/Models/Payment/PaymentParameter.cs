namespace AguaDeMaria.Models.Payment
{
    public class PaymentParameter
    {
        public int? DeliveryReceiptId { get; set; }

        public bool IsNew
        {
            get { return DeliveryReceiptId.HasValue; }
        }
    }
}