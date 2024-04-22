
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class ProductSizeRepository : BaseRepository<ProductSize>,IProductSizeRepository
    {
        private readonly TailorProTrackContext _ctx;
        public ProductSizeRepository(TailorProTrackContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
