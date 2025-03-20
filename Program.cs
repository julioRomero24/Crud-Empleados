using Crud.Models;
using Crud.Services;
using Crud.Services.Interfaces;
using Crud.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddControllers();



//para poder usar JsonPatchDocument
builder.Services.AddControllers()
    .AddNewtonsoftJson();

//inyecta el contexto de Entity Framework con dependency injection
builder.Services.AddDbContext<NominaPreasyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddScoped<IEmpleadosServices, EmpleadosServices>();
builder.Services.AddEndpointsApiExplorer();//para visualizar el swaguer en navegador
builder.Services.AddSwaggerGen();




// Configurar AutoMapper builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Configurar servicios y agregar Logging
builder.Services.AddControllers(); 
builder.Services.AddLogging(config => 
{ 
    config.ClearProviders(); 
    config.AddConsole(); 
    config.AddDebug(); 
});

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




app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/openapi.json", "API CRUD de Empleados v1");
});


app.Run();
