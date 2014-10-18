using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Filters;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Delivery;
using AutoMapper;
using AguaDeMaria.Controllers.Helpers;
using AguaDeMaria.Report;
using System.IO;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private IRepository<DeliveryReceipt> DeliveryRepository { get; set; }

        public ReportController(IRepository<DeliveryReceipt> deliveryRepository)
        {
            DeliveryRepository = deliveryRepository;
        }

        public ActionResult DeliveryReceipt(int deliveryReceiptId)
        {
            var deliveryReceipt = DeliveryRepository.Get(x => x.DeliveryReceiptId == deliveryReceiptId).FirstOrDefault();
            byte[] buffer;
            using (DeliveryReceiptPdf report = new DeliveryReceiptPdf(deliveryReceipt, Server.MapPath(@"~\Images\Water.png")))
            {
                buffer = report.GenerateContent();
            }
            return new BinaryContentResult(buffer, "application/pdf");
        }
    }
}
