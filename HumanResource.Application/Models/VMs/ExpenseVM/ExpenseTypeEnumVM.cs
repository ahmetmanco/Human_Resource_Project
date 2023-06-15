using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource.Application.Models.VMs.ExpenseVM
{
	public enum ExpenseTypeEnumVM
	{
		[Display(Name = "Food Expenses")]
		FoodExpenses = 1,
		[Display(Name = "Transportation Expenses")]
		TransportationExpenses,
		[Display(Name = "Clothing Expenses")]
		ClothingExpenses,
		[Display(Name = "Energy and Communication Invoice Expenses")]
		EnergyandCommunicationInvoiceExpenses,
		[Display(Name = "Rental and Dues Expenses")]
		RentalandDuesExpenses,
		[Display(Name = "Office Expenses")]
		OfficeExpenses,
		[Display(Name = "Education Expenses")]
		EducationExpenses
	}
}
