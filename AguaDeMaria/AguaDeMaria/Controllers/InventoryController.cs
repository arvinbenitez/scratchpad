using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Filters;
using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;
using AguaDeMaria.Service;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IInventoryLedgerService inventoryLedgerService;

        public InventoryController(IRepository<Inventory> inventoryRepository,
            IRepository<InventorySummary> inventorySummaryRepository,
            IInventoryLedgerService inventoryLedgerService)
        {
            this.inventoryLedgerService = inventoryLedgerService;
            InventoryRepository = inventoryRepository;
            InventorySummaryRepository = inventorySummaryRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetInventorySummary()
        {
            var inventorySummary = InventorySummaryRepository.Get(x => true).OrderBy(x=> x.CustomerName); // get everything
            return Json(inventorySummary, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InventoryDetail(int customerId)
        {
            var summary = InventorySummaryRepository.Get(x => x.CustomerId == customerId).FirstOrDefault();
            return PartialView(summary);
        }


        [HttpGet]
        public ActionResult BeginningBalanceEditor(int customerId)
        {
            var inventoryLedger = inventoryLedgerService.GetBeginningBalance(customerId);
            return PartialView(inventoryLedger);
        }

        [HttpPost]
        [ConvertDatesToUtc]
        public ActionResult SaveAdjustment(InventoryLedgerDto inventoryLedger)
        {
            inventoryLedgerService.SetBeginningBalance(inventoryLedger);
            var customerInventoryDetail =
                InventorySummaryRepository.Get(x => x.CustomerId == inventoryLedger.CustomerId).FirstOrDefault();
            return Json(customerInventoryDetail);
        }

        public JsonResult GetInventoryDetail(int? customerId)
        {
            var result = InventoryRepository.Get(x => x.CustomerId == customerId, x => x.OrderBy(y => y.RefDate)) ??
                         new List<Inventory>();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private IRepository<Inventory> InventoryRepository { get; set; }
        private IRepository<InventorySummary> InventorySummaryRepository { get; set; }
    }
}

