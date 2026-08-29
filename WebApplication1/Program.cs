using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using System.Text.Json.Serialization;

// Nome da política de CORS usada para permitir chamadas do frontend
const string FrontendCorsPolicy = "FrontendCorsPolicy";

// Cria o builder da aplicação (configuração, serviços, etc.)
var builder = WebApplication.CreateBuilder(args);

// Força a aplicação a escutar sempre em http://localhost:5287

// Regista a política de CORS para permitir chamadas vindas do frontend
// Neste caso, apenas permitir origens de http://localhost:5173 (ex.: Vite/React dev)
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()   // Permitir qualquer header nas requisições
            .AllowAnyMethod();  // Permitir qualquer método HTTP (GET, POST, ...)
    });
});

// Regista os controllers do ASP.NET Core (endpoints via controllers)
// e configura a serialização para enviar enums como strings no JSON
builder.Services.AddControllers()
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Obtém a connection string chamada "DefaultConnection" do appsettings
// Lança exceção se não estiver configurada para evitar falhas silenciosas
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "A connection string 'DefaultConnection' não foi configurada.");

// Regista o DbContext da aplicação usando PostgreSQL (Npgsql)
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(connectionString)); 

// Constrói a aplicação com os serviços configurados
var app = builder.Build();

// Aplica a política de CORS definida anteriormente
app.UseCors(FrontendCorsPolicy);

// Regista o middleware global de tratamento de erros
app.UseMiddleware<WebApplication1.Middleware.ErrorHandlingMiddleware>();

app.MapGet("/Dashboard", () => Results.Ok(new
{
    message = "Financial Overview API is running"
}));

app.MapControllers();

app.Run();
