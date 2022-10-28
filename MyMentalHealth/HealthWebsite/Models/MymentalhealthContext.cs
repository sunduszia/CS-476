using System;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Models;
using Microsoft.EntityFrameworkCore.Design;


namespace MyMentalHealth.Models;



public class MymentalhealthContext : DbContext
{

    public MymentalhealthContext(DbContextOptions<MymentalhealthContext> options)
        : base(options) { }

    public DbSet<AdminModel> AdminModels { get; set; }
    public DbSet<UserModel> UserModels { get; set; }

    
}
