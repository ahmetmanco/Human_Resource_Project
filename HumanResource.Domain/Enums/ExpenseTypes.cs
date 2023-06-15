using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource.Domain.Enums
{
	public enum ExpenseTypes
	{
		[Display(Name = "Food Expenses")]
		Food_Expenses = 1,
		[Display(Name = "Transportation Expenses")]
		Transportation_Expenses,
		[Display(Name = "Clothing Expenses")]
		Clothing_Expenses,
		[Display(Name = "Energy and Communication Invoice Expenses")]
		Energy_and_Communication_Invoice_Expenses,
		[Display(Name = "Rental and Dues Expenses")]
		Rentaland_Dues_Expenses,
		[Display(Name = "Office Expenses")]
		Office_Expenses,
		[Display(Name = "Education Expenses")]
		Education_Expenses


	}
}
