using System.Runtime.Serialization;

namespace HumanResource.Application.Models.VMs.CompanyVM
{
    [DataContract]
    public class CompanyStatuVM
    {
        public string CompanyName { get; set; }
        public int? CompanyStatuId { get; set; }
        public string CompanyStatuName { get; set; }
    }
}
