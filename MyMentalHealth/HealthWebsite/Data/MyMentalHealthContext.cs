using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MyMentalHealth.Data
{
    public partial class MyMentalHealthContext : DbContext
    {
        public MyMentalHealthContext()
        {
        }

        public MyMentalHealthContext(DbContextOptions<MyMentalHealthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=Uzbas-MacBook-Air.local; database=MyMentalHealth; Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId)
                    .HasColumnType("int(11)")
                    .HasColumnName("admin_id");

                entity.Property(e => e.AEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("a_email");

                entity.Property(e => e.AName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("a_name");

                entity.Property(e => e.APassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("a_password");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.Property(e => e.UEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("u_email");

                entity.Property(e => e.UName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("u_name");

                entity.Property(e => e.UPassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("u_password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
