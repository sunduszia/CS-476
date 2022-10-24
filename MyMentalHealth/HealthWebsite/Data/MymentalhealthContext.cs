using System;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Models;
using Microsoft.EntityFrameworkCore.Design;


namespace MyMentalHealth.Data;



public class MymentalhealthContext : DbContext
{
    public DbSet<AdminModel> AdminModels { get; set; }
    public DbSet<UserModel> UserModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Server=localhost;port=3306;database=Mymentalhealth;user=root;password=my-secret-pw");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AdminModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserName).IsRequired();
            entity.Property(e => e.Password).IsRequired();

        });

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.DateOfBirth);
            entity.Property(e => e.UserName);
            entity.Property(e => e.Password).IsRequired();

        });

    }
}
