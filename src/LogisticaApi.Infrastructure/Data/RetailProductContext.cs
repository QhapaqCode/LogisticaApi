using Microsoft.EntityFrameworkCore;
using RetailProductMicroservice.Domain.Entities;

namespace RetailProductMicroservice.Infrastructure.Data
{
    public class RetailProductContext : DbContext
    {
        public RetailProductContext(DbContextOptions<RetailProductContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Almacen> Almacenes { get; set; }

        public DbSet<Serializable> Serializables { get; set; }

        public DbSet<Existencia> Existencias { get; set; }

        public DbSet<Anaquel> Anaqueles { get; set; }

        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RetailProductContext).Assembly);

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Almacen)
                .WithMany()
                .HasForeignKey(m => m.AlmacenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.Cantidad)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Anaquel)
                .WithMany()
                .HasForeignKey(m => m.AnaquelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Producto)
                .WithMany()
                .HasForeignKey(m => m.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Anaquel>()
                .HasOne(a => a.Almacen)
                .WithMany()
                .HasForeignKey(a => a.AlmacenId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}