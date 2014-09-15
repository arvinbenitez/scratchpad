using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaDeMaria.Model
{
    [Table("OrderStatus")]
    public partial class OrderStatus
    {
        public int OrderStatusId { get; set; }

        public string StatusName { get; set; }
    }
}
