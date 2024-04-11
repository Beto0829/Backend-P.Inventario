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
        public DbSet<Entrada> Entradas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)            // Un producto tiene una categoría
                .WithMany(c => c.Productos)          // Una categoría puede tener varios productos
                .HasForeignKey(p => p.IdCategoria)   // La clave foránea en productos apunta a IdCategoria
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

            //relaciones del modelo entradas, aca abajo

            modelBuilder.Entity<Entrada>()
                .HasOne(e => e.Categoria)            // Una compra tiene una categoría
                .WithMany(cat => cat.Compras)        // Una categoría puede tener varias compras
                .HasForeignKey(e => e.IdCategoria)   // La clave foránea en compras apunta a IdCategoria
                .IsRequired();

            modelBuilder.Entity<Entrada>()
               .HasOne(e => e.Producto)            // Una compra tiene un producto
               .WithMany(p => p.Compras)           // Un producto puede tener varias compras
               .HasForeignKey(e => e.IdProducto)   // La clave foránea en compras apunta a IdProducto
               .IsRequired();

            modelBuilder.Entity<Entrada>()
              .HasOne(e => e.proveedor)            // Una compra tiene un proveedor
              .WithMany(p => p.Compras)            // Un proveedor puede tener varias compras
              .HasForeignKey(e => e.IdProveedor)   // La clave foránea en compras apunta a IdProveedor
              .IsRequired();


            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 1, Nombre = "Sin categoria" });
        }
    }
}
