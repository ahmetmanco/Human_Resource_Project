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
	public class UpdateAdvanceDTO
	{
		public int Id { get; set; }
        [Required(ErrorMessage = "Amount field cannot be empty!")]
        [Range(0, 99999.99, ErrorMessage = "Please enter between 0-99999.99!")]
        [Display(Name ="Amount")]
        [Amount]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Installment field cannot be empty!")]
        [Range(0, 10, ErrorMessage = "Please enter between 0-10.")]
        [Display(Name ="Number Of Installments")]
        public int NumberOfInstallments { get; set; }
        public string Description { get; set; }
		[Display(Name = "Advance Date")]
        [DataType(DataType.Date)]
		public DateTime AdvanceDate { get; set; }
        public DateTime ModifiedDate => DateTime.Now;

        [Required(ErrorMessage = "Update date cannot be blank!"), DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        [ValidateNever]
        public int StatuId { get; set; }
    }
}
