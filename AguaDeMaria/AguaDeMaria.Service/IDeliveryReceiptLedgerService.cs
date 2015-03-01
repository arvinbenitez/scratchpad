using AguaDeMaria.Model;

namespace AguaDeMaria.Service
{
    public interface IDeliveryReceiptLedgerService
    {
        void RecordToLedger(int deliveryReceiptId, decimal amount);
        DeliveryReceiptLedger GetByDeliveryReceipt(int deliveryReceiptId);
        void Insert(DeliveryReceiptLedger ledgerEntry);
        void Update(DeliveryReceiptLedger ledgerEntry);
    }
}