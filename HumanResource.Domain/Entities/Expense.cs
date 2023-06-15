using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource.Domain.Entities
{
    public class Expense : IBaseEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyTypeId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string? LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public int ExpenseTypeId { get; set; }

        public int? StatuId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Statu? Statu { get; set; }


        //Navigation Properties
        public CurrencyType CurrencyType { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public AppUser User { get; set; }
    }
}
