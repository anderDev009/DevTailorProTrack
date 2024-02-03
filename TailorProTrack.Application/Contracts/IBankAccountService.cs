

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.BankAccount;

namespace TailorProTrack.Application.Contracts
{
    public interface IBankAccountService : IBaseService<BankAccountDtoAdd,BankAccountDtoRemove,BankAccountDtoUpdate>
    {
    }
}
