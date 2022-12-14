using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyMentalHealth.Controllers;
using MyMentalHealth.Models;
using MyMentalHealth.Interface;
using MyMentalHealth.Observers;
using NuGet.Protocol.Core.Types;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
    //options.AccessDeniedPath = "/path/unauthorized";
    options.LoginPath = "/User/Login";
});

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IIssueItemsService, IssueItemService>();

//builder.Services.AddScoped<IMediator, ConcreteMediator>();
//builder.Services.AddScoped<Colleague, MentalHealthIssues>();
//builder.Services.AddScoped<Colleague, IssueItems>();
//builder.Services.AddScoped<IMediator, ConcreteMediator>();




//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<MymentalhealthContext>(options =>
//    options.UseSqlServer(connectionString));


builder.Services.AddDbContext<MymentalhealthContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

