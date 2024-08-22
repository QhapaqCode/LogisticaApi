using Microsoft.EntityFrameworkCore;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Infrastructure.Data
{
    public class RetailProductContext : DbContext
    {
        public RetailProductContext(DbContextOptions<RetailProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Logistic> Logistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RetailProductContext).Assembly);
        }
    }
}