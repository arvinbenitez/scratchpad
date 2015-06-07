using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AguaDeMaria.Models.Expense
{
    public class ExpenseDto
    {
        [Display(Name="Category")]
        [Required]
        public int ExpenseCategoryTypeId { get; set; }

        public string ExpenseCategory { get; set; }

        [Display(Name = "Expense Id")]
        [Required]
        public int ExpenseId { get; set; }

        [Display(Name = "Expense Type")]
        [Required]
        public int ExpenseTypeId { get; set; }

        public string ExpenseType { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Date")]
        [Required]
        public DateTime ExpenseDate { get; set; }

        [Display(Name = "Amount")]
        [Required]
        public decimal Amount { get; set; }

        public IEnumerable<string> ValidationErrors {
            get
            {
                if (Amount == 0)
                    yield return "Amount must have a value.";
                if (ExpenseTypeId == 0)
                    yield return "You must select an expense type and category";
            }
        }

        public bool IsValid
        {
            get { return !ValidationErrors.Any(); }
        }

        public bool IsNew
        {
            get { return ExpenseId == 0; }
        }
    }
}