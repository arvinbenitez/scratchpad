using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("DeliveryReceiptLedger")]
    public class DeliveryReceiptLedger
    {
        public int DeliveryReceiptLedgerId { get; set; }
        public int DeliveryReceiptId { get;set; }

        public virtual DeliveryReceipt DeliveryReceipt { get; set; }

        public decimal Amount { get; set; }

        public bool IsNew()
        {
            return DeliveryReceiptLedgerId == 0;
        }
    }
}