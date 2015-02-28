namespace AguaDeMaria.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ReturnReceipt")]
    public partial class ReturnReceipt
    {
        public ReturnReceipt()
        {
            ReturnReceiptDetails = new HashSet<ReturnReceiptDetail>();
        }

        public int ReturnReceiptId { get; set; }

        public DateTime ReturnReceiptDate { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<ReturnReceiptDetail> ReturnReceiptDetails { get; set; }
    }
}
