using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Middleware;

// Middleware simples para capturar exceções não tratadas
// e devolver uma resposta genérica ao cliente.
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Loga o erro internamente (não expõe detalhes ao cliente)
            _logger.LogError(ex, "Unhandled exception caught by middleware");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var payload = new { error = "An unexpected error occurred." };
            var json = JsonSerializer.Serialize(payload);

            await context.Response.WriteAsync(json);
        }
    }
}
