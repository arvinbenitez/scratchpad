namespace AguaDeMaria.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PickupSlip")]
    public class PickupSlip
    {
        public PickupSlip()
        {
            PickupSlipDetails = new HashSet<PickupSlipDetail>();
        }

        public int PickupSlipId { get; set; }

        [Required]
        [StringLength(50)]
        public string PickupSlipNumber { get; set; }

        [StringLength(200)]
        public string Notes { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Pickup Date")]
        public DateTime PickupDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<PickupSlipDetail> PickupSlipDetails { get; set; }

    }
}
