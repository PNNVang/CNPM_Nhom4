using Dot_Net_ECommerceWeb.Model;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.DBContext;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }

    public DbSet<users> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductImage>();
        modelBuilder.Entity<users>()
            .Property(u => u.Gender)
            .HasConversion<string>();

        modelBuilder.Entity<users>()
            .Property(u => u.TypeLogin)
            .HasConversion<string>();
        // Product and ProductImage have a one-to-one relationship
        // modelBuilder.Entity<Product>()
        //     .HasOne(p => p.ProductImage)
        //     .WithOne(pi => pi.Product).HasForeignKey<ProductImage>(p => p.Id);


        // Order Configuration
        modelBuilder.Entity<Order>()
            .HasKey(o => o.Id);

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .IsRequired()
            .HasConversion<string>(); // If you decide to use enum, change this accordingly

        // OrderDetail Configuration
        modelBuilder.Entity<OrderDetail>()
            .HasKey(od => new { od.OrderId, od.ProductId }); // Composite Key

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);
        // modelBuilder.Entity<Product>()
        //     .HasMany(p => p.OrderDetail)
        //     .WithOne(o => o.Product)
        //     .HasForeignKey(o => o.ProductId);
        modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products);
        // .HasForeignKey(p => p.CategoryId);
        // modelBuilder.Entity<OrderDetail>()
        //     .HasOne(od => od.Product)
        //     .WithMany() // Assuming Product has no navigation property to OrderDetail
        //     .HasForeignKey(od => od.ProductId);
        // modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
    }
}