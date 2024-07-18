

namespace TailorProTrack.Application.Dtos.BankAccount
{
    public class BankAccountDtoGet
    {
        public int Id { get; set; }
        public string Account {  get; set; }
        public string BankType { get; set; }
        public decimal Balance { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
	}
}
