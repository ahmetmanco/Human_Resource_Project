namespace HumanResource.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatuId { get; set; }
        public int? CompanyId { get; set; }

        //Navigation Property
        public Statu Statu { get; set; }
        public List<AppUser> Users { get; set; }
        public Company? Company { get; set; }
    }
}
