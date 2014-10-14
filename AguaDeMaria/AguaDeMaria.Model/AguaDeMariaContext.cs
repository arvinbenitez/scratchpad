namespace AguaDeMaria.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AguaDeMariaContext : DbContext
    {
        public AguaDeMariaContext()
            : base("name=AguaDeMariaContext")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<DeliveryReceipt> DeliveryReceipts { get; set; }
        public virtual DbSet<DeliveryReceiptDetail> DeliveryReceiptDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<RefStatus> RefStatus { get; set; }
        public virtual DbSet<ReturnReceipt> ReturnReceipts { get; set; }
        public virtual DbSet<ReturnReceiptDetail> ReturnReceiptDetails { get; set; }
        public virtual DbSet<SalesInvoice> SalesInvoices { get; set; }
        public virtual DbSet<SalesInvoiceDetail> SalesInvoiceDetails { get; set; }

        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        //public virtual DbSet<UserAuth> UserAuths { get; set; }
        //public virtual DbSet<UserOAuthProvider> UserOAuthProviders { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<PickupSlip> PickupSlips { get; set; }
        public virtual DbSet<PickupSlipDetail> PickupSlipDetails { get; set; }

        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<InventorySummary> InventorySummaries { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = true;

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.DeliveryReceipts)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ReturnReceipts)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.SalesInvoices)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.PickupSlips)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerType>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.CustomerType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeliveryReceipt>()
                .HasMany(e => e.DeliveryReceiptDetails)
                .WithRequired(e => e.DeliveryReceipt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.DeliveryReceipts)
                .WithOptional(e=> e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.BasePrice)
                .HasPrecision(9, 2);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.DeliveryReceiptDetails)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.ReturnReceiptDetails)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.SalesInvoiceDetails)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.PickupSlipDetails)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReturnReceipt>()
                .HasMany(e => e.ReturnReceiptDetails)
                .WithRequired(e => e.ReturnReceipt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReturnReceiptDetail>()
                .Property(e => e.Quantity)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesInvoice>();

            modelBuilder.Entity<SalesInvoice>()
                .HasMany(e => e.SalesInvoiceDetails)
                .WithRequired(e => e.SalesInvoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalesInvoiceDetail>()
                .Property(e => e.Quantity)
                .HasPrecision(9, 2);

            modelBuilder.Entity<SalesInvoiceDetail>()
                .Property(e => e.Price)
                .HasPrecision(9, 2);

            modelBuilder.Entity<PickupSlip>()
                .HasMany(e => e.PickupSlipDetails)
                .WithRequired(e => e.PickupSlip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Inventory>()
                .Property(e => e.TransactionType)
                .IsUnicode(false);


        }
    }
}
