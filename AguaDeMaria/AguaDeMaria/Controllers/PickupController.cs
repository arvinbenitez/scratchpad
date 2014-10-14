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
    [Authorize]
    public class PickupController : Controller
    {

        public PickupController(IRepository<PickupSlip> orderRepository,
                                IRepository<Customer> customerRepository,
                                IRepository<DeliveryReceipt> deliveryRepository,
                                LookupDataManager manager,
                                SettingsManager settingsManager)
        {
            PickupSlipRepository = orderRepository;
            DeliveryRepository = deliveryRepository;
            CustromeRepository = customerRepository;
            LookupDataManager = manager;
            SettingsManager = settingsManager;
        }

        public IRepository<DeliveryReceipt> DeliveryRepository { get; set; }

        private IRepository<PickupSlip> PickupSlipRepository { get; set; }

        private IRepository<Customer> CustromeRepository { get; set; }

        private LookupDataManager LookupDataManager { get; set; }

        private SettingsManager SettingsManager { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPickupSlipList(DateTime? startDate, DateTime? endDate)
        {
            DateTime orderStartDate = startDate.HasValue ? startDate.Value.Date : DateTime.Today;
            DateTime orderEndDate = endDate.HasValue ? endDate.Value.Date.AddDays(1) : orderStartDate.AddDays(1);

            var pickups = PickupSlipRepository.Get(x => x.PickupDate >= orderStartDate && x.PickupDate <= orderEndDate, includedProperties: "Customer");
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
                    var deliveryReceipt = DeliveryRepository.Get(x => x.DeliveryReceiptId == pickupParams.DeliveryReceiptId).First();
                    pickupSlipDto = Mapper.Map<PickupSlipDto>(deliveryReceipt);
                }
                else
                {
                    pickupSlipDto = Mapper.Map<PickupSlipDto>(pickupSlip);
                }
                pickupSlipDto.PickupDate = DateTime.Now;
                pickupSlipDto.PickupSlipNumber = SettingsManager.GetNextPickupSlipNumber();
            }
            else
            {
                PickupSlip pickupSlip = PickupSlipRepository.Get(x => x.PickupSlipId == pickupParams.PickupSlipId).FirstOrDefault();
                pickupSlipDto = Mapper.Map<PickupSlipDto>(pickupSlip);
            }
            ViewBag.CustomerList = CustomerListItems();
            return PartialView("PickupSlipEditor", pickupSlipDto);

        }

        public ActionResult SavePickupSlip(PickupSlipDto pickupSlipDto)
        { 
            if (pickupSlipDto.IsValid)
            {
                PickupSlip pickupSlip = null;
                if (pickupSlipDto.PickupSlipId > 0)
                {
                    pickupSlip = PickupSlipRepository.Get(x => x.PickupSlipId == pickupSlipDto.PickupSlipId).FirstOrDefault();
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
                    CustromeRepository.Get(x => x.CustomerId == pickupSlipDto.CustomerId).First().CustomerName;

                return Json(pickupSlipDto);
            }
            ViewBag.CustomerList = CustomerListItems();
            return PartialView("PickupSlipEditor", pickupSlipDto);
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
