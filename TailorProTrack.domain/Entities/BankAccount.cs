
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class BankAccount : BaseEntity   
    {
        public string BANK_ACCOUNT {  get; set; }
        public int FK_BANK {  get; set; }
        public decimal BALANCE { get; set; }

        public List<Expenses>? Expenses { get; set; }
    }
}
