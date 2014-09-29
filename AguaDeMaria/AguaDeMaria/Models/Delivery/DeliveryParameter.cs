using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AguaDeMaria.Models.Delivery
{
    public class DeliveryParameter
    {
        public int? OrderId { get; set; }

        public int? DeliveryId { get; set; }

        public bool IsNewDeliveryFromOrder
        {
            get { return (OrderId.HasValue && OrderId.Value > 0); }
        }

        public bool IsNewDelivery
        {
            get { return (!OrderId.HasValue && !DeliveryId.HasValue); }
        }

    }
}