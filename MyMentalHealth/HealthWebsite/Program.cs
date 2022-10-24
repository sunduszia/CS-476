using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyMentalHealth.Data;

var builder = WebApplication.CreateBuilder(args);
/*builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("UserContext") ?? throw new InvalidOperationException("Connection string 'UserContext' not found.")));
builder.Services.AddDbContext<MyMentalHealthContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("MyMentalHealthContext") ?? throw new InvalidOperationException("Connection string 'MyMentalHealthContext' not found.")));
*/
string ConnString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<MymentalhealthContext>(options => options.UseMySQL(ConnString));

// Add services to the container.
builder.Services.AddControllersWithViews();



//string ConnectString = builder.Configuration.GetConnectionString("Default");
//builder.Services.AddDbContext<UserContext>(options => options.UseMySQL(ConnectString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

