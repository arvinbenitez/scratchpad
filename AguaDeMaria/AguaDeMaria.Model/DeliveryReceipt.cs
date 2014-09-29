namespace AguaDeMaria.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DeliveryReceipt")]
    public partial class DeliveryReceipt
    {
        public DeliveryReceipt()
        {
            DeliveryReceiptDetails = new HashSet<DeliveryReceiptDetail>();
        }

        public int DeliveryReceiptId { get; set; }

        [Required]
        [StringLength(50)]
        public string DRNumber { get; set; }

        public DateTime DRDate { get; set; }

        public int CustomerId { get; set; }

        public int SalesInvoiceId { get; set; }

        public int? OrderId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual SalesInvoice SalesInvoice { get; set; }

        public virtual ICollection<DeliveryReceiptDetail> DeliveryReceiptDetails { get; set; }

        public virtual Order Order { get; set; }
    }
}
