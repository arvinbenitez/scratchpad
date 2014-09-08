namespace AguaDeMaria.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReturnReceiptDetail")]
    public partial class ReturnReceiptDetail
    {
        public int ReturnReceiptId { get; set; }

        public int ReturnReceiptDetailId { get; set; }

        public int ProductTypeId { get; set; }

        public decimal Quantity { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual ReturnReceipt ReturnReceipt { get; set; }
    }
}
