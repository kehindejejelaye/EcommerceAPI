using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Data;

public class EcommerceContext : IdentityDbContext<ApplicationUser>
{
    public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductItem> ProductItems { get; set; }
    public DbSet<ProductItemVariantOption> ProductItemVariantOptions { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Variant> Variants { get; set; }
    public DbSet<VariantOption> VariantOptions { get; set; }
    public DbSet<WishList> WishLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductItem>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderItem>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
    }

}
