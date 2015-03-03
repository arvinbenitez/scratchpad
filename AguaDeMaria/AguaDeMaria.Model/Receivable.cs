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
        public string DrNumber { get; set; }
        public DateTime DrDate { get; set; }
        public decimal Amount { get; set; }

        public decimal PaidAmount { get; set; }

    }
}