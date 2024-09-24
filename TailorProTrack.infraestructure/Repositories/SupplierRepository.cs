
using System.Runtime.CompilerServices;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        private readonly TailorProTrackContext _ctx;
        public SupplierRepository(TailorProTrackContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public override void Update(Supplier entity)
        {
            //entity.MODIFIED_AT = DateTime.Now;
            entity.CREATED_AT = DateTime.Now;
            base.Update(entity);
        }
    }

}
