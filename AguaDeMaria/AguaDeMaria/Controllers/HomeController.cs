using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AguaDeMaria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Purified Drinking Water";

            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Aqua Kesh ";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Marialyn Benitez";

            return View();
        }
    }
}
