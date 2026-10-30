using Microsoft.EntityFrameworkCore;
using Group1Blackboard.Models;

var builder = WebApplication.CreateBuilder(args);

// Render injects PORT env var; bind to it
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add EF Core SQLite DbContext
builder.Services.AddDbContext<BlackboardContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BlackboardContext")));

// Add session services (NFLTeams pattern)
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// No UseHttpsRedirection — Render terminates TLS at the edge
app.UseRouting();

// Session middleware (placed between UseRouting and UseAuthorization)
app.UseSession();

app.UseAuthorization();
app.MapStaticAssets();

// Areas route
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Custom Catalog filter route
app.MapControllerRoute(
    name: "catalog_filter",
    pattern: "Catalog/{majorId:int?}",
    defaults: new { controller = "Catalog", action = "Index" });

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
