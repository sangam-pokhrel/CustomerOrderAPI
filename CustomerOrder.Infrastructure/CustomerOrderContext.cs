using CustomerOrder.Common.Enums;
using CustomerOrder.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrder.Infrastructure
{
    public class CustomerOrderContext : DbContext
    {
        public CustomerOrderContext(DbContextOptions<CustomerOrderContext> options) : base(options)
        {
        }

        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().Property(x => x.Id).HasMaxLength(50);
            modelBuilder.Entity<Order>().HasMany(x => x.OrderProducts).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<ProductType>().HasKey(x => x.Id);
            modelBuilder.Entity<ProductType>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<ProductType>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<ProductType>().HasMany(x => x.OrderProducts).WithOne(x => x.ProductType).HasForeignKey(x => x.ProductTypeId);

            modelBuilder.Entity<OrderProduct>().HasKey(x => new { x.OrderId, x.ProductTypeId });
            modelBuilder.Entity<OrderProduct>().Property(x => x.OrderId).HasMaxLength(50);

            //seed data
            modelBuilder.Entity<ProductType>()
                .HasData(
                    new ProductType((short)ProductTypeEnum.Photobook, nameof(ProductTypeEnum.Photobook), 19, 1),
                    new ProductType((short)ProductTypeEnum.Calendar, nameof(ProductTypeEnum.Calendar), 10, 1),
                    new ProductType((short)ProductTypeEnum.Canvas, nameof(ProductTypeEnum.Canvas), 16, 1),
                    new ProductType((short)ProductTypeEnum.Cards, nameof(ProductTypeEnum.Cards), 4.7m, 1),
                    new ProductType((short)ProductTypeEnum.Mug, nameof(ProductTypeEnum.Mug), 94, 4)
                 );
        }
    }
}