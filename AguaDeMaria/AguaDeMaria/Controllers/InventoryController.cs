using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Filters;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Pickup;
using AutoMapper;
using Microsoft.Ajax.Utilities;

namespace AguaDeMaria.Controllers
{
    public class InventoryController : Controller
    {

        public InventoryController(IRepository<Inventory> inventoryRepository)
        {
            InventoryRepository = inventoryRepository;
        }

        //
        // GET: /Inventory/

        public ActionResult Index()
        {
            return View();
        }

        private IRepository<Inventory> InventoryRepository { get; set; }
    }
}
