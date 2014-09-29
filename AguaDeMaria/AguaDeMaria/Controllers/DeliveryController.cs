using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Delivery;
using AutoMapper;

namespace AguaDeMaria.Controllers
{
    public class DeliveryController : Controller
    {
        private IRepository<Order> OrderRepository { get; set; }

        private IRepository<Customer> CustromeRepository { get; set; }

        private LookupDataManager LookupDataManager { get; set; }

        private SettingsManager SettingsManager { get; set; }

        private IRepository<DeliveryReceipt> DeliveryRepository { get; set; }

        public DeliveryController(IRepository<Order> orderRepository,
                            IRepository<Customer> customerRepository,
                            LookupDataManager manager,
                            SettingsManager settingsManager,
                            IRepository<DeliveryReceipt> deliveryRepository
            )
        {
            OrderRepository = orderRepository;
            CustromeRepository = customerRepository;
            LookupDataManager = manager;
            SettingsManager = settingsManager;
            DeliveryRepository = deliveryRepository;
        }
        public ActionResult DeliveryEditor(DeliveryParameter parameter)
        {
            DeliveryDto deliveryDto = null;
            if (parameter.IsNewDeliveryFromOrder)
            {
                //this must be a new delivery
                Order order = OrderRepository.Get(x => x.OrderId == parameter.OrderId).FirstOrDefault();
                deliveryDto = Mapper.Map<DeliveryDto>(order);
                deliveryDto.DRDate = DateTime.Now;
                deliveryDto.DRNumber = SettingsManager.GetNextDeliveryReceiptNumber();
            }
            ViewBag.CustomerList = CustomerListItems();
            return PartialView(deliveryDto);
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
