using Bogus;
using HumanResource.Domain.Entities;
using HumanResource.Domain.Enums;
using HumanResource.Infrastructure.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HumanResource.Infrastructure.SeedData
{
    public class SeedData
    {
        public async static Task Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                List<Statu> status = new List<Statu>();
                List<BloodType> bloodTypes = new List<BloodType>();
                List<City> city = new List<City>();
                List<District> district = new List<District>();
                List<Country> countries = new List<Country>();
                List<Department> department = new List<Department>();
                List<CurrencyType> currencyTypes = new List<CurrencyType>();
                List<CompanySector> companySectors = new List<CompanySector>();

                if (!context.Status.Any())
                {

                    foreach (var item in Enum.GetValues(typeof(Status)))
                    {
                        Statu statu = new Statu();
                        statu.Name = String.Join(" ", item.ToString().Split("_"));
                        statu.StatuEnumId = item.GetHashCode();
                        status.Add(statu);
                    }

                    await context.Status.AddRangeAsync(status);
                    await context.SaveChangesAsync();


                }

                if (!context.BloodTypes.Any())
                {
                    foreach (var item in Enum.GetValues(typeof(BloodTypes)))
                    {

                        BloodType bloodType = new BloodType();

                        var bloodTypesSplit = item.ToString().Split("_");
                        if (bloodTypesSplit[0] != "Zero")
                        {
                            if (bloodTypesSplit[1] == "positive")
                                bloodType.Name = bloodTypesSplit[0] + " rh +";
                            else
                                bloodType.Name = bloodTypesSplit[0] + " rh -";

                            bloodType.BloodTypeEnumId = item.GetHashCode();
                            bloodTypes.Add(bloodType);
                        }
                        else
                        {
                            if (bloodTypesSplit[1] == "positive")
                                bloodType.Name = "0 rh +";
                            else
                                bloodType.Name = "0 rh -";

                            bloodType.BloodTypeEnumId = item.GetHashCode();
                            bloodTypes.Add(bloodType);
                        }
                    }

                    await context.BloodTypes.AddRangeAsync(bloodTypes);
                    await context.SaveChangesAsync();
                }

                if (!context.CurrencyTypes.Any())
                {
                    foreach (var item in Enum.GetValues(typeof(CurrencyTypes)))
                    {

                        CurrencyType currencyType = new CurrencyType();
                        currencyType.Name = item.ToString();
                        currencyType.CurrencyTypeEnumId = item.GetHashCode();
                        currencyTypes.Add(currencyType);
                    }

                    await context.CurrencyTypes.AddRangeAsync(currencyTypes);
                    await context.SaveChangesAsync();
                }

                if (!context.CompanySectors.Any())
                {

                    foreach (var item in Enum.GetValues(typeof(CompanySectors)))
                    {
                        CompanySector companySector = new CompanySector();
                        companySector.Name = String.Join(" ", item.ToString().Split("_"));
                        companySector.CompanySectorEnumId = item.GetHashCode();
                        companySectors.Add(companySector);
                    }

                    await context.CompanySectors.AddRangeAsync(companySectors);

                    await context.SaveChangesAsync();



                }

                if (!context.Countries.Any())
                {
                    var countryFaker = new Faker<Country>()
                       .RuleFor(x => x.Name, y => y.Address.City());

                    countries = countryFaker.Generate(5);
                    await context.Countries.AddRangeAsync(countries);
                    await context.SaveChangesAsync();

                }

                if (!context.Cities.Any())
                {
                    var cityFaker = new Faker<City>()
                       .RuleFor(x => x.Name, y => y.Address.City())
                       .RuleFor(x => x.Country, y => y.PickRandom(context.Countries.ToList()));

                    city = cityFaker.Generate(15);
                    await context.Cities.AddRangeAsync(city);
                    await context.SaveChangesAsync();

                }

                if (!context.Districts.Any())
                {
                    var districtFaker = new Faker<District>()
                        .RuleFor(x => x.Name, y => y.Address.State())
                        .RuleFor(x => x.City, y => y.PickRandom(context.Cities.ToList()));

                    district = districtFaker.Generate(50);
                    await context.Districts.AddRangeAsync(district);
                    await context.SaveChangesAsync();

                }

                if (!context.Roles.Any())
                {
                    var roleStore = new RoleStore<IdentityRole<Guid>, ApplicationDbContext, Guid>(context);

                    await roleStore.CreateAsync(new IdentityRole<Guid>() { Name = "SiteAdmin", NormalizedName = "SITEADMIN" });
                    await roleStore.CreateAsync(new IdentityRole<Guid>() { Name = "CompanyManager", NormalizedName = "COMPANYMANAGER" });
                    await roleStore.CreateAsync(new IdentityRole<Guid>() { Name = "Employee", NormalizedName = "EMPLOYEE" });
                    await roleStore.CreateAsync(new IdentityRole<Guid>() { Name = "Manager", NormalizedName = "MANAGER" });
                    await context.SaveChangesAsync();
                }


                if (!context.Users.Any())
                {


                    Company company = new Company()
                    {
                        CompanyName = "Test",
                        TaxNumber = "1234567890",
                        PhoneNumber = "12345678901",
                        NumberOfEmployee = "20-50",
                        TaxOfficeName = "Test",
                        StatuId = Status.Active.GetHashCode(),
                        CreatedDate = DateTime.Now,
                        CompanySectorId = 1,

                    };
                    await context.Companies.AddAsync(company);
                    await context.SaveChangesAsync();
                    var companyManagerId = Guid.NewGuid();

                    var passwordHasher = new PasswordHasher<AppUser>();
                    AppUser companyManager = new AppUser
                    {
                        Id = companyManagerId,
                        FirstName = "company",
                        LastName = "manager",
                        UserName = "companyManager",
                        NormalizedUserName = "COMPANYMANAGER",
                        Email = "companyManager@gmail.com",
                        NormalizedEmail = "COMPANYMANAGER@GMAIL.COM",
                        ImagePath = "/images/noImage.png",
                        CreatedDate = DateTime.Now,
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        TwoFactorEnabled = false,
                        PhoneNumberConfirmed = false,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        CompanyId = 1
                    };

                    var hashed = passwordHasher.HashPassword(companyManager, "123456");
                    companyManager.PasswordHash = hashed;

                    var companyManagerStore = new UserStore<AppUser, IdentityRole<Guid>, ApplicationDbContext, Guid>(context);

                    await companyManagerStore.CreateAsync(companyManager);
                    await companyManagerStore.AddToRoleAsync(companyManager, "COMPANYMANAGER");
                    company.CompanyRepresentativeId = companyManagerId;
                    context.Companies.Update(company);
                    await context.SaveChangesAsync();


                    AppUser employee = new AppUser
                    {
                        FirstName = "employee",
                        LastName = "employee",
                        UserName = "employee",
                        NormalizedUserName = "EMPLOYEE",
                        Email = "employee@gmail.com",
                        NormalizedEmail = "EMPLOYEE@GMAIL.COM",
                        ImagePath = "/images/noImage.png",
                        CreatedDate = DateTime.Now,
                        ManagerId = companyManagerId,
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        TwoFactorEnabled = false,
                        PhoneNumberConfirmed = false,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        CompanyId = 1
                    };

                    var hashedCustomer = passwordHasher.HashPassword(employee, "123456");
                    employee.PasswordHash = hashedCustomer;

                    var employeeStore = new UserStore<AppUser, IdentityRole<Guid>, ApplicationDbContext, Guid>(context);

                    await employeeStore.CreateAsync(employee);
                    await employeeStore.AddToRoleAsync(employee, "EMPLOYEE");
                    await context.SaveChangesAsync();


                    AppUser siteAdmin = new AppUser
                    {
                        FirstName = "siteAdmin",
                        LastName = "siteAdmin",
                        UserName = "siteAdmin",
                        NormalizedUserName = "SITEADMIN",
                        Email = "siteAdmin@gmail.com",
                        NormalizedEmail = "SITEADMIN@GMAIL.COM",
                        ImagePath = "/images/noImage.png",
                        CreatedDate = DateTime.Now,
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        TwoFactorEnabled = false,
                        PhoneNumberConfirmed = false,
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };

                    var hashedsiteAdmin = passwordHasher.HashPassword(siteAdmin, "123456");
                    siteAdmin.PasswordHash = hashedsiteAdmin;

                    var siteAdminStore = new UserStore<AppUser, IdentityRole<Guid>, ApplicationDbContext, Guid>(context);

                    await siteAdminStore.CreateAsync(siteAdmin);
                    await siteAdminStore.AddToRoleAsync(siteAdmin, "SITEADMIN");
                    await context.SaveChangesAsync();
                }

            }
        }
    }
}

