using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("Expense")]
    public class Expense
    {
        public int ExpenseId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int ExpenseTypeId { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }
    }
}