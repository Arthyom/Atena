using System;
using System.Collections.Generic;
using Atena.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBaseContext
{
    public partial class CaadiDbContext : DbContext
    {
        private readonly AtenaGlobalConfigs _atenaGlobalConfigs;
        public CaadiDbContext()
        {
        }

        public CaadiDbContext(DbContextOptions<CaadiDbContext> options, AtenaGlobalConfigs atenaGlobalConfigs)
            : base(options)
        {
            _atenaGlobalConfigs = atenaGlobalConfigs;
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<Visit1> Visits1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string cn = _atenaGlobalConfigs.ConnectionString;
                if (!string.IsNullOrEmpty(cn))
                {
                    optionsBuilder.UseMySql(cn, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(cn));
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Visible).HasDefaultValueSql("b'1'");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .HasConstraintName("FK_Groups_employeeNumber");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.PeriodId)
                    .HasConstraintName("FK_Groups_periodId");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasKey(e => new { e.Nua, e.GroupId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMembers_groupId");

                entity.HasOne(d => d.NuaNavigation)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.Nua)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMembers_nua");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.Property(e => e.Actual).HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Nua)
                    .HasName("PRIMARY");

                entity.Property(e => e.Visible).HasDefaultValueSql("b'1'");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.EmployeeNumber)
                    .HasName("PRIMARY");

                entity.Property(e => e.Visible).HasDefaultValueSql("b'1'");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Visible).HasDefaultValueSql("b'1'");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.Property(e => e.Visible).HasDefaultValueSql("b'1'");

                entity.HasOne(d => d.NuaNavigation)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.Nua)
                    .HasConstraintName("FK_Visit_nua");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.PeriodId)
                    .HasConstraintName("FK_Visit_periodId");
            });

            modelBuilder.Entity<Visit1>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Visible).HasDefaultValueSql("b'0'");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.Visit1s)
                    .HasForeignKey(d => d.Periodid)
                    .HasConstraintName("FK_Visits_periodid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
