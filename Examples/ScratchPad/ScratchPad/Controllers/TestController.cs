using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScratchPad.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostTheFile(string filename)
        {
            ViewBag.FileName = filename;
            System.Threading.Thread.Sleep(2000);
            return PartialView("UploadFile");
        }

        [HttpPost]
        public ActionResult PostTheFile2(string filename)
        {
            var sleepTime = (new Random(500)).Next(5000);
            System.Threading.Thread.Sleep(sleepTime);
            ViewBag.FileName = string.Format("Next call to {0} took {1} ms", filename, sleepTime);

            return PartialView("UploadFile");
        }
	}
}