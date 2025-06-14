using Microsoft.AspNetCore.Authentication.Cookies;
using Octopus.DependencyContainer;
using Octopus.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperURT));
// Agregar servicios al contenedor
builder.Services.AddControllersWithViews()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.PropertyNamingPolicy = null; // para nombres exactos como en JS
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.DependencyInjection();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/InicioSesion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.AccessDeniedPath = "/Auth/InicioSesion";
    });

var app = builder.Build();

// Middleware para evitar el almacenamiento en caché en páginas protegidas
app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    await next();
});

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
    pattern: "{controller=Auth}/{action=InicioSesion}/{id?}");

app.Run();
//dotnet ef dbcontext scaffold "Server=DESKALEJO\SQLEXPRESS;Database=Octopus;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities/Domain/DBOctopus/OctopusEntities --context AppDbContext --force --project "E:\\empresas\\Personales\\Red de mercadeo\\Proyecto\\Octopus\\Octopus\\Models\\Models\\Models.csproj"