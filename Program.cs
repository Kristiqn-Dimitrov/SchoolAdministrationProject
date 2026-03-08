using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary.Data;
using SchoolLibrary.Models;
using SchoolLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Missing connection string 'DefaultConnection'.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Denied";
});

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ScheduleGenerator>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("LibraryAdmin", p =>
        p.RequireRole(SchoolLibrary.Data.DbSeeder.AdminRole, SchoolLibrary.Data.DbSeeder.LibrarianRole));

});
builder.Services.AddAuthorization(options =>
{
    // Library module: Admin OR Librarian
    options.AddPolicy("LibraryStaff", p =>
        p.RequireRole(DbSeeder.AdminRole, DbSeeder.LibrarianRole));
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await DbSeeder.SeedAsync(app.Services);

app.Run();
