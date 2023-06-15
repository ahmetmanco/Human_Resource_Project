namespace HumanResource.Domain.Entities
{
    public class Address : IBaseEntity
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int PostCode { get; set; }
        public int? DistrictId { get; set; }
        public int? CompanyId { get; set; }
        public Guid? AppUserId { get; set; }

        public int? StatuId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Statu? Statu { get; set; }


        //Navigation Property
        public District? District { get; set; }
        public AppUser? AppUser { get; set; }
        public Company? Company { get; set; }
    }
}
