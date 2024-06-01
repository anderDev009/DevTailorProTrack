

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Bank;

namespace TailorProTrack.Application.Contracts
{
    public interface IBankService : IBaseService<BankDtoAdd,BankDtoRemove,BankDtoUpdate>
    {
    }
}
