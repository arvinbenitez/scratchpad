using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Filters;
using AguaDeMaria.Model;
using AguaDeMaria.Models.Expense;
using AguaDeMaria.Service;
using AutoMapper;
using ServiceStack.Common.Extensions;

namespace AguaDeMaria.Controllers
{
    [Authorize]
    [ConvertDatesToUtc]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService expenseService;
        private readonly LookupDataManager lookupDataManager;

        public ExpenseController(IExpenseService expenseService, LookupDataManager lookupDataManager)
        {
            this.expenseService = expenseService;
            this.lookupDataManager = lookupDataManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetExpenseList(DateTime? startDate, DateTime? endDate)
        {
            DateTime expenseStartDate = startDate.HasValue ? startDate.Value : DateTime.Today;
            DateTime expenseEndDate = endDate.HasValue ? endDate.Value : expenseStartDate.AddDays(1);

            var result = expenseService.GetExpenses(expenseStartDate, expenseEndDate);

            var list = from dr in result
                         select Mapper.Map<ExpenseDto>(dr);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetExpenseSummary(DateTime? startDate, DateTime? endDate)
        {
            DateTime expenseStartDate = startDate.HasValue ? startDate.Value : DateTime.Today;
            DateTime expenseEndDate = endDate.HasValue ? endDate.Value : expenseStartDate.AddDays(1);

            var result = expenseService.GetExpenseSummaries(expenseStartDate, expenseEndDate);
            return Json(new {rows = result}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExpenseEditor(int? expenseId)
        {
            var expenseDto = new ExpenseDto {ExpenseDate = DateTime.UtcNow, ExpenseCategoryTypeId = 1};
            if (expenseId.HasValue)
            {
                var expense = expenseService.Get(expenseId.Value);
                expenseDto = Mapper.Map<ExpenseDto>(expense);
            }

            ViewBag.ExpenseCategoryList = GetExpenseCategoryList();
            ViewBag.ExpenseTypeList = GetExpenseTypeList(expenseDto.ExpenseCategoryTypeId);
            return PartialView("ExpenseEditor", expenseDto);
        }

        public ActionResult GetExpenseTypes(int expenseCategoryId)
        {
            return Json(GetExpenseTypeList(expenseCategoryId), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<SelectListItem> GetExpenseTypeList(int expenseCategoryId)
        {
            if (expenseCategoryId > 0) { 
            return from x in lookupDataManager.ExpenseTypes
                    where x.ExpenseCategoryId == expenseCategoryId
                select new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ExpenseTypeId.ToString(CultureInfo.InvariantCulture)
                };
            }

            return new[] {new SelectListItem {Text = "<<select a category>>", Value = "0"}};
        }

        private IEnumerable<SelectListItem> GetExpenseCategoryList()
        {
            return from x in lookupDataManager.ExpenseCategories
                select new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ExpenseCategoryId.ToString(CultureInfo.InvariantCulture)
                };
        }

        [ExcludeIdValidation(IdField = "ExpenseId")]
        [HttpPost]
        [ConvertDatesToUtc]
        public ActionResult Save(ExpenseDto expenseDto)
        {
            if (ModelState.IsValid && expenseDto.IsValid)
            {
                var expense = Mapper.Map<Expense>(expenseDto);
                if (expenseDto.IsNew)
                {
                    expenseService.Insert(expense);
                }
                else
                {
                    expenseService.Update(expense);
                }
                expense = expenseService.Get(expense.ExpenseId);
                return Json(Mapper.Map<ExpenseDto>(expense));
            }
            return PartialView("ExpenseEditor", expenseDto);
        }
    }
}
