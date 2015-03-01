namespace AguaDeMaria.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DeliveryReceipt")]
    public partial class DeliveryReceipt
    {
        public DeliveryReceipt()
        {
            DeliveryReceiptDetails = new HashSet<DeliveryReceiptDetail>();
            DeliveryReceiptLedgers = new HashSet<DeliveryReceiptLedger>();
        }

        public int DeliveryReceiptId { get; set; }

        [Required]
        [StringLength(50)]
        public string DRNumber { get; set; }

        public DateTime DRDate { get; set; }

        public int CustomerId { get; set; }

        public int? OrderId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<DeliveryReceiptDetail> DeliveryReceiptDetails { get; set; }

        public virtual ICollection<DeliveryReceiptLedger> DeliveryReceiptLedgers { get; set; }

        public virtual Order Order { get; set; }
    }
}
