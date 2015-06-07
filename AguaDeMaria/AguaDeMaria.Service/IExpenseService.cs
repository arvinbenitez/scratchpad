using System;
using System.Collections.Generic;
using AguaDeMaria.Model;

namespace AguaDeMaria.Service
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetExpenses(DateTime startDate, DateTime endDate);
        void Insert(Expense expense);
        void Update(Expense expense);

        Expense Get(int expenseId);
    }
}