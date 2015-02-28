namespace AguaDeMaria.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DeliveryReceiptDetail")]
    public partial class DeliveryReceiptDetail
    {
        public int DeliveryReceiptDetailId { get; set; }

        public int DeliveryReceiptId { get; set; }

        public int ProductTypeId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Amount { get; set; }

        public virtual DeliveryReceipt DeliveryReceipt { get; set; }

        public virtual ProductType ProductType { get; set; }
    }
}
