using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class GestionEventosContext : DbContext
{
    public GestionEventosContext()
    {
    }

    public GestionEventosContext(DbContextOptions<GestionEventosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activities> Activities { get; set; }

    public virtual DbSet<Categories> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activities>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activiti__3213E83FB082C228");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategorieId).HasColumnName("categorieId");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.NameActivitie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nameActivitie");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.StarDate).HasColumnName("starDate");

            entity.HasOne(d => d.Categorie).WithMany(p => p.Activities)
                .HasForeignKey(d => d.CategorieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_categorie");
        });

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3213E83F823DBA09");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NameCategory)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nameCategory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
