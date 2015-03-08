using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Service;

namespace AguaDeMaria.Controllers
{
    public class ReceivableController : Controller
    {
        private readonly IPaymentService paymentService;

        public ReceivableController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public JsonResult List()
        {
            var receivables = from r in paymentService.GetReceivables(null).ToList()
                select new
                {
                    r.CustomerId,
                    r.CustomerName,
                    r.DrNumber,
                    r.DrDate,
                    r.PaidAmount,
                    r.Amount,
                    r.ReceivableAmount,
                    r.DeliveryReceiptId
                };
            return Json(receivables, JsonRequestBehavior.AllowGet);
        }

    }
}
