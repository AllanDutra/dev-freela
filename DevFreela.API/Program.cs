using DevFreela.API.Filters;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Validators;
using DevFreela.Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DevFreela.API.Extensions;
using DevFreela.Application.Consumers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#pragma warning disable CS0618
builder.Services
    .AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" }); // * SINGLETON: INSTÂNCIA PARA TODA A APLICAÇÃO

#region INJEÇÃO DE DEPÊNDENCIA

builder.Services.AddInfrastructure();

#endregion

#region CONFIGURAÇÃO DE BANCO DE DADOS

var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs");

builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));

// builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase("DevFreela")); // * PARA USAR O BANCO DE DADOS NA MEMÓRIA ENQUANTO NÃO EXISTIR O FÍSICO (instalar pacote Microsoft.EntityFrameworkCore.InMemory)

#endregion

#region ADICIONANDO SERVIÇOS DE BACKGROUND

builder.Services.AddHostedService<PaymentApprovedConsumer>();

#endregion

#region ADICIONANDO MICROSERVICES

builder.Services.AddHttpClient();

#endregion

#region ADICIONANDO CQRS

builder.Services.AddMediatR(typeof(CreateProjectCommand));

#endregion

#region ADICIONANDO LOGIN

builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DevFreela.API",
                    Version = "v1",
                    Description = "Repository developed during the ASP .NET Core Training course maintained by the company Luis Dev. In this project, concepts of development of Web APIs using .NET 6, Clean Architecture, CQRS, Entity Framework Core, Dapper, Repository Pattern, Unit Tests, Authentication and Authorization with JWT, Messaging and Microservices were applied."
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header usando o esquema Bearer."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
                 });
            });

#region PARA ADICIONAR AUTENTICAÇÃO

builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,

          ValidIssuer = builder.Configuration["Jwt:Issuer"],
          ValidAudience = builder.Configuration["Jwt:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
      };
  });

#endregion

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
