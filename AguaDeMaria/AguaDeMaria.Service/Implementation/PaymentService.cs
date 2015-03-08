using System;
using System.Collections.Generic;
using System.Linq;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;

namespace AguaDeMaria.Service.Implementation
{
    public class PaymentService: IPaymentService
    {
        private readonly IRepository<Receivable> receivableRepository;
        private readonly IRepository<SalesInvoiceDetail> invoiceDetailsRepository;
        private readonly IRepository<SalesInvoice> salesInvoiceRepository;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IRepository<Receivable> receivableRepository,
            IRepository<SalesInvoiceDetail> invoiceDetailsRepository,
            IRepository<SalesInvoice> salesInvoiceRepository,
            IUnitOfWork unitOfWork)
        {
            this.receivableRepository = receivableRepository;
            this.invoiceDetailsRepository = invoiceDetailsRepository;
            this.salesInvoiceRepository = salesInvoiceRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Receivable> GetReceivables(int? customerId)
        {
            if (customerId.HasValue)
            {
                return receivableRepository.Get(x => x.CustomerId == customerId);
            }
            return receivableRepository.Get(x => x.DeliveryReceiptId > 0,includedProperties:"Customer");
        }

        public int Pay(PaymentDto payment)
        {
            //if salesInvoiceId > 0 then delete drs associated with the invoice and reapply
            var invoice =
                salesInvoiceRepository.Get(x => x.SalesInvoiceId == payment.SalesInvoiceId,includedProperties:"SalesInvoiceDetails").FirstOrDefault() ??
                new SalesInvoice();

            invoice.CustomerId = payment.CustomerId;
            invoice.InvoiceDate = payment.InvoiceDate;
            invoice.InvoiceNumber = payment.InvoiceNumber;
            invoice.Amount = payment.Amount;

            //let's delete anything on this invoice, before reapplying the amount
            invoice.SalesInvoiceDetails.ToList().ForEach(x=> invoiceDetailsRepository.Delete(x));
            unitOfWork.Commit();

            var receivables = receivableRepository.Get(x => x.CustomerId == payment.CustomerId)
                .OrderBy(x => x.DeliveryReceiptId).ToList();
            var paymentAmount = payment.Amount;
            for (int i = 0; i < receivables.Count; i++)
            {
                var salesInvoiceDetail = new SalesInvoiceDetail();
                                var receivable = receivables[i];


                salesInvoiceDetail.DeliveryReceiptLedgerId = receivable.DeliveryReceiptLedgerId;
                if (receivable.ReceivableAmount <= paymentAmount)
                {
                    salesInvoiceDetail.Amount = receivable.ReceivableAmount;
                    paymentAmount = paymentAmount - receivable.ReceivableAmount;
                }
                else
                {
                    salesInvoiceDetail.Amount = paymentAmount;
                    paymentAmount = 0;
                }
                salesInvoiceDetail.SalesInvoice = invoice;
                invoice.SalesInvoiceDetails.Add(salesInvoiceDetail);
                if (paymentAmount <= 0)
                {
                    break;
                }
            }
            if (invoice.IsNew)
            {
                salesInvoiceRepository.Insert(invoice);
            }
            else
            {
                salesInvoiceRepository.Update(invoice);
            }
            unitOfWork.Commit();
            return invoice.SalesInvoiceId;
        }

        public IEnumerable<SalesInvoice> GetInvoices(DateTime startDate, DateTime endDate)
        {
            var invoices = salesInvoiceRepository.Get(x => x.InvoiceDate >= startDate && x.InvoiceDate <= endDate,
                includedProperties: "Customer,SalesInvoiceDetails");
            return invoices;
        }

        public SalesInvoice GetByDeliveryReceipt(int deliveryReceiptId)
        {
            var ledger =
                invoiceDetailsRepository.Get(
                    x => x.DeliveryReceiptLedger != null &&
                         x.DeliveryReceiptLedger.DeliveryReceiptId == deliveryReceiptId,
                    z => z.OrderByDescending(inv => inv.SalesInvoiceDetailId),
                    "SalesInvoice").FirstOrDefault();
            if (ledger != null)
            {
                return ledger.SalesInvoice;
            }
            return null;
        }

        public IEnumerable<Receivable> DeliveryReceiptList(int salesInvoiceId)
        {
            var invoiceDetails = invoiceDetailsRepository.Get(x => x.SalesInvoiceId == salesInvoiceId,
                z => z.OrderBy(inv => inv.SalesInvoiceDetailId),
                "SalesInvoice,DeliveryReceiptLedger,DeliveryReceiptLedger.DeliveryReceipt");
            var receivables = from detail in invoiceDetails
                select new Receivable
                {
                    Amount = detail.DeliveryReceiptLedger.Amount,
                    CustomerId = detail.SalesInvoice.CustomerId,
                    DeliveryReceiptId = detail.DeliveryReceiptLedger.DeliveryReceiptId,
                    DeliveryReceiptLedgerId = detail.DeliveryReceiptLedgerId,
                    DrDate = detail.DeliveryReceiptLedger.DeliveryReceipt.DRDate,
                    DrNumber = detail.DeliveryReceiptLedger.DeliveryReceipt.DRNumber,
                    PaidAmount = detail.Amount
                };
            return receivables;
        }
    }
}
