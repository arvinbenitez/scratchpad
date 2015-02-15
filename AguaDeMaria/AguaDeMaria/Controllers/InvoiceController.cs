using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;
using AguaDeMaria.Models;
using AguaDeMaria.Models.Invoice;
using AutoMapper;

namespace AguaDeMaria.Controllers
{
    public class InvoiceController : Controller
    {
        private IRepository<SalesInvoice> InvoiceRepository{get;set;}
        public InvoiceController(IRepository<SalesInvoice> invoiceRepository)
        {
            InvoiceRepository = invoiceRepository;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetInvoicesList(DateTime startDate, DateTime endDate)
        {
            var invoices = InvoiceRepository.Get(x => x.InvoiceDate >= startDate && x.InvoiceDate <= endDate, x => x.OrderBy(y => y.InvoiceNumber));

            var list = from inv in invoices
                         select Mapper.Map<SalesInvoiceDto>(inv);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
