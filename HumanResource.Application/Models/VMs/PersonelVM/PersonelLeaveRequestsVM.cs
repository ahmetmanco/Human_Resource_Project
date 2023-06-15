using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.PersonelVM
{
    public class PersonelLeaveRequestsVM
    {
        public int Id { get; set; }
        [Display(Name ="İzin Başlangıç Tarihi")]
        public string StartDate { get; set; }

        [Display(Name = "İzin Bitiş Tarihi")]
        public string EndDate { get; set; }

        [Display(Name = "İzinli Gün Sayısı")]
        public string LeavePeriod { get; set; }

        [Display(Name = "Geri Dönüş Tarihi")]
        public string ReturnDate { get; set; }

        [Display(Name ="İzin Sebebi")]
        public string LeaveType { get; set; }
    }
}
