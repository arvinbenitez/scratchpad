using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Filters;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Order;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        public OrderController(IRepository<Order> orderRepository, IRepository<Customer> customerRepository)
        {
            OrderRepository = orderRepository;
            CustromeRepository = customerRepository;
        }

        private IRepository<Order> OrderRepository { get; set; }

        private IRepository<Customer> CustromeRepository { get; set; }

        //
        // GET: /Order/
        public ActionResult Index(DateTime? orderDate)
        {
            return View();
        }

        public ActionResult GetOrderList(DateTime? orderDate)
        {
            DateTime startDate, endDate;
            if (!orderDate.HasValue)
            {
                startDate = DateTime.Today;
            }
            else
            {
                startDate = orderDate.Value.Date;
            }
            endDate = startDate.AddDays(1);
            //Get the orders for Today
            var orders = OrderRepository.Get(
                x => x.OrderDate >= startDate && x.OrderDate <= endDate,
                orderBy: x => x.OrderBy(y => y.OrderDate),
                includedProperties: "Customer");
            var ordersList = from o in orders
                             select OrderDto.TranslateFrom(o);
            return Json(ordersList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult OrderEditor(OrderParameter parameter)
        {
            Order order;
            if (parameter.IsEdit())
            {
                order =
                    OrderRepository.Get(x => x.OrderId == parameter.OrderId, includedProperties: "OrderDetails")
                        .FirstOrDefault();
            }
            else
            {
                order = CreateNewOrder(parameter);

            }
            ViewBag.CustomerList = CustomerListItems();
            OrderDto orderDto = OrderDto.TranslateFrom(order);

            return PartialView(orderDto);
        }

        private static Order CreateNewOrder(OrderParameter parameter)
        {
            Order order = new Order
            {
                CustomerId = parameter.CustomerId,
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetail>
                                    {
                                        new OrderDetail(){ProductTypeId = DataConstants.ProductTypes.Slim},
                                        new OrderDetail(){ProductTypeId = DataConstants.ProductTypes.Round}
                                    }
            };
            return order;
        }

        [ExcludeIdValidation(IdField = "OrderId")]
        [HttpPost]
        public ActionResult SaveOrder(OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                Order order = OrderDto.TranslateFrom(orderDto);
                if (order.OrderId > 0)
                {
                    this.OrderRepository.Update(order);
                }
                else
                {
                    this.OrderRepository.Insert(order);
                }
                this.OrderRepository.Commit();
                orderDto.OrderId = order.OrderId;
                return Json(orderDto);
            }
            ViewBag.CustomerList = CustomerListItems();
            return PartialView("OrderEditor", orderDto);
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