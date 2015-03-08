using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("Receivable")]
    public class Receivable
    {
        [Key]
        public int DeliveryReceiptId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string CustomerName
        {
            get { return Customer != null ? Customer.CustomerName : string.Empty; }
        }

        public string DrNumber { get; set; }
        public DateTime DrDate { get; set; }
        public decimal Amount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal ReceivableAmount
        {
            get { return Amount - PaidAmount; }
        }

        public int DeliveryReceiptLedgerId { get; set; }
    }
}