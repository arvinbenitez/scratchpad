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
    
    public partial class ReturnReceiptDetail
    {
        public int ReturnReceiptId { get; set; }
        public int ReturnReceiptDetailId { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Quantity { get; set; }
    
        public virtual ProductType ProductType { get; set; }
        public virtual ReturnReceipt ReturnReceipt { get; set; }
    }
}
