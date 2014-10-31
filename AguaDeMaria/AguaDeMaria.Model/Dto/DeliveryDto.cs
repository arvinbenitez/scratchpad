﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AguaDeMaria.Model;

namespace AguaDeMaria.Model.Dto
{
    public class DeliveryDto
    {
        public int DeliveryReceiptId { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Delivery Number")]
        public string DRNumber { get; set; }

        [Display(Name = "Delivery Date")]
        public DateTime DRDate { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }

        public int? OrderId { get; set; }

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

        public IEnumerable<string> ValidationErrors
        {
            get
            {
                if (SlimQty <= 0 && RoundQty <= 0)
                    yield return "You must enter a Quantity";
                if (SlimQty > 0 && SlimUnitPrice == 0 & SlimAmount == 0)
                    yield return "You must specify an Amount or UnitPrice for Slim";
                if (RoundQty > 0 && RoundUnitPrice == 0 & RoundAmount == 0)
                    yield return "You must specify an Amount or UnitPrice for Round";
            }
        }

        public bool IsValid
        {
            get { return !ValidationErrors.Any(); }
        }

        public string CustomerAddress { get; set; }
    }
}