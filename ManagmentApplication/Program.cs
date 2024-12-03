using ManagmentApplication.Data;
using ManagmentApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Configurar EPPlus para que use la licencia de "NonCommercial"
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MiContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de Identity


var app = builder.Build();

// Configuración de rutas
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();  // Asegúrate de agregar esta línea
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");  // Página de Login por defecto

app.Run();
