using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Filters;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Pickup;
using AguaDeMaria.Service;
using AutoMapper;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    public class PickupController : Controller
    {
        private readonly IOrderService orderService;

        public PickupController(IRepository<PickupSlip> pickupSlipRepository,
            IRepository<Customer> customerRepository,
            IRepository<DeliveryReceipt> deliveryRepository,
            LookupDataManager manager,
            SettingsManager settingsManager,
            IOrderService orderService)
        {
            this.orderService = orderService;
            PickupSlipRepository = pickupSlipRepository;
            DeliveryRepository = deliveryRepository;
            CustomerRepository = customerRepository;
            LookupDataManager = manager;
            SettingsManager = settingsManager;
        }

        public IRepository<DeliveryReceipt> DeliveryRepository { get; set; }

        private IRepository<PickupSlip> PickupSlipRepository { get; set; }

        private IRepository<Customer> CustomerRepository { get; set; }

        private LookupDataManager LookupDataManager { get; set; }

        private SettingsManager SettingsManager { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPickupSlipList(DateTime? startDate, DateTime? endDate)
        {
            DateTime orderStartDate = startDate.HasValue ? startDate.Value : DateTime.Today;
            DateTime orderEndDate = endDate.HasValue ? endDate.Value : orderStartDate.AddDays(1);

            var pickups = PickupSlipRepository.Get(x => x.PickupDate >= orderStartDate && x.PickupDate <= orderEndDate,
                includedProperties: "Customer");
            var pickupsList = from p in pickups
                select Mapper.Map<PickupSlipDto>(p);
            return Json(pickupsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PickupSlipEditor(PickupSlipParameter pickupParams)
        {
            PickupSlipDto pickupSlipDto = null;
            if (pickupParams.IsNewPickup())
            {
                PickupSlip pickupSlip = new PickupSlip();
                if (pickupParams.DeliveryReceiptId.HasValue && pickupParams.DeliveryReceiptId > 0)
                {
                    var deliveryReceipt =
                        DeliveryRepository.Get(x => x.DeliveryReceiptId == pickupParams.DeliveryReceiptId).First();
                    pickupSlipDto = Mapper.Map<PickupSlipDto>(deliveryReceipt);
                }
                else if (pickupParams.OrderId.HasValue && pickupParams.OrderId > 0)
                {
                    var order = orderService.Get(pickupParams.OrderId.Value);
                    pickupSlipDto = Mapper.Map<PickupSlipDto>(pickupSlip);
                    if (order != null) pickupSlipDto.CustomerId = order.CustomerId;
                }
                else
                {
                    pickupSlipDto = Mapper.Map<PickupSlipDto>(pickupSlip);
                }
                pickupSlipDto.PickupDate = DateTime.UtcNow;
                pickupSlipDto.PickupSlipNumber = SettingsManager.GetNextPickupSlipNumber();
            }
            else
            {
                PickupSlip pickupSlip =
                    PickupSlipRepository.Get(x => x.PickupSlipId == pickupParams.PickupSlipId).FirstOrDefault();
                pickupSlipDto = Mapper.Map<PickupSlipDto>(pickupSlip);
            }
            ViewBag.CustomerList = CustomerListItems();
            return PartialView("PickupSlipEditor", pickupSlipDto);
        }

        [ConvertDatesToUtc]
        public ActionResult SavePickupSlip(PickupSlipDto pickupSlipDto)
        {
            if (pickupSlipDto.IsValid)
            {
                PickupSlip pickupSlip = null;
                if (pickupSlipDto.PickupSlipId > 0)
                {
                    pickupSlip =
                        PickupSlipRepository.Get(x => x.PickupSlipId == pickupSlipDto.PickupSlipId).FirstOrDefault();
                    if (pickupSlip != null)
                    {
                        Mapper.Map(pickupSlipDto, pickupSlip);
                        PickupSlipRepository.Update(pickupSlip);
                    }
                }
                else
                {
                    pickupSlip = Mapper.Map<PickupSlip>(pickupSlipDto);
                    PickupSlipRepository.Insert(pickupSlip);
                }

                PickupSlipRepository.Commit();
                pickupSlipDto.PickupSlipId = pickupSlip.PickupSlipId;

                //let's get the customer name from the lookup
                pickupSlipDto.CustomerName =
                    CustomerRepository.Get(x => x.CustomerId == pickupSlipDto.CustomerId).First().CustomerName;

                return Json(pickupSlipDto);
            }
            ViewBag.CustomerList = CustomerListItems();
            return PartialView("PickupSlipEditor", pickupSlipDto);
        }

        private IEnumerable<SelectListItem> CustomerListItems()
        {
            IEnumerable<SelectListItem> customerList =
                CustomerRepository.Get(c => c.CustomerId > 0)
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