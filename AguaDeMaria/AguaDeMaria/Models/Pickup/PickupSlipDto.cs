#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#endregion

namespace AguaDeMaria.Models.Pickup
{
    public class PickupSlipDto
    {
        public int PickupSlipId { get; set; }

        [Display(Name = "Pickup Slip Number")]
        [Required]
        public string PickupSlipNumber { get; set; }

        [StringLength(200)]
        public string Notes { get; set; }

        [Display(Name = "Pickup Date")]
        [Required]
        public DateTime PickupDate { get; set; }

        [Display(Name = "Customer")]
        [Required]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        [Display(Name = "Round")]
        [Required]
        public int RoundQty { get; set; }

        [Display(Name = "Slim")]
        [Required]
        public int SlimQty { get; set; }

        public int SlimPickupSlipDetailId { get; set; }
        public int RoundPickupSlipDetailId { get; set; }

        public IEnumerable<string> ValidationErrors
        {
            get
            {
                if (SlimQty <= 0 && RoundQty <= 0)
                    yield return "You must enter a Quantity";
            }
        }

        public bool IsValid
        {
            get { return !ValidationErrors.Any(); }
        }

    }
}