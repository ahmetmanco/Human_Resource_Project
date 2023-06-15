using AutoMapper;
using HumanResource.Application.Models.DTOs.AccountDTO;
using HumanResource.Application.Models.VMs.PersonelVM;
using HumanResource.Application.Services.ExpenceTypeService;
using HumanResource.Application.Services.LeaveTypeService;
using HumanResource.Domain.Entities;
using HumanResource.Domain.Enums;
using HumanResource.Domain.Repositories;
using HumanResource.Domain.Repositries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Image = SixLabors.ImageSharp.Image;

namespace HumanResource.Application.Services.AccountServices
{
    public class AccountServices : IAccountServices
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IExpenceTypeService _expenceTypeService;
        private readonly ILeaveTypeService _leaveTypeService;

        public AccountServices(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper, ICompanyRepository companyRepository, IAddressRepository addressRepository, IExpenceTypeService expenceTypeService, ILeaveTypeService leaveTypeService)
        {
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
            _expenceTypeService = expenceTypeService;
            _leaveTypeService = leaveTypeService;
        }

        public async Task<IdentityResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                return result;
            }
            return IdentityResult.Failed();


        }

        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            UpdateProfileDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
            select: x => new UpdateProfileDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                CountryId = x.Address.District.City.CountryId,
                CityId = x.Address.District.CityId,
                DistrictId = x.Address.DistrictId,
                AddressDescription = x.Address.Description,
                BloodTypeId = x.BloodTypeId,
                BirthDate = x.BirthDate,
                RecruitmentDate = x.RecruitmentDate,
                ImagePath = x.ImagePath,
                TitleId = x.TitleId,
                DepartmentId = x.DepartmentId,
                ManagerId = x.ManagerId
            },
            where: x => x.UserName == userName,
            orderby: null,
            include: x => x.Include(x => x.Manager).Include(x => x.Department).Include(x => x.Address).Include(x => x.Address.District).Include(x => x.Address.District.City).Include(x => x.Title).Include(x => x.Company)
            );


            return result;
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            var userEmail = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
            var userName = await _userManager.FindByNameAsync(model.UserNameOrEmail);
            if (userEmail == null && userName == null)
            {
                return SignInResult.Failed;
            }

            if (userEmail != null)
            {
                return await _signInManager.PasswordSignInAsync(userEmail.UserName, model.Password, false, false);
            }
            return await _signInManager.PasswordSignInAsync(model.UserNameOrEmail, model.Password, false, false);
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterVM> Register(RegisterDTO model)
        {
            AppUser user = _mapper.Map<AppUser>(model);
            Company company = _mapper.Map<Company>(model);
            company.StatuId = Status.Awating_Approval.GetHashCode();
            Address address = _mapper.Map<Address>(model);
            RegisterVM register = new RegisterVM();

            IdentityResult resultUser = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "CompanyManager");
            await _userManager.AddToRoleAsync(user, "Manager");
            await _userManager.AddToRoleAsync(user, "Employee");
            bool resultCompany = await _companyRepository.Add(company);
            if (resultCompany && resultUser.Succeeded)
            {
                bool resultAddress = await _addressRepository.Add(address);
                if (resultAddress)
                {
                    AppUser user1 = await _userManager.FindByNameAsync(user.UserName);
                    Company company1 = await _companyRepository.GetDefault(x => x.TaxNumber == model.TaxNumber);
                    user1.CompanyId = company1.Id;
                    company1.Address = address;
                    company.CompanyRepresentativeId = user1.Id;
                    await _userManager.UpdateAsync(user1);
                    await _companyRepository.Update(company1);

                    await _expenceTypeService.CreateDefault(company1.Id);
                    await _leaveTypeService.CreateDefault(company1.Id);

                    if (resultUser.Succeeded)
                    {
                        register.Email = user.Email;
                        register.Result = resultUser;

                    }
                    else
                    {
                        register.Result = resultUser;
                    }

                    return register;
                }
            }

            register.Result = IdentityResult.Failed();
            return register;




        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.Id == model.Id);

            if (model.Password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            }

            if (model.Email != null)
            {
                AppUser isUserMailExists = await _userManager.FindByEmailAsync(model.Email);
                if (isUserMailExists == null)
                    await _userManager.SetEmailAsync(user, model.Email);
            }
            if (model.UserName != null)
            {
                AppUser isUserNameExists = await _userManager.FindByNameAsync(model.UserName);
                if (isUserNameExists == null)
                    await _userManager.SetUserNameAsync(user, model.UserName);
            }
            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/media/images/{guid}.jpg");

                user.ImagePath = $"/media/images/{guid}.jpg";
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.BirthDate = model.BirthDate;
            user.BloodTypeId = model.BloodTypeId;
            user.DepartmentId = model.DepartmentId;
            user.TitleId = model.TitleId;
            user.ManagerId = model.ManagerId;
            user.BirthDate = model.BirthDate;
            user.RecruitmentDate = model.RecruitmentDate;

            Address address = await _addressRepository.GetDefault(x => x.AppUserId == model.Id);
            if (user.Address != null)
            {
                if (model.DistrictId != address.DistrictId || model.AddressDescription != address.Description)
                {
                    address.DistrictId = model.DistrictId;
                    address.Description = model.AddressDescription;
                    await _addressRepository.Update(address);
                }
            }
            else
            {
                user.Address = new Address()
                {
                    CreatedDate = DateTime.Now,
                    Description = model.AddressDescription,
                    DistrictId = model.DistrictId,
                };
            }

            await _userManager.UpdateAsync(user);

        }

        public async Task<IList<string>> GetUserRole(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<bool> IsCompanyManager(string userName)
        {
            var roles = await GetUserRole(userName);

            foreach (var role in roles)
            {
                if (role == "CompanyManager")
                    return true;
            }
            return false;
        }

        public async Task<bool> IsAdmin(string userName)
        {
            var userEmail = await _userManager.FindByEmailAsync(userName);
            var userUserName = await _userManager.FindByNameAsync(userName);
            if (userUserName != null)
            {
                var roles = await GetUserRole(userName);

                foreach (var role in roles)
                {
                    if (role == "SiteAdmin")
                        return true;
                }
                return false;
            }
            else
            {
                var roles = await GetUserRole(userEmail.UserName);

                foreach (var role in roles)
                {
                    if (role == "SiteAdmin")
                        return true;
                }
                return false;

            }
        }
    }
}
