using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlmacenWS.Models
{
    public partial class Almacen_dbContext : DbContext
    {
        public Almacen_dbContext()
        {
        }

        public Almacen_dbContext(DbContextOptions<Almacen_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-L5AHUMH\\SQLEXPRESS;Database=Almacen_db;Trusted_Connection=True; User=sa; Password=maverick#23");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__CD54BC5A29096CB0");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.DescripcionCategoria)
                    .HasColumnName("descripcionCategoria")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCategoria)
                    .IsRequired()
                    .HasColumnName("nombreCategoria")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__FF341C0D6E9DE6D4");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.CantidadExistencia)
                    .HasColumnName("cantidadExistencia")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.CodigoProducto)
                    .IsRequired()
                    .HasColumnName("codigoProducto")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DescripcionProducto)
                    .HasColumnName("descripcionProducto")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.MarcaProducto)
                    .HasColumnName("marcaProducto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreProducto)
                    .IsRequired()
                    .HasColumnName("nombreProducto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioCosto)
                    .HasColumnName("precioCosto")
                    .HasColumnType("decimal(16, 4)");

                entity.Property(e => e.PresentacionProducto)
                    .HasColumnName("presentacionProducto")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Producto__id_cat__38996AB5");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__4E3E04AD2DF526BB");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
