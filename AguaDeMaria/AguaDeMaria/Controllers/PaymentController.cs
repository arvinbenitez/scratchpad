using System;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model.Dto;
using AguaDeMaria.Models.Payment;
using AguaDeMaria.Service;

namespace AguaDeMaria.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService paymentService;
        private readonly SettingsManager settingsManager;
        private readonly IDeliveryReceiptService deliveryReceiptService;

        public PaymentController(IPaymentService paymentService, SettingsManager settingsManager,
            IDeliveryReceiptService deliveryReceiptService)
        {
            this.paymentService = paymentService;
            this.settingsManager = settingsManager;
            this.deliveryReceiptService = deliveryReceiptService;
        }

        public ActionResult Save(PaymentDto paymentDto)
        {
            if (ModelState.IsValid && paymentDto.IsValid)
            {
                paymentDto.SalesInvoiceId = paymentService.Pay(paymentDto);
                return Json(paymentDto);
            }
            return PartialView("PaymentEditor", paymentDto);
        }

        public ActionResult PaymentEditor(PaymentParameter parameter)
        {
            var paymentDto = new PaymentDto();
            if (parameter.IsNew)
            {
                if (parameter.DeliveryReceiptId != null)
                {
                    var deliveryReceipt = deliveryReceiptService.Get(parameter.DeliveryReceiptId.Value);
                    paymentDto.CustomerId = deliveryReceipt.CustomerId;
                    paymentDto.CustomerName = deliveryReceipt.Customer.CustomerName;
                    paymentDto.InvoiceNumber = settingsManager.GetNextInvoiceNumber();
                    paymentDto.InvoiceDate = DateTime.Now;
                }
            }

            return PartialView(paymentDto);
        }

        public JsonResult ReceivableList(int customerId)
        {
            return Json(paymentService.GetReceivables(customerId), JsonRequestBehavior.AllowGet);
        }
    }
}