using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AguaDeMaria.Model
{
    [Table("ExpenseSummary")]
    public class ExpenseSummary
    {
        
        [Key]
        public int ExpenseId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ExpenseCategory { get; set; }
        public string ExpenseType { get; set; }
        public decimal Amount { get; set; }
    }
}