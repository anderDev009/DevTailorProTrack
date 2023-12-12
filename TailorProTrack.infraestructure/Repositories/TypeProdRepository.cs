

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class TypeProdRepository : BaseRepository<Type_prod>, ITypeProdRepository
    {
        private readonly TailorProTrackContext _context;
        
        public TypeProdRepository(TailorProTrackContext ctx) : base(ctx) 
        {
            this._context = ctx;
        }
    }
}
