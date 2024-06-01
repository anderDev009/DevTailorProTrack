using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository 
    {
        private readonly TailorProTrackContext _ctx;

        public BrandRepository(TailorProTrackContext ctx) : base(ctx) 
        {
            _ctx = ctx;
        }

        public override void Update(Brand entity)
        {
            Brand brand = this.GetEntity(entity.ID);

            brand.NAME = entity.NAME;

            this._ctx.Update(brand);
            this._ctx.SaveChanges();
        }
    }
}
