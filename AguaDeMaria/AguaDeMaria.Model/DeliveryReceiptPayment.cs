using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("DeliveryReceiptPayment")]
    public class DeliveryReceiptPayment
    {
        [Key]
        public int DeliveryReceiptId { get; set; }

        public decimal AmountPaid { get; set; }

        public DeliveryReceipt DeliveryReceipt { get; set; }
        
    }
}