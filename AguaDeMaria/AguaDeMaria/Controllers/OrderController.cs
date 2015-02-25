using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Filters;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Order;
using AutoMapper;
using Microsoft.Ajax.Utilities;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        public OrderController(IRepository<Order> orderRepository, 
                            IRepository<Customer> customerRepository, 
                            LookupDataManager manager,
                            SettingsManager settingsManager)
        {
            OrderRepository = orderRepository;
            CustromeRepository = customerRepository;
            LookupDataManager = manager;
            SettingsManager = settingsManager;
        }

        private IRepository<Order> OrderRepository { get; set; }

        private IRepository<Customer> CustromeRepository { get; set; }

        private LookupDataManager LookupDataManager { get; set; }

        private SettingsManager SettingsManager { get; set; }

        //
        // GET: /Order/
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            return View();
        }

        public ActionResult GetOrderList(DateTime? startDate, DateTime? endDate)
        {
            DateTime orderStartDate = startDate.HasValue? startDate.Value.Date: DateTime.Today;
            DateTime orderEndDate = endDate.HasValue ? endDate.Value.Date.AddDays(1) : orderStartDate.AddDays(1);
            //Get the orders for Today
            var orders = OrderRepository.Get(
                x => x.OrderDate >= orderStartDate && x.OrderDate <= orderEndDate,
                orderBy: x => x.OrderBy(y => y.OrderDate),
                includedProperties: "Customer,DeliveryReceipts,DeliveryReceipts.DeliveryReceiptDetails");
            var ordersList = from o in orders
                             select Mapper.Map<OrderDto>(o);
            return Json(ordersList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult OrderEditor(OrderParameter parameter)
        {
            Order order;
            if (parameter.IsEdit())
            {
                order =
                    OrderRepository.Get(x => x.OrderId == parameter.OrderId, includedProperties: "OrderDetails,OrderStatus")
                        .FirstOrDefault();
            }
            else
            {
                order = CreateNewOrder(parameter);

            }
            ViewBag.CustomerList = CustomerListItems();
            ViewBag.OrderStatusList = OrderStatusListItems();
            OrderDto orderDto = Mapper.Map<OrderDto>(order);

            return PartialView(orderDto);
        }


        private Order CreateNewOrder(OrderParameter parameter)
        {
            Order order = new Order
            {
                CustomerId = parameter.CustomerId.HasValue ? parameter.CustomerId.Value : 0,
                OrderDate = DateTime.Now,
                OrderStatusId = DataConstants.OrderStatus.Pending,
                OrderDetails = new List<OrderDetail>
                                    {
                                        new OrderDetail(){ProductTypeId = DataConstants.ProductTypes.Slim},
                                        new OrderDetail(){ProductTypeId = DataConstants.ProductTypes.Round}
                                    }
            };
            order.OrderNumber = this.SettingsManager.GetNextOrderNumber();
            return order;
        }

        [ExcludeIdValidation(IdField = "OrderId")]
        [HttpPost]
        public ActionResult SaveOrder(OrderDto orderDto)
        {
            if (ModelState.IsValid && orderDto.IsValid)
            {
                Order order = null;
                if (orderDto.OrderId > 0)
                {
                    order = this.OrderRepository.Get(x => x.OrderId == orderDto.OrderId).FirstOrDefault();
                    Mapper.Map(orderDto, order);
                    this.OrderRepository.Update(order);
                }
                else
                {
                    order = Mapper.Map<Model.Order>(orderDto);
                    this.OrderRepository.Insert(order);
                }
                this.OrderRepository.Commit();

                //TODO: Avoid looking this up again
                orderDto.CustomerName =
                    this.CustromeRepository.Get(x => x.CustomerId == orderDto.CustomerId).First().CustomerName;
                orderDto.OrderStatusName =
                    this.LookupDataManager.OrderStatuses.First(x => x.OrderStatusId == orderDto.OrderStatusId)
                        .StatusName;

                orderDto.OrderId = order.OrderId;
                return Json(orderDto);
            }
            ViewBag.CustomerList = CustomerListItems();
            ViewBag.OrderStatusList = OrderStatusListItems();
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


        private IEnumerable<SelectListItem> OrderStatusListItems()
        {
            var orderStatus = from c in LookupDataManager.OrderStatuses
                              select new SelectListItem()
                              {
                                  Value = c.OrderStatusId.ToString(),
                                  Text = c.StatusName
                              };
            return orderStatus.ToList();
        }
    }
}