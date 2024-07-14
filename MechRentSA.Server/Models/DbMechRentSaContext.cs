using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MechRentSA.Server.Models;

public partial class DbMechRentSaContext : DbContext
{
    public DbMechRentSaContext()
    {
    }

    public DbMechRentSaContext(DbContextOptions<DbMechRentSaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Excavator> Excavators { get; set; }

    public virtual DbSet<ExcavatorWorkLog> ExcavatorWorkLogs { get; set; }

    public virtual DbSet<PublicWork> PublicWorks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Excavator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Excavato__3214EC0712237563");

            entity.ToTable("Excavator");

            entity.Property(e => e.HourlyRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<ExcavatorWorkLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Excavato__3214EC075134C50B");

            entity.ToTable("ExcavatorWorkLog");

            entity.Property(e => e.WorkDate).HasColumnType("datetime");

            entity.HasOne(d => d.Excavator).WithMany(p => p.ExcavatorWorkLogs)
                .HasForeignKey(d => d.ExcavatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Excavator__Excav__4E88ABD4");

            entity.HasOne(d => d.PublicWork).WithMany(p => p.ExcavatorWorkLogs)
                .HasForeignKey(d => d.PublicWorkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Excavator__Publi__4D94879B");
        });

        modelBuilder.Entity<PublicWork>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PublicWo__3214EC079F0F4578");

            entity.ToTable("PublicWork");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
