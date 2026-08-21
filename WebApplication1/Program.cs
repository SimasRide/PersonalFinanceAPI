using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Regista os controllers da API.
builder.Services.AddControllers();

// Ativa a documentação OpenAPI.
builder.Services.AddOpenApi();

// Obtém a ligação ao PostgreSQL.
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "A connection string 'DefaultConnection' não foi configurada.");

// Regista o AppDBContext e configura o PostgreSQL.
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(connectionString));

// Constrói a aplicação após registar todos os serviços.
var app = builder.Build();

// Disponibiliza o OpenAPI apenas em desenvolvimento.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redireciona HTTP para HTTPS.
app.UseHttpsRedirection();

// Ativa as rotas dos controllers.
app.MapControllers();

// Inicia a aplicação.
app.Run();