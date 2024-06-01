
namespace TailorProTrack.Application.Dtos.BankAccount
{
    public class BankAccountDtoBase : BaseDto
    {
        public string BankAccount {  get; set; }
        public int FkBank {  get; set; }
    }
}
