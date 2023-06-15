namespace HumanResource.Application.Models.DTOs.TitleDTOs
{
    public class UpdateTitleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int StatuId { get; set; }
    }
}
