using System.ComponentModel.DataAnnotations;

namespace HumanResource.Domain.Enums
{
	public enum CurrencyTypes
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
