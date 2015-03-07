using System;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Invoice;
using AguaDeMaria.Service;
using AutoMapper;

namespace AguaDeMaria.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IPaymentService paymentService;

        public InvoiceController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetInvoicesList(DateTime startDate, DateTime endDate)
        {
            var invoices = paymentService.GetInvoices(startDate, endDate);

            var list = from inv in invoices
                         select Mapper.Map<SalesInvoiceDto>(inv);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
