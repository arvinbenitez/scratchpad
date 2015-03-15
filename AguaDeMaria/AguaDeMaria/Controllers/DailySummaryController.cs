using System.Web.Mvc;
using AguaDeMaria.Service;

namespace AguaDeMaria.Controllers
{
    public class DailySummaryController : Controller
    {
        private readonly IReportService reportService;

        public DailySummaryController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public JsonResult List()
        {
            var summary = reportService.GetDailySummary();
            return Json(summary, JsonRequestBehavior.AllowGet);
        }
    }
}
