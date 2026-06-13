using DataAccess;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

Env.Load();
var builder = WebApplication.CreateBuilder(args);


//variables de entorno archivo .env
var connectionString =
    $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
    $"Database={Environment.GetEnvironmentVariable("DB_DATABASE")};" +
    $"User Id={Environment.GetEnvironmentVariable("DB_USER")};" +
    $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
    $"TrustServerCertificate={Environment.GetEnvironmentVariable("DB_TRUST_SERVER_CERTIFICATE")};" +
    $"Encrypt={Environment.GetEnvironmentVariable("DB_ENCRYPT")};";


// Add services to the container.
builder.Services.AddDbContext<GestionEventosContext>(options =>
{
    options.UseSqlServer(connectionString);
});





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
