using System;
using System.Collections.Generic;
using System.Linq;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;

namespace AguaDeMaria.Service.Implementation
{
    public class DeliveryReceiptService : IDeliveryReceiptService
    {
        private readonly IDeliveryReceiptLedgerService ledgerService;
        private readonly IRepository<DeliveryReceipt> deliveryRepository;

        public DeliveryReceiptService(IRepository<DeliveryReceipt> deliveryRepository, IDeliveryReceiptLedgerService ledgerService)
        {
            this.deliveryRepository = deliveryRepository;
            this.ledgerService = ledgerService;
        }

        public DeliveryReceipt GetByOrderId(int orderId)
        {
            return deliveryRepository.Get(x => x.OrderId == orderId).FirstOrDefault();
        }

        public DeliveryReceipt Get(int deliveryReceiptId)
        {
            return deliveryRepository.Get(x => x.DeliveryReceiptId == deliveryReceiptId).FirstOrDefault();
        }

        public IEnumerable<DeliveryReceipt> DeliveryReceipts(DateTime drStartDate, DateTime drEndDate)
        {
            var deliveryReceipts = deliveryRepository.Get(x => x.DRDate >= drStartDate && x.DRDate <= drEndDate,
                x => x.OrderBy(y => y.DRNumber));
            return deliveryReceipts.ToList();

        }

        public void Update(DeliveryReceipt deliveryReceipt)
        {
            deliveryRepository.Update(deliveryReceipt);
            RecordToLedger(deliveryReceipt);
        }

        public void Insert(DeliveryReceipt deliveryReceipt)
        {
            deliveryRepository.Insert(deliveryReceipt);
            RecordToLedger(deliveryReceipt);
        }

        private void RecordToLedger(DeliveryReceipt deliveryReceipt)
        {
            var amount = deliveryReceipt.DeliveryReceiptDetails.Sum(x => x.UnitPrice*x.Quantity);
            ledgerService.RecordToLedger(deliveryReceipt.DeliveryReceiptId,amount);
        }
    }
}
