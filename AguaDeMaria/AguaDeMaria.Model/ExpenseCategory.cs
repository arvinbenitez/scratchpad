using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("ExpenseCategory")]
    public class ExpenseCategory
    {
        public ExpenseCategory()
        {
            ExpenseTypes = new HashSet<ExpenseType>();
        }
        public int ExpenseCategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<ExpenseType> ExpenseTypes { get; set; }
    }
}