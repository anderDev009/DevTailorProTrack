

using Microsoft.EntityFrameworkCore;
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

        //public Task<Product> GetProductByProductIdAsync(int productId)
        //{
        //    ProductColor product = _ctx.Set<ProductColor>()
        //        .Include(x => x.Product)
        //        .Include(x => x.Color)
        //        .Where(p => p.FK_PRODUCT == productid)

        //}
    }
}
