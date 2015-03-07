using System;

namespace AguaDeMaria.Models.Invoice
{
    public class SalesInvoiceDto
    {
        public int SalesInvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }

        public string DeliveryReceipts { get; set; }
    }
}