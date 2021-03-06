﻿#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#endregion

namespace AguaDeMaria.Model.Dto
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

        public bool IsEditable
        {
            get { return CalculatedStatusId == DataConstants.OrderStatus.Pending; }
        }

        public int SlimQtyDelivered { get; set; }
        public int RoundQtyDelivered { get; set; }

        public int SlimQtyBalance
        {
            get { return SlimQtyDelivered > SlimQty ? 0 : SlimQty - SlimQtyDelivered; }
        }

        public int RoundQtyBalance
        {
            get { return RoundQtyDelivered > RoundQty ? 0 : RoundQty - RoundQtyDelivered; }
        }

        public int CalculatedStatusId
        {
            get { return CalculateOrderStatus(this); }
        }

        private static int CalculateOrderStatus(OrderDto orderDto)
        {
            if (orderDto.OrderStatusId == DataConstants.OrderStatus.Cancelled)
                return DataConstants.OrderStatus.Cancelled;
            if (orderDto.RoundQtyBalance <= 0 && orderDto.SlimQtyBalance <= 0)
                return DataConstants.OrderStatus.Delivered;
            if (orderDto.RoundQty == orderDto.RoundQtyBalance && orderDto.SlimQty == orderDto.SlimQtyBalance)
                return DataConstants.OrderStatus.Pending;
            return DataConstants.OrderStatus.PartiallyDelivered;
        }
    }
}