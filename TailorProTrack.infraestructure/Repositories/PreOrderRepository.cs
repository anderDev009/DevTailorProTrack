

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PreOrderRepository : BaseRepository<PreOrder>, IPreOrderRepository
    {
        private readonly TailorProTrackContext _ctx;
        public PreOrderRepository(TailorProTrackContext ctx) : base(ctx)
        {
                _ctx = ctx;
        }

        public bool PreOrderIsEditable(int id)
        {
            return !_ctx.Set<Order>().Any(x => x.FK_PREORDER == id);
        }

   
    }
}
