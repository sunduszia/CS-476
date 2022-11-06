using System;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Models;
using Microsoft.EntityFrameworkCore.Design;


namespace MyMentalHealth.Models;



public class MymentalhealthContext : DbContext
{

    public MymentalhealthContext(DbContextOptions<MymentalhealthContext> options)
        : base(options) { }

    public DbSet<MentalHealthIssues> MentalHealthIssues { get; set; }
    public DbSet<ResourceTypes> ResourceTypes { get; set; }
    public DbSet<IssueItems> IssueItems { get; set; }
    public DbSet<Contents> Contents { get; set; }
    
}
