using System.Runtime.Serialization;

namespace HumanResource.Application.Models.VMs.CompanyVM
{
    [DataContract]
    public class CompanySectorPieVM
    {
        public CompanySectorPieVM(string label, double y)
        {
            Label = label;
            Y = y;
        }

        [DataMember(Name = "label")]
        public string Label = "";

        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }
}
