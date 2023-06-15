namespace HumanResource.Domain.Entities
{
    public class CompanySector
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CompanySectorEnumId { get; set; }

        //Navigation Property
        public List<Company> Companies { get; set; }
    }
}
