namespace AguaDeMaria.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SalesInvoice")]
    public partial class SalesInvoice
    {
        public SalesInvoice()
        {
            SalesInvoiceDetails = new HashSet<SalesInvoiceDetail>();
        }

        public int SalesInvoiceId { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int CustomerId { get; set; }


        public virtual Customer Customer { get; set; }

        public decimal? Amount { get; set; }


        public virtual ICollection<SalesInvoiceDetail> SalesInvoiceDetails { get; set; }

        public bool IsNew { get { return SalesInvoiceId <= 0; } }
    }
}
