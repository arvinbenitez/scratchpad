using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("OrderStatus")]
    public partial class OrderStatus
    {
        public int OrderStatusId { get; set; }

        public string StatusName { get; set; }
    }
}
