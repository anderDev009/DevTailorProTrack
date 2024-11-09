using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TailorProTrack.domain.Entities;
using TailorProTrack.domain.Reports;

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
        public DbSet<PaymentExpenses> PAYMENT_EXPENSES { get; set; }
        public DbSet<PreOrder> PRE_ORDER {  get; set; }
        public DbSet<PreOrderProducts> PRE_ORDER_PRODUCTS {  get; set; }

        public DbSet<BuyInventory> BUY_INVENTORY { get; set; }
        public DbSet<BuyInventoryDetail> BUY_INVENTORY_DETAIL { get; set; }
        public DbSet<ProductColor> PRODUCTS_COLOR { get; set; }
        public DbSet<ProductSize> PRODUCTS_SIZE { get; set; }
        public DbSet<NoteCredit> NOTE_CREDIT { get; set; }
        public DbSet<NoteCreditPayment> PAYMENT_CREDIT_NOTE { get; set; }
		public DbSet<AccountDebit> ACCOUNT_DEBIT { get; set; }
        public DbSet<AccountCredit> ACCOUNT_CREDIT { get; set; }

        public DbSet<Supplier> SUPPLIERS { get; set; }

        //reportes
        public DbSet<MissedInventory> MissedInv { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //no keys - entidades de solo lectura
            #region Entidades solo lectura - Reportes
            modelBuilder.Entity<MissedInventory>().HasNoKey();
            #endregion
            #region Foreign Keys


            modelBuilder.Entity<Size>().HasOne<CategorySize>(s => s.categorySize).WithMany().HasForeignKey(s => s.FKCATEGORYSIZE);


            
            #region Compras de inventario

            modelBuilder.Entity<BuyInventory>().HasMany(x => x.Expenses)
                .WithOne(x => x.BuyInventory)
                .HasForeignKey(x => x.FK_BUY);

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
            #region Product-Color
            modelBuilder.Entity<ProductColor>().HasOne(p => p.Product)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(p => p.FK_PRODUCT);
            modelBuilder.Entity<ProductColor>().HasOne(p => p.Color)
                .WithMany(p => p.ProductColor)
                .HasForeignKey(p => p.FK_COLOR);
            #endregion
            #region Product Size
            modelBuilder.Entity<ProductSize>().HasOne(p => p.Product)
                .WithMany(p => p.ProductSizes)
                .HasForeignKey(p => p.FK_PRODUCT);
            modelBuilder.Entity<ProductSize>().HasOne(p => p.Size)
                .WithMany(p => p.ProductSize)
                .HasForeignKey(p => p.FK_SIZE);
            #endregion
            #region PreOrder Products
            modelBuilder.Entity<PreOrderProducts>()
                .HasOne(p => p.PreOrder)
                .WithMany(p => p.PreOrderProducts)
                .HasForeignKey(p => p.FK_PREORDER);

            modelBuilder.Entity<PreOrderProducts>()
                .HasOne(p => p.Product)
                .WithMany(p => p.PreOrderProducts)
                .HasForeignKey(p => p.FK_PRODUCT);

            modelBuilder.Entity<PreOrderProducts>()
                .HasOne(p => p.Size)
                .WithMany(p => p.PreOrderProducts)
                .HasForeignKey(p => p.FK_SIZE);

            modelBuilder.Entity<PreOrderProducts>()
                .HasOne(p => p.ColorPrimary)
                .WithMany(p => p.ColorPrimaryInPreOrder)
                .HasForeignKey(p => p.COLOR_PRIMARY);


            modelBuilder.Entity<PreOrderProducts>()
                .HasOne(p => p.ColorSecondary)
                .WithMany(p => p.ColorSecondaryInPreOrder)
                .HasForeignKey(p => p.COLOR_SECONDARY);
            #endregion
            #region Order
            modelBuilder.Entity<Order>()
                .HasOne(x => x.Client)
                .WithMany(x => x.Order)
                .HasForeignKey(x => x.FK_CLIENT);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.PreOrder)
                .WithMany(x => x.Order)
                .HasForeignKey(x => x.FK_PREORDER);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderProducts)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.FK_ORDER);
            #region OrderProducts
            modelBuilder.Entity<OrderProduct>()
                .HasOne(x => x.InventoryColor)
                .WithMany(x => x.OrderProducts)
                .HasForeignKey(x => x.FK_INVENTORYCOLOR);
            #endregion
            #endregion
            #region PreOrder
            modelBuilder.Entity<PreOrder>()
                .HasOne(x => x.Client)
                .WithMany(x => x.PreOrder)
                .HasForeignKey(x => x.FK_CLIENT);

            modelBuilder.Entity<PreOrder>()
                .HasMany(x => x.Sale)
                .WithOne(x => x.PreOrder)
                .HasForeignKey(x => x.FK_PREORDER);
            #endregion
            #region Expenses

            modelBuilder.Entity<PaymentExpenses>().HasOne(x => x.Expense).WithMany(x => x.PaymentsExpenses)
                .HasForeignKey(x => x.FK_EXPENSE);

            modelBuilder.Entity<PaymentExpenses>().HasOne(x => x.BankAccount).WithMany(x => x.PaymentExpenses)
                .HasForeignKey(x => x.FK_BANK_ACCOUNT);

            modelBuilder.Entity<PaymentExpenses>().HasOne(x => x.PaymentType)
                .WithMany(x => x.PaymentExpensesList)
                .HasForeignKey(x => x.FK_PAYMENT_TYPE);
            #endregion
            #region Inventory
            modelBuilder.Entity<Inventory>()
                .HasMany(x => x.InventoryColor)
                .WithOne(x => x.Inventory)
                .HasForeignKey(x => x.FK_INVENTORY);

            #endregion
            #region NoteCredit
            modelBuilder.Entity<NoteCredit>()
				.HasOne(x => x.Client)
				.WithMany(x => x.NoteCredit)
				.HasForeignKey(x => x.FK_CLIENT);


            #endregion
            #region Cuenta de  credito

            modelBuilder.Entity<AccountCredit>(builder =>
            {
	            builder.HasOne(x => x.BankAccount)
		            .WithMany(x => x.AccountCredit)
		            .HasForeignKey(x => x.FK_BANK_ACC);

                builder.HasOne(x => x.Expense)
					.WithMany(x => x.AccountCredit)
					.HasForeignKey(x => x.FK_EXPENSE);

			});

			#endregion
			#region Cuenta de debito

			modelBuilder.Entity<AccountDebit>(builder =>
			{
				builder.HasOne(x => x.BankAccount)
					.WithMany(x => x.AccountDebit)
					.HasForeignKey(x => x.FK_BANK_ACC);

				builder.HasOne(x => x.Payment)
					.WithMany(x => x.AccountDebit)
					.HasForeignKey(x => x.FK_PAYMENT);

			});

			#endregion

			#endregion

        }
    }
}
