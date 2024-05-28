
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PaymentExpensesRepository : BaseRepository<PaymentExpenses>, IPaymentExpenses
    {
        private readonly TailorProTrackContext _ctx;

        public PaymentExpensesRepository(TailorProTrackContext ctx) : base(ctx)
        {


        }
    }
}
