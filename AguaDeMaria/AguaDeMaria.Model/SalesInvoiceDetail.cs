namespace AguaDeMaria.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesInvoiceDetail")]
    public partial class SalesInvoiceDetail
    {
        public int SalesInvoiceDetailId { get; set; }

        public int SalesInvoiceId { get; set; }

        public int ProductTypeId { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual SalesInvoice SalesInvoice { get; set; }
    }
}
