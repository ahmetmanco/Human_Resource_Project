using Microsoft.AspNetCore.Identity;

namespace HumanResource.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>, IBaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public int AddressId { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? RecruitmentDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? BloodTypeId { get; set; }
        public int? TitleId { get; set; }
        public int? CompanyId { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? SiteAdminId { get; set; }
        public string? ImagePath { get; set; }

        public int? StatuId { get; set; }
        public DateTime CreatedDate { get; set; }//??
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Statu? Statu { get; set; }


        //Navigation Properties
        public Department? Department { get; set; }
        public BloodType? BloodType { get; set; }
        public AppUser? Manager { get; set; }
        public List<AppUser>? Employees { get; set; }
        public Address? Address { get; set; }
        public List<Advance>? Advances { get; set; }
        public List<Expense>? Expenses { get; set; }
        public Title? Title { get; set; }
        public Company? Company { get; set; }
        public Company? CompanyRepresentative { get; set; }
        public AppUser? SiteAdmin { get; set; }


    }
}
