using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AguaDeMaria.Models.Order
{
    public class OrderParameter
    {
        public int? CustomerId { get; set; }
        public int? OrderId { get; set; }

        public bool IsEdit()
        {
            return OrderId.HasValue;
        }
    }
}