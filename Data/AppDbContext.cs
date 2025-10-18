using Fast_Food_Delievery.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Fast_Food_Delievery.ViewModel;

namespace Fast_Food_Delievery.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Category → SubCategory (1 : Many)
            builder.Entity<SubCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Category → Item (1 : Many)
            builder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // SubCategory → Item (1 : Many)
            builder.Entity<Item>()
                .HasOne(i => i.SubCategory)
                .WithMany(sc => sc.Items)
                .HasForeignKey(i => i.SubCategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // Item → Cart (1 : Many)
            builder.Entity<Cart>()
                .HasOne(c => c.Item)
                .WithMany(i => i.Carts)
                .HasForeignKey(c => c.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // User → Cart (1 : Many)
            builder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderHeader → OrderDetails (1 : Many)
            builder.Entity<OrderDetails>()
                .HasOne(od => od.OrderHeader)
                .WithMany(oh => oh.OrderDetails)
                .HasForeignKey(od => od.OrderHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Item → OrderDetails (1 : Many)
            builder.Entity<OrderDetails>()
                .HasOne(od => od.Item)
                .WithMany(i => i.OrderDetails)
                .HasForeignKey(od => od.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // User → OrderHeader (1 : Many)
            builder.Entity<OrderHeader>()
                .HasOne(oh => oh.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(oh => oh.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Voucher → OrderHeader (1 : Many, optional)
            builder.Entity<OrderHeader>()
                .HasOne(oh => oh.Voucher)
                .WithMany(v => v.Orders)
                .HasForeignKey(oh => oh.VoucherId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Cart> Carts{ get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

     }
}