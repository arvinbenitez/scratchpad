namespace AguaDeMaria.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int ProductTypeId { get; set; }

        public int Qty { get; set; }

        public virtual Order Order { get; set; }

        public virtual ProductType ProductType { get; set; }
    }
}
