using Cros.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cros.DataAccess
{
    public class CrosDbContext(DbContextOptions<CrosDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCustomer(modelBuilder.Entity<Customer>());
        }
        //
        // Helper methods
        //
        private void ConfigureCustomer(EntityTypeBuilder<Customer> entityBuilder)
        {
            entityBuilder.HasIndex(e => new { e.CustomerNo, e.DeletedAt })
                .IsUnique();

            entityBuilder.HasQueryFilter(e => e.DeletedAt == null);
        }
    }
}