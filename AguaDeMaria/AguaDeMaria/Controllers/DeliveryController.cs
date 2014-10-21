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
using AguaDeMaria.Models.Delivery;
using AutoMapper;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    public class DeliveryController : Controller
    {
        private IRepository<Order> OrderRepository { get; set; }

        private IRepository<Customer> CustromeRepository { get; set; }

        private LookupDataManager LookupDataManager { get; set; }

        private SettingsManager SettingsManager { get; set; }

        private IRepository<DeliveryReceipt> DeliveryRepository { get; set; }

        private IUnitOfWork UnitOfWork { get; set; }

        public DeliveryController(IRepository<Order> orderRepository,
                            IRepository<Customer> customerRepository,
                            LookupDataManager manager,
                            SettingsManager settingsManager,
                            IRepository<DeliveryReceipt> deliveryRepository,
                            IUnitOfWork unitOfWork
            )
        {
            OrderRepository = orderRepository;
            CustromeRepository = customerRepository;
            LookupDataManager = manager;
            SettingsManager = settingsManager;
            DeliveryRepository = deliveryRepository;
            UnitOfWork = unitOfWork;
        }

        public ActionResult Index(DateTime? drDate)
        {
            return View();
        }

        public ActionResult GetDeliveryReceiptList(DateTime? startDate, DateTime? endDate)
        {
            DateTime drStartDate = startDate.HasValue ? startDate.Value : DateTime.Today;
            DateTime drEndDate = endDate.HasValue ? endDate.Value : drStartDate.AddDays(1);

            var deliveryReceipts = DeliveryRepository.Get(x => x.DRDate >= drStartDate && x.DRDate <= drEndDate,
                x => x.OrderBy(y => y.DRNumber));

            var drList = from dr in deliveryReceipts
                select Mapper.Map<DeliveryDto>(dr);
            return Json(drList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeliveryEditor(DeliveryParameter parameter)
        {
            DeliveryDto deliveryDto = null;
            if (parameter.IsNewDeliveryFromOrder || parameter.IsNewDelivery)
            {
                if (parameter.IsNewDeliveryFromOrder)
                {
                    //this must be a new delivery
                    Order order = OrderRepository.Get(x => x.OrderId == parameter.OrderId).FirstOrDefault();
                    deliveryDto = Mapper.Map<DeliveryDto>(order);
                }
                else
                {
                    deliveryDto = new DeliveryDto();
                    deliveryDto.OrderNumber = string.Format("{0:0000000000}", 0);
                }
                AssignDefaultPrice(deliveryDto);
                deliveryDto.DRDate = DateTime.Now;
                deliveryDto.DRNumber = SettingsManager.GetNextDeliveryReceiptNumber();
            }
            else
            {
                var delivery = DeliveryRepository.Get(x => x.DeliveryReceiptId == parameter.DeliveryId).FirstOrDefault();
                deliveryDto = Mapper.Map<DeliveryDto>(delivery);
            }
            ViewBag.CustomerList = CustomerListItems();
            return PartialView(deliveryDto);
        }

        private void AssignDefaultPrice(DeliveryDto deliveryDto)
        {
            deliveryDto.SlimUnitPrice =
                LookupDataManager.ProductTypes.First(x => x.ProductTypeId == DataConstants.ProductTypes.Slim).BasePrice;
            deliveryDto.SlimAmount = deliveryDto.SlimUnitPrice * deliveryDto.SlimQty;
            deliveryDto.RoundUnitPrice =
                LookupDataManager.ProductTypes.First(x => x.ProductTypeId == DataConstants.ProductTypes.Round).BasePrice;
            deliveryDto.RoundAmount = deliveryDto.RoundUnitPrice * deliveryDto.RoundQty;
        }

        [ExcludeIdValidation(IdField = "DeliveryReceiptId")]
        [HttpPost]
        public ActionResult SaveDeliveryReceipt(DeliveryDto deliveryDto)
        {
            if (ModelState.IsValid && deliveryDto.IsValid)
            {
                DeliveryReceipt deliveryReceipt;
                if (deliveryDto.DeliveryReceiptId > 0)
                {
                    deliveryReceipt =
                        DeliveryRepository.Get(x => x.DeliveryReceiptId == deliveryDto.DeliveryReceiptId)
                            .FirstOrDefault();
                    if (deliveryReceipt != null)
                    {
                        Mapper.Map(deliveryDto, deliveryReceipt);
                        DeliveryRepository.Update(deliveryReceipt);
                    }
                }
                else
                {
                    deliveryReceipt = Mapper.Map<DeliveryReceipt>(deliveryDto);
                    DeliveryRepository.Insert(deliveryReceipt);
                }
                if (deliveryReceipt != null && deliveryReceipt.OrderId > 0)
                {
                    var order = OrderRepository.Get(x => x.OrderId == deliveryDto.OrderId).FirstOrDefault();
                    if (order != null)
                    {
                        order.OrderStatusId = DataConstants.OrderStatus.Delivered;
                        OrderRepository.Update(order);
                    }
                }
                UnitOfWork.Commit();
                deliveryDto.DeliveryReceiptId = deliveryReceipt.DeliveryReceiptId;

                //let's get the customer name from the lookup
                deliveryDto.CustomerName =
                    CustromeRepository.Get(x => x.CustomerId == deliveryDto.CustomerId).First().CustomerName;

                return Json(deliveryDto);
            }
            ViewBag.CustomerList = CustomerListItems();
            return PartialView("DeliveryEditor", deliveryDto);
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
