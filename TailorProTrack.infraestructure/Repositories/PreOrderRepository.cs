

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

        public override void Update(PreOrder entity)
        {
            PreOrder preOrder = this.GetEntity(entity.ID);

            preOrder.FK_CLIENT = entity.FK_CLIENT;
            preOrder.USER_MOD = entity.USER_MOD;
            preOrder.MODIFIED_AT = DateTime.Now;

            this._ctx.Update(preOrder);
        }
    }
}
