using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System;
using System.Configuration;
using webBotica2.Models;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8090";

// Configurar Kestrel antes de crear la app
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// 1. MVC + Razor
builder.Services.AddControllersWithViews();

// ✅ 2. Session
builder.Services.AddSession();

// 3. EF Core
builder.Services.AddDbContext<MiAngelitoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4. Autenticación por cookies
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.LoginPath = "/Login/Index";   // formulario
        opts.AccessDeniedPath = "/Login/Index";   // sin permiso
        opts.ExpireTimeSpan = TimeSpan.FromHours(8);
        opts.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpClient();

var app = builder.Build();
RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

// ─────────────── Pipeline ───────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Middleware de sesión
app.UseSession();

app.UseAuthentication();   // primero autenticación
app.UseAuthorization();    // luego autorización

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
