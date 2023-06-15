namespace HumanResource.Application.Models.DTOs.ExpenseTypeDTO
{
    public class UpdateExpenseTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int StatuId { get; set; }
    }
}
