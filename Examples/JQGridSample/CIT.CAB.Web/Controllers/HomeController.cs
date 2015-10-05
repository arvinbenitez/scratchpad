using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIT.CAB.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the CAB eBlotter. ";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Browser-based, lightweight version of the CAB blotter.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "CAB Development Team Contact";

            return View();
        }
    }
}
