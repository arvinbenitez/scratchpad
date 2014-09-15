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

        #region Converters

        public static OrderDto TranslateFrom(Model.Order order)
        {
            var orderDto = new OrderDto
            {
                CustomerId = order.CustomerId,
                CustomerName = order.Customer != null ? order.Customer.CustomerName : string.Empty,
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                OrderStatusId = order.OrderStatusId,
                OrderStatusName = order.OrderStatus != null ? order.OrderStatus.StatusName : string.Empty
            };
            var slim = order.OrderDetails.FirstOrDefault(x => x.ProductTypeId == DataConstants.ProductTypes.Slim);
            if (slim != null)
            {
                orderDto.SlimQty = slim.Qty;
                orderDto.SlimOrderDetailId = slim.OrderDetailId;
            }

            var round = order.OrderDetails.FirstOrDefault(x => x.ProductTypeId == DataConstants.ProductTypes.Round);
            if (round != null)
            {
                orderDto.RoundQty = round.Qty;
                orderDto.RoundOrderDetailId = round.OrderDetailId;
            }
            return orderDto;
        }

        public static Model.Order TranslateFrom(OrderDto orderDto)
        {
            Model.Order order = new Model.Order
            {
                OrderId = orderDto.OrderId,
                OrderNumber = orderDto.OrderNumber,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
                OrderStatusId = orderDto.OrderStatusId
            };
            order.OrderDetails.Add(new OrderDetail
            {
                Order = order,
                OrderDetailId = orderDto.SlimOrderDetailId,
                ProductTypeId = DataConstants.ProductTypes.Slim,
                Qty = orderDto.SlimQty
            });
            order.OrderDetails.Add(new OrderDetail
            {
                Order = order,
                OrderDetailId = orderDto.RoundOrderDetailId,
                ProductTypeId = DataConstants.ProductTypes.Round,
                Qty = orderDto.RoundQty
            });
            return order;
        }

        #endregion
    }
}