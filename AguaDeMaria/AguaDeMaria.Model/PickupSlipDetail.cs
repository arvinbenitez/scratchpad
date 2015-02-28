namespace AguaDeMaria.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PickupSlipDetail")]
    public class PickupSlipDetail
    {
        public int PickupSlipDetailId { get; set; }

        public int PickupSlipId { get; set; }

        public int ProductTypeId { get; set; }

        public int Quantity { get; set; }

        public virtual PickupSlip PickupSlip { get; set; }

        public virtual ProductType ProductType { get; set; }

    }
}
