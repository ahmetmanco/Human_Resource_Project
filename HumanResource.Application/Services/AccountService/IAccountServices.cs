using HumanResource.Application.Models.DTOs.AccountDTO;
using HumanResource.Application.Models.VMs.PersonelVM;
using Microsoft.AspNetCore.Identity;

namespace HumanResource.Application.Services.AccountServices
{
    public interface IAccountServices
    {
        Task<RegisterVM> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task<UpdateProfileDTO> GetByUserName(string userName);
        Task UpdateUser(UpdateProfileDTO model);
        Task LogOut();
        Task<bool> IsAdmin(string userName);
        Task<IdentityResult> ConfirmEmail(string token, string username);

    }
}
