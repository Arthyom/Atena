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
        public virtual DbSet<Visit> Visits { get; set; }

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
                entity.Property(e => e.Employeenumber).HasDefaultValueSql("''");

                entity.Property(e => e.Identifier).HasDefaultValueSql("''");

                entity.Property(e => e.Learningunit).HasDefaultValueSql("''");

                entity.Property(e => e.Level).HasDefaultValueSql("''");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.Periodid)
                    .HasConstraintName("Groups_Periods_FK");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasOne(d => d.Group)
                    .WithMany()
                    .HasForeignKey(d => d.Groupid)
                    .HasConstraintName("GroupMembers_Groups_FK");

                entity.HasOne(d => d.NuaNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Nua)
                    .HasConstraintName("GroupMember_Stuents_FK");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Nua)
                    .HasName("PRIMARY");

                entity.Property(e => e.Nua).HasDefaultValueSql("''");

                entity.Property(e => e.Email).HasDefaultValueSql("''");

                entity.Property(e => e.Firstlastname).HasDefaultValueSql("''");

                entity.Property(e => e.Gender).HasDefaultValueSql("''");

                entity.Property(e => e.Name).HasDefaultValueSql("''");

                entity.Property(e => e.Program).HasDefaultValueSql("''");

                entity.Property(e => e.Secondlastname).HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.Employeenumber)
                    .HasName("PRIMARY");

                entity.Property(e => e.Employeenumber).HasDefaultValueSql("''");

                entity.Property(e => e.Email).HasDefaultValueSql("''");

                entity.Property(e => e.Firstlastname).HasDefaultValueSql("''");

                entity.Property(e => e.Gender).HasDefaultValueSql("''");

                entity.Property(e => e.Name).HasDefaultValueSql("''");

                entity.Property(e => e.Secondlastname).HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.Periodid)
                    .HasConstraintName("Visits_Periods");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
