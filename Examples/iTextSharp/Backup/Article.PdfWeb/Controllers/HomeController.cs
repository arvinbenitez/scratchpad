using System.IO;
using System.Web.Mvc;
using Article.Pdf;
using Article.PdfWeb.Models.Home;

namespace Article.PdfWeb.Controllers {


    public class HomeController : Controller {

        public ActionResult Index() {
            return View();
        }

        [HttpPost] 
        public FileContentResult Generate(GenerateModel model) {
            DiplomaPrinter printer = 
                new DiplomaPrinter(model.Name, model.Distance, model.Date, model.RaceName, model.ShowRulers);
            MemoryStream memoryStream = new MemoryStream();
            printer.Create(memoryStream);

            return File(memoryStream.GetBuffer(), "application/pdf", "diploma.pdf");
        }
    }


}