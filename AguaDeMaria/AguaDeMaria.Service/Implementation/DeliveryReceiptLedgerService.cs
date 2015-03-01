using System.Linq;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;

namespace AguaDeMaria.Service.Implementation
{
    public class DeliveryReceiptLedgerService : IDeliveryReceiptLedgerService
    {
        private readonly IRepository<DeliveryReceiptLedger> ledgeRepository;

        public DeliveryReceiptLedgerService(IRepository<DeliveryReceiptLedger> ledgeRepository)
        {
            this.ledgeRepository = ledgeRepository;
        }

        public void RecordToLedger(int deliveryReceiptId, decimal amount)
        {
            var ledgerEntry = GetByDeliveryReceipt(deliveryReceiptId) ?? new DeliveryReceiptLedger();
            ledgerEntry.Amount = amount;
            ledgerEntry.DeliveryReceiptId = deliveryReceiptId;
            if (ledgerEntry.IsNew())
            {
                ledgeRepository.Insert(ledgerEntry);
            }
            else
            {
                ledgeRepository.Update(ledgerEntry);
            }
        }

        public DeliveryReceiptLedger GetByDeliveryReceipt(int deliveryReceiptId)
        {
            return ledgeRepository.Get(x => x.DeliveryReceiptId == deliveryReceiptId).FirstOrDefault();
        }

        public void Insert(DeliveryReceiptLedger ledgerEntry)
        {
            ledgeRepository.Insert(ledgerEntry);
        }

        public void Update(DeliveryReceiptLedger ledgerEntry)
        {
            ledgeRepository.Update(ledgerEntry);
        }
    }
}