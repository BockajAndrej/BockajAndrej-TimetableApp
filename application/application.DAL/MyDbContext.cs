using System;
using System.Collections.Generic;
using application.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace application.DAL;

public partial class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Cp> Cps { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__City__3213E83F176C70CD");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Cp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CP__3213E83F2C958E69");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Cps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_emp");

            entity.HasOne(d => d.IdEndCityNavigation).WithMany(p => p.CpIdEndCityNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_endCity");

            entity.HasOne(d => d.IdStartCityNavigation).WithMany(p => p.CpIdStartCityNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_startCity");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83F59A0A4FC");

            entity.Property(e => e.BirthNumber).IsFixedLength();
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transpor__3213E83FCCA4B586");

            entity.HasOne(d => d.IdCpNavigation).WithMany(p => p.Transports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cp");

            entity.HasOne(d => d.IdVehicleNavigation).WithMany(p => p.Transports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_veh");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicle__3213E83FC6432934");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
