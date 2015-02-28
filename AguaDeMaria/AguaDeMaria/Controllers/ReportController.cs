using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;
using AutoMapper;
using AguaDeMaria.Controllers.Helpers;
using AguaDeMaria.Report;
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

        public ActionResult Index()
        {
            throw new System.NotImplementedException();
        }
    }
}
