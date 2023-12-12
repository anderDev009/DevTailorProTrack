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
    public class SizeRepository : BaseRepository<Size>, ISizeRepository
    {
        private readonly TailorProTrackContext _context;
        public SizeRepository(TailorProTrackContext ctx) : base(ctx) 
        {
            _context = ctx;
        }
    }
}
