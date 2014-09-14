using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Order;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        public OrderController(IRepository<Order> ordeRepository, IRepository<Customer> customerRepository)
        {
            OrdeRepository = ordeRepository;
            CustromeRepository = customerRepository;
        }

        private IRepository<Order> OrdeRepository { get; set; }

        private IRepository<Customer> CustromeRepository { get; set; }

        //
        // GET: /Order/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult OrderEditor(OrderParameter parameter)
        {
            Order order;
            if (parameter.IsEdit())
            {
                order =
                    OrdeRepository.Get(x => x.OrderId == parameter.OrderId, includedProperties: "OrderDetails")
                        .FirstOrDefault();
            }
            else
            {
                order = new Order {CustomerId = parameter.CustomerId, OrderDate = DateTime.Today};

            }
            ViewBag.CustomerList = CustomerListItems();

            return PartialView(order);
        }

        [HttpPost]
        public ActionResult SaveOrder(Order order)
        {
            return Json(order);
        }

        private IEnumerable<SelectListItem> CustomerListItems()
        {
            IEnumerable<SelectListItem> customerList =
                CustromeRepository.Get(c => c.CustomerId > 0)
                    .OrderBy(cust => cust.CustomerName)
                    .Select(cust => new SelectListItem
                    {
                        Text = cust.CustomerName,
                        Value = cust.CustomerId.ToString(CultureInfo.InvariantCulture)
                    });
            return customerList;
        }
    }
}