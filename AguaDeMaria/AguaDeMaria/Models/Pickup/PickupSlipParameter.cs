namespace AguaDeMaria.Models.Pickup
{
    public class PickupSlipParameter
    {
        public int? CustomerId { get; set; }
        public int? PickupSlipId { get; set; }

        public int? DeliveryReceiptId { get; set; }

        public int? OrderId { get; set; }

        public bool IsNewPickup()
        {
            return (!PickupSlipId.HasValue || PickupSlipId.Value <= 0);
        }
    }
}