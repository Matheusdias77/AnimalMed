using AnimalMed.Application.Data.Repositories;
using AnimalMed.Application.Data.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Adiciona CORS (libera qualquer origem)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Adiciona Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona repositório no DI
builder.Services.AddSingleton<IEstoqueRepository>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    var logger = sp.GetRequiredService<ILogger<EstoqueRepository>>();
    return new EstoqueRepository(connectionString);
});

var app = builder.Build();

// Habilita CORS
app.UseCors();

app.UseHttpsRedirection();

// Configura Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnimalMed API V1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

// Registra os controllers
app.MapControllers();

app.Run();
