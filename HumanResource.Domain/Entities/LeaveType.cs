namespace HumanResource.Domain.Entities
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public int StatuId { get; set; }


        //Navigation Property
        public List<Leave> Leaves { get; set; }
        public Company Company { get; set; }
        public Statu Statu { get; set; }
    }
}
