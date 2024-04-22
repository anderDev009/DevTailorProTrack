

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class ProductColorRepository : BaseRepository<ProductColor>, IProductColorRepository
    {
        private readonly TailorProTrackContext _ctx;
        public ProductColorRepository(TailorProTrackContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
