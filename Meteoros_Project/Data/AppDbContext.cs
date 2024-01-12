using Meteoros_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Meteoros_Project.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerProduct> CustomersProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomerProduct>()
                .HasKey(cp => new { cp.CustomerId, cp.ProductId });

            modelBuilder.Entity<CustomerProduct>()
                .HasOne(cp => cp.Customer)
                .WithMany(c => c.CustomerProducts)
                .HasForeignKey(cp => cp.CustomerId);

            modelBuilder.Entity<CustomerProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.CustomerProducts)
                .HasForeignKey(cp => cp.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
