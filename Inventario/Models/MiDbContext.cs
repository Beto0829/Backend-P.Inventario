using Microsoft.EntityFrameworkCore;

namespace Inventario.Models
{
    public class MiDbContext : DbContext
    {
        public MiDbContext(DbContextOptions<MiDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proveedor> Proveedors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)            // Una nota tiene una categoría
                .WithMany(c => c.Productos)          // Una categoría puede tener varias notas
                .HasForeignKey(p => p.IdCategoria)   // La clave foránea en Nota apunta a IdCategoria
                .IsRequired();

            modelBuilder.Entity<Cliente>()
               .HasIndex(c => c.Correo)
               .IsUnique();

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Celular)
                .IsUnique();

            modelBuilder.Entity<Proveedor>()
               .HasIndex(p => p.Correo)
               .IsUnique();

            modelBuilder.Entity<Proveedor>()
                .HasIndex(p => p.Celular)
                .IsUnique();

            modelBuilder.Entity<Categoria>()
               .HasIndex(c => c.Nombre)
               .IsUnique();

            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 1, Nombre = "Sin categoria" });
        }
    }
}
