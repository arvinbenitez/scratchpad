using System;
using System.ComponentModel.DataAnnotations;

namespace AguaDeMaria.Model.Dto
{
    public class InventoryLedgerDto
    {
        [Display(Name = "Notes")]
        [Required]
        public string Notes { get; set; }

        [Display(Name = "Slim")]
        [Required]
        public int SlimQty { get; set; }
        [Display(Name = "Round")]
        [Required]
        public int RoundQty { get; set; }
        [Display(Name = "Date")]
        [Required]
        public DateTime PostDate { get; set; }
        public int CustomerId { get; set; }
    }
}