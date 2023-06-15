namespace HumanResource.Domain.Entities
{
    public class Title
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatuId { get; set; }
        public int? CompanyId { get; set; }

        //Navigation Properties
        public Statu Statu { get; set; }
        public List<AppUser> Users { get; set; }
        public Company? Company { get; set; }
    }
}
