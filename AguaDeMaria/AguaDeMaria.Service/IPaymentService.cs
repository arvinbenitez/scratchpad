using System;
using System.Collections.Generic;
using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;

namespace AguaDeMaria.Service
{
    public interface IPaymentService
    {
        IEnumerable<Receivable> GetReceivables(int? customerId);
        int Pay(PaymentDto payment);
        IEnumerable<SalesInvoice> GetInvoices(DateTime startDate, DateTime endDate);
        SalesInvoice GetByDeliveryReceipt(int deliveryReceiptId);
        IEnumerable<Receivable> DeliveryReceiptList(int salesInvoiceId);
    }
}