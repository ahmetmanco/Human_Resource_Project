namespace HumanResource.Domain.Entities
{
    public class ExpenseType
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public int? CompanyId{ get; set; }
        public int StatuId { get; set; }

        //Navigation Property
        public List<Expense> Expenses { get; set; }
        public Company Company { get; set; }
        public Statu Statu { get; set; }
    }
}
