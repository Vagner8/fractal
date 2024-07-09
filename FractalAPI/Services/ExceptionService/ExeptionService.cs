using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace FractalAPI.Services
{
  public class ExceptionService(ILogger<ExceptionService> logger) : IExceptionHandler
  {
    private readonly ILogger<ExceptionService> _logger = logger;
    public async ValueTask<bool> TryHandleAsync(HttpContext ctx, Exception ex, CancellationToken token)
    {
      _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
      var details = new ProblemDetails
      {
        Detail = ex.Message,
        Instance = "API",
        Status = (int)HttpStatusCode.InternalServerError,
        Title = "API Error",
        Type = "Server Error"
      };
      var response = JsonSerializer.Serialize(details);
      ctx.Response.ContentType = "application/json";
      await ctx.Response.WriteAsync(response, token);
      return true;
    }
  }
}
