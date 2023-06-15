using AutoMapper;
using HumanResource.Application.Models.DTOs.CompanyDTO;
using HumanResource.Application.Models.DTOs.CompanyManagerDTO;
using HumanResource.Application.Models.VMs.CompanyManagerVMs;
using HumanResource.Application.Models.VMs.ExpenseVM;
using HumanResource.Application.Models.VMs.PersonelVM;
using HumanResource.Application.Services.PersonelService;
using HumanResource.Domain.Entities;
using HumanResource.Domain.Enums;
using HumanResource.Domain.Repositories;
using HumanResource.Domain.Repositries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace HumanResource.Application.Services.CompanyManagerService
{
    internal class CompanyManagerService : ICompanyManagerService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ITitleRepository _titleRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IPersonelService _personelService;
        private readonly ILeaveRepository _leaveRepository;
        private readonly IAdvanceRepository _advanceRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly ILeaveTypeRepository _LeaveTypeRepository;
        public CompanyManagerService(IDepartmentRepository departmentRepository, ITitleRepository titleRepository, IMapper mapper, UserManager<AppUser> userManager, IAppUserRepository appUserRepository, IPersonelService personelService, ILeaveRepository leaveRepository, IAdvanceRepository advanceRepository, IExpenseRepository expenseRepository, ICompanyRepository companyRepository, IAddressRepository addressRepository, IExpenseTypeRepository expenseTypeRepository, ILeaveTypeRepository leaveTypeRepository)
        {
            _departmentRepository = departmentRepository;
            _titleRepository = titleRepository;
            _mapper = mapper;
            _userManager = userManager;
            _appUserRepository = appUserRepository;
            _personelService = personelService;
            _leaveRepository = leaveRepository;
            _advanceRepository = advanceRepository;
            _expenseRepository = expenseRepository;
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
            _expenseTypeRepository = expenseTypeRepository;
            _LeaveTypeRepository = leaveTypeRepository;
        }

        public async Task<UpdateEmployeeDTO> GetByUserName(Guid id)
        {
            UpdateEmployeeDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
            select: x => new UpdateEmployeeDTO
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
                DepartmentId = x.DepartmentId,
                BirthDate = x.BirthDate,
                RecruitmentDate = x.RecruitmentDate,
                ManagerId = x.ManagerId,
                ImagePath = x.ImagePath,
                TitleId = x.TitleId
            },
            where: x => x.Id == id,
            orderby: null,
            include: x => x.Include(x => x.Address).Include(x => x.Address.District).Include(x => x.Address.District.City)
            );


            return result;
        }
        public async Task<CreateEmployeeVM> CreateEmployee(CreateEmployeeDTO model)
        {
            var userEmail = await _userManager.FindByEmailAsync(model.Email);
            var userUserName = await _userManager.FindByNameAsync(model.UserName);
            CreateEmployeeVM createEmployee = new CreateEmployeeVM();
            if (userEmail == null && userUserName == null)
            {
                AppUser newEmployee = _mapper.Map<AppUser>(model);
                if (model.Image != null)
                {
                    using var image = Image.Load(model.Image.OpenReadStream());

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/media/images/{guid}.jpg");

                    newEmployee.ImagePath = $"/media/images/{guid}.jpg";
                }
                else
                    newEmployee.ImagePath = model.ImagePath;
                newEmployee.Address = new Address()
                {
                    CreatedDate = DateTime.Now,
                    Description = model.AddressDescription,
                    DistrictId = model.DistrictId,
                };
                var password = new Random().Next(100000, 9999990).ToString();

                await _userManager.CreateAsync(newEmployee, password);
                IdentityResult result = await _userManager.AddToRoleAsync(newEmployee, "Employee");
                if (!model.IsEmployee)
                {
                   await _userManager.AddToRoleAsync(newEmployee, "Manager");
                }

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(newEmployee);
                    createEmployee.Email = newEmployee.Email;
                    createEmployee.Token = token;
                    createEmployee.Result = result;
                    createEmployee.Password = password;
                }
                else
                {
                    createEmployee.Result = result;
                }
                return createEmployee;
            }
            else
            {
                if (userEmail != null)
                {
                    createEmployee.Errors.Add("Email already exist.");
                }
                if (userUserName != null)
                {
                    createEmployee.Errors.Add("User Name already exist.");
                }
                createEmployee.Model = model;
                return createEmployee;
            }
        }

        public async Task<List<CompanyManagerVM>> GetCompanyManagers(int? companyId)
        {
            var companyPersonels = await _appUserRepository.GetFilteredList(
              select: x => new CompanyManagerVM()
              {
                  Id = x.Id,
                  FullName = x.FirstName + " " + x.LastName,
                  UserName = x.UserName,
                  Email = x.Email
              },
              where: x => x.CompanyId == companyId,
              orderby: x => x.OrderByDescending(x => x.FirstName)
              );
            List<CompanyManagerVM> companyManagers = new List<CompanyManagerVM>();
            foreach (var personel in companyPersonels)
            {
                if (await IsManager(personel.UserName))
                {
                    companyManagers.Add(personel);
                }
            }

            return companyManagers;
        }

        public async Task<List<DepartmentVM>> GetDepartments(int? companyId)
        {
            var departments = await _departmentRepository.GetFilteredList(
              select: x => new DepartmentVM()
              {
                  Id = x.Id,
                  Name = x.Name

              },
              where: x => x.StatuId == Status.Active.GetHashCode() && x.CompanyId == companyId,
              orderby: x => x.OrderByDescending(x => x.Name)
              );

            return departments;
        }

        public async Task<List<EmployeeVM>> GetEmployees(int companyId)
        {

            var employees = await _appUserRepository.GetFilteredList(
              select: x => new EmployeeVM()
              {
                  Id = x.Id,
                  FullName = x.FirstName + " " + x.LastName,
                  DepartmentName = x.Department.Name,
                  Title = x.Title.Name,
                  UserName = x.UserName,
                  ManagerName = x.Manager.FirstName + " " + x.Manager.LastName,
                  Phone = x.PhoneNumber,
                  Email = x.Email,
                  BirthDate = x.BirthDate.ToString(),
              },
              where: x => x.StatuId != Status.Deleted.GetHashCode() && x.CompanyId == companyId,
              orderby: x => x.OrderByDescending(x => x.CreatedDate),
              include: x => x.Include(x => x.Department).Include(x => x.Title).Include(x => x.Manager)
              );

            return employees;
        }

        public async Task<List<TitleVM>> GetTitles(int? companyId)
        {
            var titles = await _titleRepository.GetFilteredList(
             select: x => new TitleVM()
             {
                 Id = x.Id,
                 Name = x.Name

             },
             where: x => x.StatuId == Status.Active.GetHashCode() && x.CompanyId == companyId,
             orderby: x => x.OrderByDescending(x => x.Name)
             );

            return titles;
        }

        public async Task<bool> IsCompanyManager(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);

            foreach (var roles in await _userManager.GetRolesAsync(user))
            {
                if (roles == "CompanyManager")
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> IsManager(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);

            foreach (var roles in await _userManager.GetRolesAsync(user))
            {
                if (roles == "Manager")
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IdentityResult> UpdateEmployee(UpdateEmployeeDTO model)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.Id == model.Id);
            AppUser isUserMailExists = await _userManager.FindByEmailAsync(model.Email);
            IdentityError errorEmail = new IdentityError();
            if (user.Email != model.Email)
            {
                if (isUserMailExists == null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }
                else
                {
                    errorEmail.Description = "Email already exist.";
                }
            }
            AppUser isUserNameExists = await _userManager.FindByNameAsync(model.UserName);
            IdentityError errorUserName = new IdentityError();
            if (user.UserName != model.UserName)
            {
                if (isUserNameExists == null)
                {
                    await _userManager.SetUserNameAsync(user, model.UserName);
                }
                else
                {
                    errorUserName.Description = "User name already exist.";
                }
            }
            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/media/images/{guid}.jpg");

                user.ImagePath = $"/media/images/{guid}.jpg";
            }
            user.BirthDate = model.BirthDate;
            user.BloodTypeId = model.BloodTypeId;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.DepartmentId = model.DepartmentId;
            user.RecruitmentDate = model.RecruitmentDate;
            user.ManagerId = model.ManagerId;
            user.TitleId = model.TitleId;

            if(!model.IsEmployee && !(await IsManager(user.UserName)))
            {
                await _userManager.AddToRoleAsync(user, "Manager");
            }
            if (model.IsEmployee && (await IsManager(user.UserName)))
            {
                await _userManager.RemoveFromRoleAsync(user, "Manager");
            }


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

          
            if (errorEmail.Description == null && errorUserName.Description == null)
                return await _userManager.UpdateAsync(user);
            else
            {
                if (errorEmail.Description != null)
                {
                    if (errorUserName.Description != null)
                    {
                        return IdentityResult.Failed(errorEmail, errorUserName);

                    }
                    return IdentityResult.Failed(errorEmail);
                }
                else
                    return IdentityResult.Failed(errorUserName);
            }
        }

        public async Task Delete(Guid id)
        {
            AppUser leave = await _appUserRepository.GetDefault(x => x.Id == id);
            if (leave != null)
            {
                leave.StatuId = Status.Deleted.GetHashCode();
                leave.DeletedDate = DateTime.Now;
                await _appUserRepository.Delete(leave);
            }
        }

        public async Task<List<PersonelLeaveRequestVM>> GetPersonelLeaveRequests(Guid id)
        {
            var personelLeaveRequests = await _leaveRepository.GetFilteredList(
             select: x => new PersonelLeaveRequestVM()
             {
                 Id = x.Id,
                 PersonelFullName = x.User.FirstName + " " + x.User.LastName,
                 LeaveType = x.LeaveType.Name,
                 LeavePeriod = x.LeavePeriod,
                 StartDate = x.StartDate.ToShortDateString(),
                 EndDate = x.EndDate.ToShortDateString(),

             },
             where: x => x.StatuId == Status.Awating_Approval.GetHashCode() && x.User.ManagerId == id,
             orderby: x => x.OrderByDescending(x => x.CreatedDate),
             include: x => x.Include(x => x.LeaveType).Include(x => x.User).Include(x => x.User.Manager)
             );

            return personelLeaveRequests;
        }

        public async Task<List<PersonelAdvanceRequestVM>> GetPersonelAdvanceRequests(Guid id)
        {
            var personelAdvanceRequests = await _advanceRepository.GetFilteredList(
             select: x => new PersonelAdvanceRequestVM()
             {
                 Id = x.Id,
                 PersonelFullName = x.User.FirstName + " " + x.User.LastName,
                 Amount = x.Amount,
                 NumberOfInstallments = x.NumberOfInstallments,
                 Description = x.Description,
                 AdvanceDate = x.AdvanceDate.ToShortDateString()

             },
             where: x => x.StatuId == Status.Awating_Approval.GetHashCode() && x.User.ManagerId == id,
             orderby: x => x.OrderByDescending(x => x.CreatedDate),
             include: x => x.Include(x => x.User).Include(x => x.User.Manager)
             );

            return personelAdvanceRequests;
        }

        public async Task<List<PersonelExpenseRequestVM>> GetPersonelExpenseRequests(Guid id)
        {
            var personelAdvanceRequests = await _expenseRepository.GetFilteredList(
             select: x => new PersonelExpenseRequestVM()
             {
                 Id = x.Id,
                 PersonelFullName = x.User.FirstName + " " + x.User.LastName,
                 Amount = x.Amount,
                 ExpenseDate = x.ExpenseDate.ToShortDateString(),
                 CurrencyType = x.CurrencyType.Name,
                 ShortDescription = x.ShortDescription

             },
             where: x => x.StatuId == Status.Awating_Approval.GetHashCode() && x.User.ManagerId == id,
             orderby: x => x.OrderByDescending(x => x.CreatedDate),
             include: x => x.Include(x => x.User).Include(x => x.User.Manager).Include(x => x.CurrencyType)
                );

            return personelAdvanceRequests;
        }

        public async Task<UpdateCompanyDTO> GetCompany(int? id)
        {
            UpdateCompanyDTO result = await _companyRepository.GetFilteredFirstOrDefault(
            select: x => new UpdateCompanyDTO
            {
                CompanyId = x.Id,
                CompanyName = x.CompanyName,
                TaxNumber = x.TaxNumber,
                TaxOfficeName = x.TaxOfficeName,
                PhoneNumber = x.PhoneNumber,
                NumberOfEmployee = x.NumberOfEmployee,
                CountryId = x.Address.District.City.CountryId,
                CityId = x.Address.District.CityId,
                DistrictId = x.Address.DistrictId,
                AddressDescription = x.Address.Description,
                ManagerId = x.CompanyRepresentativeId,
                ImagePath = x.ImagePath
            },
            where: x => x.Id == id,
            orderby: null,
            include: x => x.Include(x => x.Address).Include(x => x.Address.District).Include(x => x.Address.District.City).Include(x=>x.CompanyRepresentative)
            );
            return result;
        }
        public async Task<bool> UpdateCompany(UpdateCompanyDTO model)
        {
            Company company = await _companyRepository.GetDefault(x=>x.Id == model.CompanyId);
            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/media/images/{guid}.jpg");

				company.ImagePath = $"/media/images/{guid}.jpg";
            }
            company.CompanyName = model.CompanyName;
            company.PhoneNumber = model.PhoneNumber;
            company.NumberOfEmployee = model.NumberOfEmployee;


            Address address = await _addressRepository.GetDefault(x => x.CompanyId == model.CompanyId);

            if(model.DistrictId != address.DistrictId || model.AddressDescription != address.Description)
            {
                address.DistrictId = model.DistrictId;
                address.Description = model.AddressDescription;
                await _addressRepository.Update(address);
            }

            company.CompanyRepresentativeId = model.ManagerId;
            return await _companyRepository.Update(company);
        }

        public async Task<List<ExpenseTypeVM>> GetExpenseTypes(int? companyId)
        {
            var expenseTypeVM = await _expenseTypeRepository.GetFilteredList(
              select: x => new ExpenseTypeVM()
              {
                  Id = x.Id,
                  Name = x.Name

              },
              where: x => x.StatuId == Status.Active.GetHashCode() && x.CompanyId == companyId,
              orderby: x => x.OrderByDescending(x => x.Name)
              );

            return expenseTypeVM;
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes(int? companyId)
        {
            var leaveTypeVM = await _LeaveTypeRepository.GetFilteredList(
              select: x => new LeaveTypeVM()
              {
                  Id = x.Id,
                  Name = x.Name

              },
              where: x => x.StatuId == Status.Active.GetHashCode() && x.CompanyId == companyId,
              orderby: x => x.OrderByDescending(x => x.Name)
              );

            return leaveTypeVM;
        }
		public async Task<List<ExpenseTypePieVM>> ExpensesDistributionByExpensesType(int? companyId)
		{
			List<ExpenseTypePieVM> ExpensesDistributionByExpensesType = new List<ExpenseTypePieVM>();
			var expenses = await _expenseRepository.GetFilteredList(
				select: x => new ExpenseTypePie2VM()
				{
					ExpenseTypeId = x.ExpenseTypeId
				},
				where: x=>x.User.CompanyId == companyId,
				orderby: x => x.OrderBy(x => x.ExpenseTypeId),
                include: x=>x.Include(x=>x.User)
				);
			double expenseCount = expenses.Count;
			for (int i = 1; i <= Enum.GetValues(typeof(ExpenseTypes)).Length; i++)
			{
				var tempExpenses = await _expenseRepository.GetFilteredList(
				select: x => new ExpenseTypePie2VM()
				{
					ExpenseTypeId = x.ExpenseTypeId,
					ExpenseTypeName = x.ExpenseType.Name
				},
				where: x => x.ExpenseTypeId == i && x.User.CompanyId == companyId,
				orderby: null,
				include: x => x.Include(x => x.ExpenseType).Include(x=>x.User)
				);

				if (tempExpenses.Count != 0)
				{
					double ratio = (tempExpenses.Count / expenseCount) * 100;
					ExpensesDistributionByExpensesType.Add(new ExpenseTypePieVM($"{tempExpenses[0].ExpenseTypeName} ({tempExpenses.Count})", Math.Round(ratio, 2)));
				}
			}
			return ExpensesDistributionByExpensesType;
		}
	}
}
