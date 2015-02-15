using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AguaDeMaria.Models.Invoice
{
    public class SalesInvoiceDto
    {
        public int SalesInvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string OrderNumber{get;set;}

        public bool IsPaid { get; set; }
    }
}