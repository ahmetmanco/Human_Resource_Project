using HumanResource.Application.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.DTOs.ExpenseDTO
{
    public class UpdateExpenseDTO
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        [Display(Name = "Expense Date")]
        [Required(ErrorMessage = "Expense date cannot be null.")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }
        public DateTime Modified => DateTime.Now;

        [Required(ErrorMessage = "Amount field cannot be empty!")]
        [Range(0, 99999.99, ErrorMessage = "Please enter between 0-99999.99!")]
        //[RegularExpression(@"^\d+$", ErrorMessage = "No letters or symbols can be entered!")]
        [Display(Name = "Amount")]
        [Amount]
        public Decimal Amount { get; set; }

        [Required(ErrorMessage = "Description field cannot be null.")]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string? LongDescription { get; set; }

        [Required(ErrorMessage = "Expense Type cannot be null.")]
        [Display(Name = "Expense Type")]
        public int ExpenseTypeId { get; set; }

        [Required(ErrorMessage = "Currency Type cannot be null.")]
        [Display(Name = "Currency Type")]
        public int CurrencyTypeId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ValidateNever]
        public int StatuId { get; set; }
    }
}
