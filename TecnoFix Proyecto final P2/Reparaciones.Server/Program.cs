using Microsoft.EntityFrameworkCore;
using Reparaciones.Domain.Interfaces;
using Reparaciones.Infrastructure.Context;
using Reparaciones.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ReparacionesContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ITecnicoRepository, TecnicoRepository>();
builder.Services.AddScoped<IElectrodomesticoRepository, ElectrodomesticoRepository>();
builder.Services.AddScoped<IRepuestoRepository, RepuestoRepository>();
builder.Services.AddScoped<IReparacionRepository, ReparacionRepository>();
builder.Services.AddScoped<IGarantiaRepository, GarantiaRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ReparacionesContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Control de Reparaciones API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
