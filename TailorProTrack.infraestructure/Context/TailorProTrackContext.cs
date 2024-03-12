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

        public DbSet<BuyInventory> BUY_INVENTORY { get; set; }
        public DbSet<BuyInventoryDetail> BUY_INVENTORY_DETAIL { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Foreign Keys
            modelBuilder.Entity<Size>().HasOne<CategorySize>(s => s.categorySize).WithMany().HasForeignKey(s => s.FKCATEGORYSIZE);

            #region Compras de inventario
            modelBuilder.Entity<BuyInventory>()
                .HasMany(b => b.Details)
                .WithOne(d => d.BuyInventory)
                .HasForeignKey(d => d.FK_BUY_INVENTORY);

            modelBuilder.Entity<BuyInventoryDetail>()
                .HasOne(b => b.Product)
                .WithMany(p => p.productsInBuys)
                .HasForeignKey(b => b.FK_PRODUCT);

            modelBuilder.Entity<BuyInventoryDetail>()
                .HasOne(b => b.Size)
                .WithMany(Size => Size.sizeInBuys)
                .HasForeignKey(b => b.FK_SIZE);

            modelBuilder.Entity<BuyInventoryDetail>()
                .HasOne(b => b.ColorPrimary)
                .WithMany(c => c.ColorPrimaryInBuys)
                .HasForeignKey(b => b.COLOR_PRIMARY);

            modelBuilder.Entity<BuyInventoryDetail>()
              .HasOne(b => b.ColorSecondary)
              .WithMany(c => c.ColorSecondaryInBuys)
              .HasForeignKey(b => b.COLOR_SECONDARY);
            #endregion
            #region product
            modelBuilder.Entity<Product>().HasOne(p => p.Type).WithMany(t => t.Products).HasForeignKey(p => p.FK_TYPE);
            #endregion
            #endregion

        }
    }
}
