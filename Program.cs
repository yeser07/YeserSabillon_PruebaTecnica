using Microsoft.AspNetCore.Builder;
using YeserSabillon_PruebaTecnica.Configuraciones;
using YeserSabillon_PruebaTecnica.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<DBcon>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<ClienteRepositorio>();
builder.Services.AddScoped<ProductoRepositorio>();
builder.Services.AddScoped<FacturaHeaderRepositorio>();
builder.Services.AddScoped<FacturaDetalleRepositorio>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
