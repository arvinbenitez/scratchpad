using System;
using System.Collections.Generic;
using System.Linq;

namespace AguaDeMaria.Model.Dto
{
    public class PaymentDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int SalesInvoiceId { get; set; }

        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

        public decimal Amount { get; set; }

        public bool IsNew
        {
            get { return SalesInvoiceId == 0; }
        }

        public bool IsValid { get { return !ValidationErrors.Any(); }}

        public IEnumerable<string> ValidationErrors
        {
            get
            {
                if (Amount <= 0 )
                    yield return "You must enter an Amount";
            }
        }
    }
}