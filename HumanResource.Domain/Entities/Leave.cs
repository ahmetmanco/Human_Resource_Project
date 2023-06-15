namespace HumanResource.Domain.Entities
{
    public class Leave : IBaseEntity
	{
		public int Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime ReturnDate { get; set; }
        public string LeavePeriod { get; set; }
        public int LeaveTypeId { get; set; }
		public Guid UserId { get; set; }

        public int? StatuId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Statu? Statu { get; set; }


        //Navigation Property
        public LeaveType LeaveType { get; set; }
		public AppUser User { get; set; }
	}
}
