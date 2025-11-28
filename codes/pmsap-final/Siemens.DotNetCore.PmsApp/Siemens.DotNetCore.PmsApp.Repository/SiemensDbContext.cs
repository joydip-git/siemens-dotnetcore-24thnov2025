using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Siemens.DotNetCore.PmsApp.Repository
{
    public class SiemensDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=JOYDIP-PC\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Initial Catalog=siemensdb;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Product> productModelBuilder = modelBuilder
                .Entity<Product>();

            productModelBuilder
                .ToTable("products")
                .HasKey(p => p.ProductId);

            productModelBuilder
                .Property<int>(p => p.ProductId)
                .HasColumnName("productid")
                .HasColumnType("int")
                .IsRequired();

            productModelBuilder
                .Property<string>(p => p.ProductName)
                .HasColumnName("productname")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            productModelBuilder
                .Property<decimal?>(p => p.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(18,2)")
                .IsRequired(false)
                .HasDefaultValue(0.0m);

            productModelBuilder
                .Property<string?>(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            productModelBuilder
                .HasData(
                    new Product
                    {
                        ProductId = 1,
                        ProductName = "Laptop",
                        Price = 1200.00m,
                        Description = "A high-performance laptop."
                    },
                    new Product
                    {
                        ProductId = 2,
                        ProductName = "Smartphone",
                        Price = 800.00m,
                        Description = "A latest model smartphone."
                    },
                    new Product
                    {
                        ProductId = 3,
                        ProductName = "Tablet",
                        Price = 500.00m,
                        Description = "A lightweight tablet."
                    }
                );
        }
    }
}
