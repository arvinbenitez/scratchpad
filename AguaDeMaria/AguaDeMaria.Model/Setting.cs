using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaDeMaria.Model
{
    [Table("Settings")]
    public class Setting
    {
        public int SettingId { get; set; }

        public int OrderNumberCounter { get; set; }

        public int DeliveryReceiptNumberCounter { get; set; }

        public int PickupSlipNumberCounter { get; set; }

    }
}
