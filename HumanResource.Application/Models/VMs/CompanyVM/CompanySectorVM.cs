using System.Runtime.Serialization;

namespace HumanResource.Application.Models.VMs.CompanyVM
{
    [DataContract]
    public class CompanySectorVM
    {
        public string CompanyName { get; set; }
        public int CompanySectorId { get; set; }
        public string CompanySectorName { get; set; }
    }
}
