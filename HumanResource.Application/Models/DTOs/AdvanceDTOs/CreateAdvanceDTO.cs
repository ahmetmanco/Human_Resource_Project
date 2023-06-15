using HumanResource.Application.Extensions;
using HumanResource.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource.Application.Models.DTOs.AdvanceDTOs
{
    public class CreateAdvanceDTO
    {

        [Required(ErrorMessage = "Amount field cannot be empty!")]
        [Range(0, 9999999.99, ErrorMessage = "Please enter between 0-10000000!")]
        //[RegularExpression(@"^\d+$", ErrorMessage = "No letters or symbols can be entered!")]
        [Display(Name = "Amount")]
        [Amount]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Installment field cannot be empty!")]
        [Range(0, 10, ErrorMessage = "Please enter between 0-10.")]
        [Display(Name = "Number of Installments")]
        public int NumberOfInstallments { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Advance Date")]
        public DateTime AdvanceDate { get; set; }
        public DateTime CreatedDate => DateTime.Now;
        public Guid UserId { get; set; }
        [ValidateNever]
        public int StatuId { get; set; }

    }
}
