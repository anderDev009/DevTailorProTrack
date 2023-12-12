using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.infraestructure.Context
{
    public class TailorProTrackContext : DbContext
    {
        public TailorProTrackContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> PRODUCT { get; set; }
        public DbSet<Type_prod> TYPE_PROD { get; set; }
    }
}
