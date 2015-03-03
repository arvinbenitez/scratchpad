using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("Settings")]
    public class Setting
    {
        public int SettingId { get; set; }

        public int OrderNumberCounter { get; set; }

        public int DeliveryReceiptNumberCounter { get; set; }

        public int PickupSlipNumberCounter { get; set; }
        public int InvoiceNumberCounter { get; set; }
    }
}
