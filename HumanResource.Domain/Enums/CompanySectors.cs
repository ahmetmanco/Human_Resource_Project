using System.ComponentModel.DataAnnotations;

namespace HumanResource.Domain.Enums
{
    public enum CompanySectors
    {
        Agriculture = 1,
        [Display(Name ="Basic Metal Production")]
        Basic_Metal_Production,
        [Display(Name = "Chemical Industry")]
        Chemical_Industry,
        Commerce,
        Construction,
        Education,
        [Display(Name = "Financial Service")]
        Financial_Service,
        Food,
        Forestry,
        [Display(Name = "Health Service")]
        Health_Service,
        Hotel,
        Mining,
        [Display(Name = "Mechanical and Electrical Engineering")]
        Mechanical_and_Electrical_Engineering,
        Media,
        [Display(Name = "Oil and Gas Production")]
        Oil_and_Gas_Production,
        [Display(Name = "Telecommunication Service")]
        Telecommunication_Service,
        [Display(Name = "Public Service")]
        Public_Service,
        Shipping,
        Textile,
        Transportation,
        Utility
    }
}
