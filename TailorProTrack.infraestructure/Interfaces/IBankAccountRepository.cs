

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IBankAccountRepository : IBaseRepository<BankAccount>
    {
        void AddBalance(int IdAccount, decimal Balance);
        void SubstractBalance(int IdAccount, decimal Balance);
    }
}
