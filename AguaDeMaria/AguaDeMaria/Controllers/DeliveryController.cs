using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Filters;
using AguaDeMaria.Model;
using AguaDeMaria.Model.Dto;
using AguaDeMaria.Models.Delivery;
using AguaDeMaria.Service;
using AutoMapper;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    public class DeliveryController : Controller
    {
        private readonly IDeliveryReceiptService deliveryReceiptService;
        private readonly IOrderService orderService;
        private readonly IInventoryLedgerService inventoryLedgerService;

        private IRepository<Customer> CustromeRepository { get; set; }

        private LookupDataManager LookupDataManager { get; set; }

        private SettingsManager SettingsManager { get; set; }

        private IUnitOfWork UnitOfWork { get; set; }

        public DeliveryController(IRepository<Customer> customerRepository,
            LookupDataManager manager,
            SettingsManager settingsManager,
            IUnitOfWork unitOfWork,
            IDeliveryReceiptService deliveryReceiptService,
            IOrderService orderService,
            IInventoryLedgerService inventoryLedgerService)
        {
            this.deliveryReceiptService = deliveryReceiptService;
            this.orderService = orderService;
            this.inventoryLedgerService = inventoryLedgerService;
            CustromeRepository = customerRepository;
            LookupDataManager = manager;
            SettingsManager = settingsManager;
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

            var deliveryReceipts = deliveryReceiptService.DeliveryReceipts(drStartDate, drEndDate);

            var drList = from dr in deliveryReceipts
                select Mapper.Map<DeliveryDto>(dr);
            return Json(drList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeliveryEditor(DeliveryParameter parameter)
        {
            DeliveryDto deliveryDto = null;
            if (parameter.IsDeliveryReceiptFromOrder || parameter.IsNewDelivery)
            {
                if (parameter.IsDeliveryReceiptFromOrder)
                {
                    //check if there is an existing delivery receipt
                    if (parameter.OrderId != null)
                    {
                        var delivery = deliveryReceiptService.GetByOrderId(parameter.OrderId.Value);
                        if (delivery != null)
                        {
                            deliveryDto = Mapper.Map<DeliveryDto>(delivery);
                        }
                        else
                        {
                            //this must be a new delivery
                            Order order = orderService.Get(parameter.OrderId.Value);
                            var orderDto = Mapper.Map<OrderDto>(order);
                            deliveryDto = Mapper.Map<DeliveryDto>(orderDto);
                            AssignDefaultValues(deliveryDto);
                        }
                    }
                }
                else
                {
                    deliveryDto = new DeliveryDto();
                    deliveryDto.OrderNumber = string.Format("{0:0000000000}", 0);
                    AssignDefaultValues(deliveryDto);
                }
            }
            else
            {
                if (parameter.DeliveryId != null)
                {
                    var delivery = deliveryReceiptService.Get(parameter.DeliveryId.Value);
                    deliveryDto = Mapper.Map<DeliveryDto>(delivery);
                }
            }
            ViewBag.CustomerList = CustomerListItems();
            return PartialView(deliveryDto);
        }

        private void AssignDefaultValues(DeliveryDto deliveryDto)
        {
            AssignDefaultPrice(deliveryDto);
            deliveryDto.DRDate = DateTime.UtcNow;
            deliveryDto.DRNumber = SettingsManager.GetNextDeliveryReceiptNumber();
        }

        private void AssignDefaultPrice(DeliveryDto deliveryDto)
        {
            deliveryDto.SlimUnitPrice =
                LookupDataManager.ProductTypes.First(x => x.ProductTypeId == DataConstants.ProductTypes.Slim).BasePrice;
            deliveryDto.SlimAmount = deliveryDto.SlimUnitPrice*deliveryDto.SlimQty;
            deliveryDto.RoundUnitPrice =
                LookupDataManager.ProductTypes.First(x => x.ProductTypeId == DataConstants.ProductTypes.Round).BasePrice;
            deliveryDto.RoundAmount = deliveryDto.RoundUnitPrice*deliveryDto.RoundQty;
        }

        [ExcludeIdValidation(IdField = "DeliveryReceiptId")]
        [HttpPost]
        [ConvertDatesToUtc]
        public ActionResult SaveDeliveryReceipt(DeliveryDto deliveryDto)
        {
            if (ModelState.IsValid && deliveryDto.IsValid)
            {
                DeliveryReceipt deliveryReceipt;
                if (deliveryDto.DeliveryReceiptId > 0)
                {
                    deliveryReceipt = deliveryReceiptService.Get(deliveryDto.DeliveryReceiptId);
                    if (deliveryReceipt != null)
                    {
                        Mapper.Map(deliveryDto, deliveryReceipt);
                        deliveryReceiptService.Update(deliveryReceipt);
                    }
                }
                else
                {
                    deliveryReceipt = Mapper.Map<DeliveryReceipt>(deliveryDto);
                    deliveryReceiptService.Insert(deliveryReceipt);
                }

                if (deliveryReceipt != null && deliveryReceipt.OrderId > 0)
                {
                    UpdateOrderStatus(deliveryDto);
                }
                UnitOfWork.Commit();

                inventoryLedgerService.PostToLedger(deliveryReceipt);

                if (deliveryReceipt != null) deliveryDto.DeliveryReceiptId = deliveryReceipt.DeliveryReceiptId;

                //let's get the customer name from the lookup
                deliveryDto.CustomerName =
                    CustromeRepository.Get(x => x.CustomerId == deliveryDto.CustomerId).First().CustomerName;

                return Json(deliveryDto);
            }
            ViewBag.CustomerList = CustomerListItems();
            return PartialView("DeliveryEditor", deliveryDto);
        }

        private void UpdateOrderStatus(DeliveryDto deliveryDto)
        {
            if (deliveryDto.OrderId != null)
            {
                var order = orderService.Get(deliveryDto.OrderId.Value);
                if (order != null)
                {
                    var orderDto = Mapper.Map<OrderDto>(order);
                    order.OrderStatusId = orderDto.CalculatedStatusId;
                    orderService.Update(order);
                }
            }
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
