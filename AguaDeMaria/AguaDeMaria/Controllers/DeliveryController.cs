using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AguaDeMaria.Models.Delivery;

namespace AguaDeMaria.Controllers
{
    public class DeliveryController : Controller
    {
        public ActionResult DeliveryEditor(DeliveryParameter parameter)
        {
            DeliveryDto dto = null;
            return PartialView(dto);
        }
    }
}
