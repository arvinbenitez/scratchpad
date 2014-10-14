using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Filters;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Pickup;
using AutoMapper;
using Microsoft.Ajax.Utilities;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {

        public InventoryController(IRepository<Inventory> inventoryRepository,
            IRepository<InventorySummary> inventorySummaryRepository)
        {
            InventoryRepository = inventoryRepository;
            InventorySummaryRepository = inventorySummaryRepository;
        }

        //
        // GET: /Inventory/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetInventorySummary()
        {
            var inventorySummary = InventorySummaryRepository.Get(x => true); // get everything
            return Json(inventorySummary, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InventoryDetail(int customerId)
        {
            var summary = InventorySummaryRepository.Get(x => x.CustomerId == customerId).FirstOrDefault();
            return PartialView(summary);
        }

        public JsonResult GetInventoryDetail(int? customerId)
        {
            var result = InventoryRepository.Get(x => x.CustomerId == customerId, x => x.OrderBy(y => y.RefDate));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private IRepository<Inventory> InventoryRepository { get; set; }
        private IRepository<InventorySummary> InventorySummaryRepository { get; set; }
    }
}

