
using ECommerceAPI.Entities;
using ECommerceAPI.Entities.PivotTables;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.APIContext;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) {}

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //One brand to many product//
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId)
            .IsRequired(false);

        //Many product to many category//
        modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });

        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductCategories)
            .HasForeignKey(pc => pc.ProductId)
            .IsRequired(false);

        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(pc => pc.CategoryId)
            .IsRequired(false);

        //Many product to many order//
        modelBuilder.Entity<ProductOrder>()
            .HasKey(po => new { po.ProductId, po.OrderId });

        modelBuilder.Entity<ProductOrder>()
            .HasOne(po => po.Product)
            .WithMany(p => p.ProductOrders)
            .HasForeignKey(po => po.ProductId)
            .IsRequired(false);

        modelBuilder.Entity<ProductOrder>()
            .HasOne(po => po.Order)
            .WithMany(o => o.ProductOrders)
            .HasForeignKey(po => po.OrderId)
            .IsRequired(false);

        //One user to many order//
        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .IsRequired(false);

        //Soft delete filters//
        modelBuilder.Entity<Brand>()
            .HasQueryFilter(b => !b.IsDeleted);
        modelBuilder.Entity<Category>()
            .HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<User>()
            .HasQueryFilter(u => !u.IsDeleted);

        //Decimal attributes default declerations
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,5)");

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalPrice)
            .HasColumnType("decimal(18,5)");

    }
    
}