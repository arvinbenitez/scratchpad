using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;

namespace AguaDeMaria.Service
{
    public interface IInventoryLedgerService
    {
        void PostToLedger(DeliveryReceipt deliveryReceipt);
        void PostToLedger(PickupSlip pickupSlip);

        void SetBeginningBalance(InventoryLedgerDto inventoryLedgerDto);
        InventoryLedgerDto GetBeginningBalance(int customerId);
    }
}
