using System.IO;
using LetsChatFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using LetsChatFinal.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// SQLite location: persistent /home/App_Data on Azure, wwwroot/App_Data for local dev.
// (Azure wipes wwwroot on every deploy, so the database must live outside it.)
var homeDir = Environment.GetEnvironmentVariable("HOME");
var dataDir = string.IsNullOrEmpty(homeDir)
    ? Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "App_Data")
    : Path.Combine(homeDir, "App_Data");
Directory.CreateDirectory(dataDir);

// Register PostContext for blog posts (keep this if still in use)
builder.Services.AddDbContext<PostContext>(options =>
    options.UseSqlite($"Data Source={Path.Combine(dataDir, "blog-posts.db")}"));

// Register Identity services
builder.Services.AddDbContext<LetsChatFinalContext>(options =>
    options.UseSqlite($"Data Source={Path.Combine(dataDir, "blog-identity.db")}"));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LetsChatFinalContext>();

// Add Razor Pages for Identity
builder.Services.AddRazorPages();

var app = builder.Build();

// Create the SQLite databases on startup from the current model.
// (EnsureCreated, not Migrate: the checked-in 2022 migrations target a stale
//  "Movies" table from a tutorial and do not match the blog schema.)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    services.GetRequiredService<PostContext>().Database.EnsureCreated();
    services.GetRequiredService<LetsChatFinalContext>().Database.EnsureCreated();
}

// Configure the HTTP request pipeline
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

app.MapRazorPages();

app.Run();