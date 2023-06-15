using System.Runtime.Serialization;

namespace HumanResource.Application.Models.VMs.ExpenseVM
{
	[DataContract]
	public class ExpenseTypePieVM
	{
		public ExpenseTypePieVM(string label, double y)
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
