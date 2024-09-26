using CapaDatos;
using Microsoft.EntityFrameworkCore;
using CapaNegocio.servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//servicios de la capa de negocio
builder.Services.AddScoped<SerieServicio>();
builder.Services.AddScoped<ProductoraServicio>();
builder.Services.AddScoped<GeneroServicio>();

//inyectando en contexto de la BD
builder.Services.AddDbContext<practicasP3Context>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));

});


var app = builder.Build();

//creando una instancia para crear la DB
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<practicasP3Context>();
    context.Database.Migrate();
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Series}/{action=Series}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Productoras}/{action=Productoras}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Generos}/{action=Generos}/{id?}");




app.Run();
