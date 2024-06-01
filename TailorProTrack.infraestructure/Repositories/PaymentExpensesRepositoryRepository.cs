
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PaymentExpensesRepositoryRepository : BaseRepository<PaymentExpenses>, IPaymentExpensesRepository
    {
        private readonly TailorProTrackContext _ctx;
        private readonly IBankAccountRepository _bankAccountRepository;

        public PaymentExpensesRepositoryRepository(TailorProTrackContext ctx,
            IBankAccountRepository bankAccountRepository) : base(ctx)
        {
            _ctx = ctx;
            _bankAccountRepository = bankAccountRepository;
        }

        public override int Save(PaymentExpenses entity)
        {
            if (entity.FK_BANK_ACCOUNT != null && entity.FK_BANK_ACCOUNT != 0)
            {
                BankAccount account = _bankAccountRepository.GetEntity((int)entity.FK_BANK_ACCOUNT);
                //confirmamos que cuente con el balance suficiente y actualizamos
                if (account.BALANCE < entity.AMOUNT)
                {
                    throw new Exception("No cuenta con el balance suficiente");
                }
                account.BALANCE -= entity.AMOUNT;
                _bankAccountRepository.Update(account);

            }
            return base.Save(entity);
        }
    }
}
