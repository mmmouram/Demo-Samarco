using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Config;
using MyApp.Repository;
using MyApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Configurar a conexão com o SQL Server
// Altere a connection string conforme necessário
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Server=localhost;Database=MyAppDb;Trusted_Connection=True;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuração da Injeção de Dependência
builder.Services.AddScoped<ICorreiaRepository, CorreiaRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddControllers();

// Configurar o Swagger para testes de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar o middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
