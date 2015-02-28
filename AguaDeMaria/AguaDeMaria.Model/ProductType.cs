namespace AguaDeMaria.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductType")]
    public partial class ProductType
    {
        public ProductType()
        {
            DeliveryReceiptDetails = new HashSet<DeliveryReceiptDetail>();
            OrderDetails = new HashSet<OrderDetail>();
            ReturnReceiptDetails = new HashSet<ReturnReceiptDetail>();
            SalesInvoiceDetails = new HashSet<SalesInvoiceDetail>();
        }

        public int ProductTypeId { get; set; }

        [Column("ProductType")]
        [Required]
        [StringLength(100)]
        public string ProductType1 { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public decimal BasePrice { get; set; }

        public int IsInvetoriable { get; set; }

        public virtual ICollection<DeliveryReceiptDetail> DeliveryReceiptDetails { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<ReturnReceiptDetail> ReturnReceiptDetails { get; set; }

        public virtual ICollection<SalesInvoiceDetail> SalesInvoiceDetails { get; set; }

        public virtual ICollection<PickupSlipDetail> PickupSlipDetails { get; set; }
    }
}
