namespace AguaDeMaria.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InventorySummary")]

    public class InventorySummary
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime? LatestPickupDate { get; set; }
        public DateTime? LatestDeliveryDate { get; set; }

        public int? Slim { get; set; }
        public int? Round { get; set; }
    }
}
