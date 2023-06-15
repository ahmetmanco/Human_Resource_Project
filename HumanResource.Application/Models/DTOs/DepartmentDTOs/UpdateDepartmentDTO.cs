namespace HumanResource.Application.Models.DTOs.DepartmentDTOs
{
    public class UpdateDepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int StatuId { get; set; }
    }
}
