namespace HumanResource.Domain.Entities
{
    public class BloodType
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public int BloodTypeEnumId { get; set; }


		//Navigation Property
		public List<AppUser> Users { get; set; }

        public BloodType()
		{
			Users= new List<AppUser>();
		}
	}
}
