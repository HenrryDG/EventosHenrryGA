using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventosHenrryGA.API.Models;

public partial class BdeventosContext : DbContext
{
    public BdeventosContext()
    {
    }

    public BdeventosContext(DbContextOptions<BdeventosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<TipoEvento> TipoEventos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC270CCB5C3D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(100);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Eventos__3214EC278F5B8418");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.TipoEventoId).HasColumnName("TipoEventoID");
            entity.Property(e => e.Ubicacion).HasMaxLength(200);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__Eventos__Cliente__3C69FB99");

            entity.HasOne(d => d.TipoEvento).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.TipoEventoId)
                .HasConstraintName("FK__Eventos__TipoEve__3B75D760");
        });

        modelBuilder.Entity<TipoEvento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoEven__3214EC277B3CEEB5");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NombreTipo).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
