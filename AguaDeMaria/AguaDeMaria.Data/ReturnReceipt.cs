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
    
    public partial class ReturnReceipt
    {
        public ReturnReceipt()
        {
            this.ReturnReceiptDetails = new HashSet<ReturnReceiptDetail>();
        }
    
        public int ReturnReceiptId { get; set; }
        public System.DateTime ReturnReceiptDate { get; set; }
        public int CustomerId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual ICollection<ReturnReceiptDetail> ReturnReceiptDetails { get; set; }
    }
}
