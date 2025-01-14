using Dot_Net_ECommerceWeb.Model;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.DBContext;

public class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Logs> Logs { get; set; }
    public DbSet<InventoryDetails> InventoryDetails { get; set; }
    public DbSet<Inventories> Inventories { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Logs>();
        modelBuilder.Entity<ProductImage>();
        modelBuilder.Entity<User>()
            .Property(u => u.Gender)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .Property(u => u.TypeLogin)
            .HasConversion<string>();
        // Product and ProductImage have a one-to-one relationship
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductImage)
            .WithOne(pi => pi.Product).HasForeignKey<ProductImage>(pi => pi.Id);


     

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .IsRequired()
            .HasConversion<string>(); // If you decide to use enum, change this accordingly
       
        modelBuilder.Entity<OrderDetail>()
            .HasOne(o => o.Product)
            .WithMany(p => p.OrderDetail)
            .HasForeignKey(o => o.ProductId);
        modelBuilder.Entity<Order>().HasMany(o => o.OrderDetails).WithOne(o => o.Order).HasForeignKey(o => o.OrderId);
        modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products);
        modelBuilder.Entity<Order>().HasOne(o=>o.user).WithMany(o=>o.Orders);
        modelBuilder.Entity<Product>().HasMany(p=>p.InventoryDetail).WithOne(i=>i.product).HasForeignKey(i=>i.ProductId);
        modelBuilder.Entity<InventoryDetails>().HasOne(i=>i.product).WithMany(p=>p.InventoryDetail);
        modelBuilder.Entity<Inventories>().HasMany(i=>i.inventory_details).WithOne(i=>i.Inventories).HasForeignKey(i=>i.Id);
    }
}