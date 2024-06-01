
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class BuyInventoryDetailRepository : BaseRepository<BuyInventoryDetail>, IBuyInventoryDetailRepository
    {
        private readonly TailorProTrackContext _ctx;
        public BuyInventoryDetailRepository(TailorProTrackContext ctx) : base(ctx) 
        {
            _ctx = ctx;
        }

        public bool SaveMany(List<BuyInventoryDetail> list)
        {
            _ctx.Set<BuyInventoryDetail>().AddRange(list);
            int success =_ctx.SaveChanges();
            if(success == 0)
            {
                return false;
            }
            return true;    
        }
    }
}
