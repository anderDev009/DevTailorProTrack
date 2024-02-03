using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TailorProTrack.domain.Entities;

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
        public DbSet<Payment> PAYMENT {  get; set; }
        public DbSet<PaymentType> PAYMENT_TYPE { get; set; }
        public DbSet<Sales> SALES { get; set; }
        public DbSet<CategorySize> CATEGORYSIZE {  get; set; }
        public DbSet<Bank> BANK { get; set; }
        public DbSet<BankAccount> BANK_ACCOUNT { get; set; }
        public DbSet<Expenses> EXPENSES { get; set; }
        public DbSet<PreOrder> PRE_ORDER {  get; set; }
        public DbSet<PreOrderProducts> PRE_ORDER_PRODUCTS {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region
            modelBuilder.Entity<Size>().HasOne<CategorySize>(s => s.categorySize).WithMany().HasForeignKey(s => s.FKCATEGORYSIZE);
            #endregion

        }
    }
}
