using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaDeMaria.Model
{
    public static class DataConstants
    {
        public static class ProductTypes
        {
            public const int Undefined = 0;
            public const int Slim = 1;
            public const int Round = 2;
        }

        public static class OrderStatus
        {
            public const int Pending = 1;
            public const int Delivered = 2;
            public const int Cancelled = 3;
            public const int PartiallyDelivered = 4;
        }
    }
}
