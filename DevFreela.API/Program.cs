using DevFreela.API.Models;
using DevFreela.Application.Services.Implementations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<OpeningTimeOption>(builder.Configuration.GetSection("OpeningTime"));

// builder.Services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" }); // * SINGLETON: INSTÂNCIA PARA TODA A APLICAÇÃO

#region INJEÇÃO DE DEPÊNDENCIA

builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" }); // * SCOPED: OBJETO DIFERENTE PARA CADA REQUISIÇÃO (TEMPO DE VIDA PARA REQUISIÇÕES)

#endregion

# region CONFIGURAÇÃO DE BANCO DE DADOS

var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs");

builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
