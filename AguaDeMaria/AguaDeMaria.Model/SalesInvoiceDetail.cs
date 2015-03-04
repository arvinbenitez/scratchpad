namespace AguaDeMaria.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SalesInvoiceDetail")]
    public partial class SalesInvoiceDetail
    {
        public int SalesInvoiceDetailId { get; set; }

        public int SalesInvoiceId { get; set; }

        public int DeliveryReceiptLedgerId { get; set; }
        public decimal Amount { get; set; }

        public virtual DeliveryReceiptLedger DeliveryReceiptLedger { get; set; }

        public virtual SalesInvoice SalesInvoice { get; set; }

    }
}
