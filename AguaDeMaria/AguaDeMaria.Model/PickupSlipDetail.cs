namespace AguaDeMaria.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
