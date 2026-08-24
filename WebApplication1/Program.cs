using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

// Nome da política de CORS usada para permitir chamadas do frontend
const string FrontendCorsPolicy = "FrontendCorsPolicy";

// Cria o builder da aplicação (configuração, serviços, etc.)
var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddControllers();

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

// Linha comentada encontrada no ficheiro original; parecia ser uma URL
// solta (provocaria erro de compilação). Mantida como comentário para
// rastreio, caso seja relevante para o autor.
// https://steamcommunity.com/tradeoffer/new/?partner=1017301327&token=ULC2IdAq

// Faz o mapeamento dos controllers para as rotas HTTP
app.MapControllers();

// Inicia a aplicação e começa a escutar pedidos
app.Run();
