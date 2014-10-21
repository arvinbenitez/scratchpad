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
using AguaDeMaria.Models;
using AguaDeMaria.Model.Dto;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private IRepository<DeliveryReceipt> DeliveryRepository { get; set; }
        private IRepository<InventorySummary> InventoryRepository { get; set; }

        public ReportController(IRepository<DeliveryReceipt> deliveryRepository,
                                IRepository<InventorySummary> inventoryRepository)
        {
            DeliveryRepository = deliveryRepository;
            InventoryRepository = inventoryRepository;
        }

        public ActionResult DeliveryReceipt(int deliveryReceiptId)
        {
            var deliveryReceipt = DeliveryRepository.Get(x => x.DeliveryReceiptId == deliveryReceiptId).FirstOrDefault();
            var inventorySummary = InventoryRepository.Get(x => x.CustomerId == deliveryReceipt.CustomerId).FirstOrDefault();
            var deliveryDto = Mapper.Map<DeliveryDto>(deliveryReceipt);
            byte[] buffer;
            using (DeliveryReceiptPdf report = new DeliveryReceiptPdf(deliveryDto, inventorySummary, Server.MapPath(@"~\Images\DeliveryReceipt.png")))
            {
                buffer = report.GenerateContent();
            }
            return new BinaryContentResult(buffer, "application/pdf");
        }
    }
}
