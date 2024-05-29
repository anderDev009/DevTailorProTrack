
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PaymentExpensesRepositoryRepository : BaseRepository<PaymentExpenses>, IPaymentExpensesRepository
    {
        private readonly TailorProTrackContext _ctx;

        public PaymentExpensesRepositoryRepository(TailorProTrackContext ctx) : base(ctx)
        {
        }

    
    }
}
