using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AguaDeMaria.Model;

namespace AguaDeMaria.Models.Delivery
{
    public class DeliveryDto
    {
        public int DeliveryReceiptId { get; set; }

        [Required]
        [StringLength(20)]
        public string DRNumber { get; set; }

        public DateTime DRDate { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }

        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        [Display(Name = "Round")]
        [Required]
        public int RoundQty { get; set; }

        [Display(Name = "Slim")]
        [Required]
        public int SlimQty { get; set; }

        public int SlimDeliveryReceiptDetailId { get; set; }
        public int RoundDeliveryReceiptDetailId { get; set; }

        public decimal SlimUnitPrice { get; set; }
        public decimal SlimAmount { get; set; }

        public decimal RoundUnitPrice { get; set; }
        public decimal RoundAmount { get; set; }

    }
}