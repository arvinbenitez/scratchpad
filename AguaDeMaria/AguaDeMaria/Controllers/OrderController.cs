using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Filters;
using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;
using AguaDeMaria.Models.Order;
using AutoMapper;

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
            DateTime orderStartDate = startDate.HasValue? startDate.Value: DateTime.Today;
            DateTime orderEndDate = endDate.HasValue ? endDate.Value : orderStartDate.AddDays(1);
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
                OrderDate = DateTime.UtcNow,
                OrderStatusId = DataConstants.OrderStatus.Pending,
                OrderDetails = new List<OrderDetail>
                                    {
                                        new OrderDetail {ProductTypeId = DataConstants.ProductTypes.Slim},
                                        new OrderDetail {ProductTypeId = DataConstants.ProductTypes.Round}
                                    }
            };
            order.OrderNumber = SettingsManager.GetNextOrderNumber();
            return order;
        }

        [ExcludeIdValidation(IdField = "OrderId")]
        [HttpPost]
        [ConvertDatesToUtc]
        public ActionResult SaveOrder(OrderDto orderDto)
        {
            if (ModelState.IsValid && orderDto.IsValid)
            {
                Order order;
                if (orderDto.OrderId > 0)
                {
                    order = OrderRepository.Get(x => x.OrderId == orderDto.OrderId).FirstOrDefault();
                    Mapper.Map(orderDto, order);
                    OrderRepository.Update(order);
                }
                else
                {
                    order = Mapper.Map<Order>(orderDto);
                    OrderRepository.Insert(order);
                }
                OrderRepository.Commit();

                //TODO: Avoid looking this up again
                orderDto.CustomerName =
                    CustromeRepository.Get(x => x.CustomerId == orderDto.CustomerId).First().CustomerName;
                orderDto.OrderStatusName =
                    LookupDataManager.OrderStatuses.First(x => order != null && x.OrderStatusId == order.OrderStatusId)
                        .StatusName;

                if (order != null) orderDto.OrderId = order.OrderId;
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
                              select new SelectListItem
                              {
                                  Value = c.OrderStatusId.ToString(CultureInfo.InvariantCulture),
                                  Text = c.StatusName
                              };
            return orderStatus.ToList();
        }
    }
}