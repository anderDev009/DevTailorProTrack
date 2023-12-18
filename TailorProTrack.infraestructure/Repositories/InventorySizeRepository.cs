using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class InventorySizeRepository : BaseRepository<InventorySize>, IInventorySizeRepository
    {
        private readonly TailorProTrackContext _context;

        public InventorySizeRepository(TailorProTrackContext ctx) : base(ctx) 
        {
            _context = ctx;
        }
        public override void Save(InventorySize entity)
        {
            this._context.Add(entity);
            this._context.SaveChanges();
        }
    }
}
