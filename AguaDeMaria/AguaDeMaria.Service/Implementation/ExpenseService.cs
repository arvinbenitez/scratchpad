using System;
using System.Collections.Generic;
using System.Linq;
using AguaDeMaria.Common.Data;
using AguaDeMaria.Model;

namespace AguaDeMaria.Service.Implementation
{
    public class ExpenseService : IExpenseService
    {
        private readonly IRepository<Expense> expenseRepository;

        public ExpenseService(IRepository<Expense> expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        public IEnumerable<Expense> GetExpenses(DateTime startDate, DateTime endDate)
        {
            return expenseRepository.Get(x => x.ExpenseDate >= startDate && x.ExpenseDate <= endDate,
                x => x.OrderBy(y => y.ExpenseDate),"ExpenseType,ExpenseType.ExpenseCategory");
        }

        public void Insert(Expense expense)
        {
            expenseRepository.Insert(expense);
            expenseRepository.Commit();
        }

        public void Update(Expense expense)
        {
            expenseRepository.Update(expense);
            expenseRepository.Commit();
        }

        public Expense Get(int expenseId)
        {
            return
                expenseRepository.Get(x => x.ExpenseId == expenseId, x => x.OrderBy(y => y.ExpenseDate),
                    "ExpenseType,ExpenseType.ExpenseCategory").FirstOrDefault();
        }
    }
}