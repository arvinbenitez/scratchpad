using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AguaDeMaria.Model;
using AguaDeMaria.Common.Data;

namespace AguaDeMaria.Controllers
{
    public class CustomerController : Controller
    {
        IRepository<Customer> _customerRepository;
        LookupDataManager _lookupManager;

        public CustomerController(IRepository<Customer> customerRepository, LookupDataManager lookupManager)
        {
            this._customerRepository = customerRepository;
            this._lookupManager = lookupManager;
        }

        //
        // GET: /Customer/
        public ActionResult Index()
        {
            var customerList = _customerRepository.Get(x => x.CustomerId > 0);
            return View(customerList);
        }

        public ActionResult CustomerEditor(int? customerId)
        {
            var customer = new Customer();
            if (customerId.HasValue)
            {
                customer = this._customerRepository.Get(x => x.CustomerId == customerId).FirstOrDefault();
            }

            var customerTypesList = from custType in _lookupManager.CustomerTypes
                                    select new SelectListItem
                                    {
                                        Text = custType.CustomerTypeName,
                                        Value = custType.CustomerTypeId.ToString()
                                    };

            var selectedCustomerType = customerTypesList.Where(x => x.Value == customer.CustomerTypeId.ToString()).FirstOrDefault();
            if (selectedCustomerType != null) selectedCustomerType.Selected = true;

            ViewBag.CustTypeList = customerTypesList;

            return PartialView("CustomerEditor", customer);
        }

        [HttpPost]
        public JsonResult SaveCustomer(Customer customer)
        {
            var aguaDeMariaContext = new AguaDeMaria.Model.AguaDeMariaContext();

            var customerTypeId = Request.Form["customerType"];
            if (customerTypeId != null)
            {
                customer.CustomerTypeId = Convert.ToInt16(customerTypeId);
            }

            if (customer.CustomerId > 0)
            {
                this._customerRepository.Update(customer);
                this._customerRepository.Commit();
            }
            else
            {
                this._customerRepository.Insert(customer);
                this._customerRepository.Commit();
            }
            return Json(customer);
        }


    }
}
