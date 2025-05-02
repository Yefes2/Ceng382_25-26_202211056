using Microsoft.EntityFrameworkCore;
using RazorPagesProject.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// 1) Enable sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

// 2) Add DbContext with SQL Server
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SchoolDbConnection")
    )
);

var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapRazorPages();
app.Run();
