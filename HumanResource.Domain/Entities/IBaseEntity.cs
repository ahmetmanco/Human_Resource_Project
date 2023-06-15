namespace HumanResource.Domain.Entities
{
    public interface IBaseEntity
    {
        public int? StatuId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        //Navigation Property
        public Statu? Statu { get; set; }
    }
}
