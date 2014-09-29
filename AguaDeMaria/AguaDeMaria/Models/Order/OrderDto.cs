#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AguaDeMaria.Model;

#endregion

namespace AguaDeMaria.Models.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        [Display(Name = "Order Number")]
        [Required]
        public string OrderNumber { get; set; }

        [Display(Name = "Order Date")]
        [Required]
        public DateTime OrderDate { get; set; }

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

        public string OrderStatusName { get; set; }

        [Display(Name = "Order Status")]
        public int OrderStatusId { get; set; }

        public int SlimOrderDetailId { get; set; }
        public int RoundOrderDetailId { get; set; }

    }
}