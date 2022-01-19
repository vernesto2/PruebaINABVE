using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models
{
    public partial class INABVEContext : DbContext
    {
        public INABVEContext()
        {
        }

        public INABVEContext(DbContextOptions<INABVEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Beneficio> Beneficio { get; set; }
        public virtual DbSet<BeneficiosVeteranos> BeneficiosVeteranos { get; set; }
        public virtual DbSet<Veterano> Veterano { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beneficio>(entity =>
            {
                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.FechaCreado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1900)-(1))-(1))");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<BeneficiosVeteranos>(entity =>
            {
                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1900)-(1))-(1))");

                entity.HasOne(d => d.IdBeneficioNavigation)
                    .WithMany(p => p.BeneficiosVeteranos)
                    .HasForeignKey(d => d.IdBeneficio)
                    .HasConstraintName("FK_BeneficiosVeteranos_Beneficio");

                entity.HasOne(d => d.IdVeteranoNavigation)
                    .WithMany(p => p.BeneficiosVeteranos)
                    .HasForeignKey(d => d.IdVeterano)
                    .HasConstraintName("FK_BeneficiosVeteranos_Veterano");
            });

            modelBuilder.Entity<Veterano>(entity =>
            {
                entity.HasIndex(e => e.Carnet)
                    .HasName("Unique_Carnet");

                entity.HasIndex(e => e.Dui)
                    .HasName("Unique_DUI")
                    .IsUnique();

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Carnet).HasMaxLength(25);

                entity.Property(e => e.Dui)
                    .HasColumnName("DUI")
                    .HasMaxLength(10);

                entity.Property(e => e.FechaCreado)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1900)-(1))-(1))");

                entity.Property(e => e.PrimerApellido).HasMaxLength(25);

                entity.Property(e => e.PrimerNombre).HasMaxLength(25);

                entity.Property(e => e.SegundoApellido).HasMaxLength(25);

                entity.Property(e => e.SegundoNombre).HasMaxLength(25);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
