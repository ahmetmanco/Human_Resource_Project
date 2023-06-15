using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.DTOs.AccountDTO
{
    public class LoginDTO
    {
        [MinLength(6, ErrorMessage = "Username cannot enter less than 6 characters"), Required(ErrorMessage = "This field is required"), Display(Name = "UserName")]
        public string UserNameOrEmail { get; set; }
        [MinLength(6, ErrorMessage = "Password cannot enter less than 6 characters"), Required(ErrorMessage = "This field is required"), Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }
       
        
        
    }
}
