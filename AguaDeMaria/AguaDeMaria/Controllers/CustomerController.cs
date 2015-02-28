using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Filters;
using AguaDeMaria.Model;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Models.Customer;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        IRepository<Customer> _customerRepository;
        private LookupDataManager _lookupManager;

        public CustomerController(IRepository<Customer> customerRepository, LookupDataManager lookupManager)
        {
            _customerRepository = customerRepository;
            _lookupManager = lookupManager;
        }

        private LookupDataManager LookupManager
        {
            get { return _lookupManager; }
        }

        //
        // GET: /Customer/
        public ActionResult Index()
        {
            var customerList = _customerRepository.Get(x => x.CustomerId > 0);
            return View(customerList);
        }

        [HttpGet]
        public JsonResult GetCustomerList()
        {
            var customerList = from c in _customerRepository.Get(x => x.CustomerId > 0)
                               let customerType = LookupManager.CustomerTypes.Where(x => x.CustomerTypeId == c.CustomerTypeId).FirstOrDefault()
                               orderby c.CustomerName
                               select new
                               CustomerDto
                               {
                                   CustomerId = c.CustomerId,
                                   CustomerCode = c.CustomerCode,
                                   CustomerName = c.CustomerName,
                                   CustomerTypeId = c.CustomerTypeId,
                                   CustomerTypeName = customerType == null ? string.Empty : customerType.CustomerTypeName,
                                   Address = c.Address,
                                   ContactNumbers = c.ContactNumbers,
                                   Notes = c.Notes
                               };
            return Json(customerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomerEditor(int? customerId)
        {
            var customer = new Customer();
            if (customerId.HasValue)
            {
                customer = _customerRepository.Get(x => x.CustomerId == customerId).FirstOrDefault();
            }

            SetCustomerTypes();
            return PartialView("CustomerEditor", customer);
        }

        private void SetCustomerTypes()
        {
            var customerTypesList = from custType in LookupManager.CustomerTypes
                select new SelectListItem
                {
                    Text = custType.CustomerTypeName,
                    Value = custType.CustomerTypeId.ToString(CultureInfo.InvariantCulture)
                };
            ViewBag.CustTypeList = customerTypesList;
        }

        [ExcludeIdValidation(IdField = "CustomerId")]
        [HttpPost]
        public ActionResult SaveCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var customerTypeId = Request.Form["customerType"];
                if (customerTypeId != null)
                {
                    customer.CustomerTypeId = Convert.ToInt16(customerTypeId);
                }

                if (customer.CustomerId > 0)
                {
                    _customerRepository.Update(customer);
                    _customerRepository.Commit();
                }
                else
                {
                    _customerRepository.Insert(customer);
                    _customerRepository.Commit();
                }
                var selectedCustomerType =
                    LookupManager.CustomerTypes.FirstOrDefault(x => x.CustomerTypeId == customer.CustomerTypeId);
                return Json(new CustomerDto
                {
                    CustomerId = customer.CustomerId,
                    CustomerCode = customer.CustomerCode,
                    CustomerName = customer.CustomerName,
                    CustomerTypeId = customer.CustomerTypeId,
                    CustomerTypeName =
                        selectedCustomerType == null ? string.Empty : selectedCustomerType.CustomerTypeName,
                    Address = customer.Address,
                    ContactNumbers = customer.ContactNumbers,
                    Notes = customer.Notes
                });
            }
            SetCustomerTypes();
            return PartialView("CustomerEditor", customer);
        }
    }
}
