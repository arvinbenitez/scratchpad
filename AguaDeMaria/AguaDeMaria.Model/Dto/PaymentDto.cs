using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AguaDeMaria.Model.Dto
{
    public class PaymentDto
    {
        public int CustomerId { get; set; }

        [Display(Name = "Customer")]
        [Required]
        public string CustomerName { get; set; }

        public int SalesInvoiceId { get; set; }

        [Display(Name = "Invoice Number")]
        [Required]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice Date")]
        [Required]
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