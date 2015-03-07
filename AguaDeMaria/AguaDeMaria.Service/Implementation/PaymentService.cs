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

        public IEnumerable<Receivable> GetReceivables(int customerId)
        {
            return receivableRepository.Get(x => x.CustomerId == customerId);
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
    }
}
