//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AguaDeMaria.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.SalesInvoices = new HashSet<SalesInvoice>();
        }
    
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public System.DateTime OrderDate { get; set; }
    
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
    }
}
