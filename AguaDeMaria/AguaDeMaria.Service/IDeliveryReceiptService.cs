using System;
using System.Collections.Generic;
using AguaDeMaria.Model;

namespace AguaDeMaria.Service
{
    public interface IDeliveryReceiptService
    {
        DeliveryReceipt GetByOrderId(int orderId);
        DeliveryReceipt Get(int deliveryReceiptId);
        IEnumerable<DeliveryReceipt> DeliveryReceipts(DateTime drStartDate, DateTime drEndDate);
        void Update(DeliveryReceipt deliveryReceipt);
        void Insert(DeliveryReceipt deliveryReceipt);
    }
}