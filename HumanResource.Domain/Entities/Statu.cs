namespace HumanResource.Domain.Entities
{
    public class Statu
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public int StatuEnumId { get; set; }


		public List<AppUser> AppUsers { get; set; }
		public List<Leave> Leaves { get; set; }
		public List<Advance> Advances { get; set; }
		public List<Address> Addresses { get; set; }
		public List<Department> Departments { get; set; }
		public List<Title> Titles { get; set; }
		public List<Expense> Expenses { get; set; }
		public List<Company> Companies { get; set; }
		public List<ExpenseType> ExpenseTypes { get; set; }
		public List<LeaveType> LeaveTypes { get; set; }

	}
}
