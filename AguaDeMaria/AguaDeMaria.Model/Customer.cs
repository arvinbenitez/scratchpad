namespace AguaDeMaria.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            DeliveryReceipts = new HashSet<DeliveryReceipt>();
            ReturnReceipts = new HashSet<ReturnReceipt>();
            SalesInvoices = new HashSet<SalesInvoice>();
        }

        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }

        [Display(Name = "Customer Code")]
        [StringLength(50)]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Type")]
        public int CustomerTypeId { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string GPSCoordinate { get; set; }

        public virtual CustomerType CustomerType { get; set; }

        public virtual ICollection<DeliveryReceipt> DeliveryReceipts { get; set; }

        public virtual ICollection<ReturnReceipt> ReturnReceipts { get; set; }

        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
    }
}
