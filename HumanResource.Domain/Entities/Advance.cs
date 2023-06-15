namespace HumanResource.Domain.Entities
{
    public class Advance : IBaseEntity
	{
		public int Id { get; set; }
		public decimal Amount { get; set; }
		public int NumberOfInstallments	{ get; set; }
        public string Description { get; set; }
        public DateTime AdvanceDate { get; set; }
        public Guid UserId { get; set; }

        public int? StatuId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Statu? Statu { get; set; }


        //Navigation Property
        public AppUser User { get; set; }
	}
}
