﻿using System.Web.Mvc;

namespace AguaDeMaria.Areas.Accounts.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Accounts/Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Manage()
        {
            throw new System.NotImplementedException();
        }
    }
}
