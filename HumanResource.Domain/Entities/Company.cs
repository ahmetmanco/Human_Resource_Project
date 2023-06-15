using HumanResource.Domain.Enums;

namespace HumanResource.Domain.Entities
{
    public class Company : IBaseEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOfficeName { get; set; }
        public string PhoneNumber { get; set; }
        public string NumberOfEmployee { get; set; }
        public string? ImagePath { get; set; }
        public int CompanySectorId { get; set; }
        public Guid? CompanyRepresentativeId { get; set; }
        public DateTime ActivationDate { get; set; }

        public int? StatuId { get; set; }
        public DateTime CreatedDate { get; set; }   
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Statu? Statu { get; set; }

        //Navigation Property
        public List<Department>? Departments { get; set; }
        public Address? Address { get; set; }
        public List<Title>? Titles { get; set; }
        public List<AppUser>? Users { get; set; }
        public AppUser? CompanyRepresentative { get; set; }
        public CompanySector? CompanySector { get; set; }
        public List<ExpenseType> ExpenseTypes { get; set; }
        public List<LeaveType> LeaveTypes{ get; set; }

    }
}
