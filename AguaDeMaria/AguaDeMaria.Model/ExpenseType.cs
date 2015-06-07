using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("ExpenseType")]
    public class ExpenseType
    {
        public ExpenseType()
        {
            Expenses = new HashSet<Expense>();
        }
        public int ExpenseTypeId { get; set; }
        public string Name { get; set; }
        public int ExpenseCategoryId { get; set; }

        public virtual ExpenseCategory ExpenseCategory { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}