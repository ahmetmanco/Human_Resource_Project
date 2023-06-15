using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource.Application.Models.VMs.ExpenseVM
{
	public enum CurrencyTypeEnumVM
	{
		[Display(Name = "TRY")]
		TRY = 1,
		[Display(Name = "USD")]
		USD,
		[Display(Name = "EUR")]
		EUR,
		[Display(Name = "GPB")]
		GPB,
		[Display(Name = "CNY")]
		CNY,
		[Display(Name = "CAD")]
		CAD,
		[Display(Name = "RUB")]
		RUB
	}
}
