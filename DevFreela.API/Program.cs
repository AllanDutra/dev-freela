using DevFreela.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<OpeningTimeOption>(builder.Configuration.GetSection("OpeningTime"));
// builder.Services.AddSingleton<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" }); // * SINGLETON: INSTÂNCIA PARA TODA A APLICAÇÃO
builder.Services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" }); // * SCOPED: OBJETO DIFERENTE PARA CADA REQUISIÇÃO (TEMPO DE VIDA PARA REQUISIÇÕES)

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
