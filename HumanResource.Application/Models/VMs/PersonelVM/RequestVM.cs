namespace HumanResource.Application.Models.VMs.PersonelVM
{
    public class RequestVM
    {
        public string ManagerEmail { get; set; }
        public bool Result { get; set; } = false;
        public int RequestId { get; set; }
        public string EmployeeName { get; set; }


    }
}
