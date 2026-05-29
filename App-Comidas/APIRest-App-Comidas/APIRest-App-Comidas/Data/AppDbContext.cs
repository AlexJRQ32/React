using APIRest_App_Comidas.Models;
using Microsoft.EntityFrameworkCore;
using RappiDozApp.Models;
using System.Data;
using System.Net;
using System.Reflection.Emit;

namespace APIRest_App_Comidas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Address> Addresses { get; set; } // Nombre actualizado de Ubications
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ReservedCoupon> ReservedCoupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relaciones con llaves foráneas estrictas
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.Category).WithMany().HasForeignKey(r => r.CategoryId);
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Dish>()
                .HasOne(d => d.Category).WithMany().HasForeignKey(d => d.CategoryId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Category).WithMany().HasForeignKey(o => o.CategoryId);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.PaymentMethod).WithMany().HasForeignKey(o => o.PaymentMethodId);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address).WithMany().HasForeignKey(o => o.AddressId);

            modelBuilder.Entity<Coupon>()
                .HasOne(c => c.Category).WithMany().HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<Coupon>()
                .HasOne(c => c.Order).WithMany().HasForeignKey(c => c.OrderId);
            modelBuilder.Entity<Coupon>()
                .HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId); // Relación para apartados por usuario

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order).WithMany(o => o.Items).HasForeignKey(oi => oi.OrderId);
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Dish).WithMany().HasForeignKey(oi => oi.DishId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role).WithMany().HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.User).WithMany().HasForeignKey(a => a.UserId);
        }
    }
}
