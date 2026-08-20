using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("A connection string da default connection não foi configurada = exceção");
builder.Services.AddDbContext<AppDBContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

builder.Services.AddRazorPages();



{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseStaticFiles();

app.MapRazorPages();
app.Run();