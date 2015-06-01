using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("InventoryLedger")]
    public class InventoryLedger
    {
        public int InventoryLedgerId { get; set; }
        public DataConstants.InventoryLedgerType InventoryLedgerTypeId { get; set; }
        public DateTime PostDate { get; set; }
        public int CustomerId { get; set; }
        public int ReferenceId { get; set; }
        public int ProductTypeId { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
    }
}