using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.infraestructure.Context
{
   
    public class TailorProTrackContext : DbContext
    {
        public TailorProTrackContext(DbContextOptions options) : base(options) { }


        public DbSet<Product> PRODUCT { get; set; }
        public DbSet<Type_prod> TYPE_PROD { get; set; }
        public DbSet<Size> SIZE { get; set; }
        public DbSet<Inventory> INVENTORY { get; set; }
        public DbSet<InventoryColor> INVENTORY_COLOR { get; set; }
        public DbSet<Color> COLOR {  get; set; } 
        public DbSet<Client> CLIENT { get; set; }
        public DbSet<Phone> PHONE { get; set; } 
        public DbSet<Order> ORDERS { get; set; }
        public DbSet<OrderProduct> ORDER_PRODUCTS { get; set; }
    }
}
