using Microsoft.EntityFrameworkCore;

namespace EcomWebAPIServer2.Models
{
    public class EcomContext : DbContext
    {
        public EcomContext() { }
        public EcomContext(DbContextOptions<EcomContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}
