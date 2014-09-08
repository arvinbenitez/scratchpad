namespace AguaDeMaria.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesInvoice")]
    public partial class SalesInvoice
    {
        public SalesInvoice()
        {
            DeliveryReceipts = new HashSet<DeliveryReceipt>();
            SalesInvoiceDetails = new HashSet<SalesInvoiceDetail>();
        }

        public int SalesInvoiceId { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int CustomerId { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }

        public int? OrderId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<DeliveryReceipt> DeliveryReceipts { get; set; }

        public virtual Order Order { get; set; }

        public virtual ICollection<SalesInvoiceDetail> SalesInvoiceDetails { get; set; }
    }
}
