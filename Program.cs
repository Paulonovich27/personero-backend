using Microsoft.EntityFrameworkCore;
using Personero.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la cadena de conexión (PostgreSQL)
// Nota: Luego pondremos tu cadena de Neon en appsettings.json
var connectionString = builder.Configuration.GetConnectionString("NeonConnection")
                       ?? "Host=localhost;Database=PersonerosDB;Username=postgres;Password=tu_password";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. Configurar CORS (Para permitir que React en el puerto 5173 envíe datos)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();  <-- Comentar esta línea

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();   <-- Comentar esta línea
    // app.UseSwaggerUI(); <-- Comentar esta línea
}

app.UseHttpsRedirection();

// 3. Activar el CORS antes de la autorización
app.UseCors("AllowReactApp");

app.UseAuthorization();
app.MapControllers();
app.Run();