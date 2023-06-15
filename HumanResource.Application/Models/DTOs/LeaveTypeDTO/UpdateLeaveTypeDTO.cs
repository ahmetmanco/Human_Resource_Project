namespace HumanResource.Application.Models.DTOs.LeaveTypeDTO
{
    public class UpdateLeaveTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int StatuId { get; set; }
    }
}
