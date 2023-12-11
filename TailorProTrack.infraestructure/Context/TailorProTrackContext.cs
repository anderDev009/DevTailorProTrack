using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailorProTrack.infraestructure.Context
{
    public class TailorProTrackContext : DbContext
    {
        public TailorProTrackContext(DbContextOptions options) : base(options) { }
    }
}
