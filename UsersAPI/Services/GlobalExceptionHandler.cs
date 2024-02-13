using Microsoft.AspNetCore.Diagnostics;
using UsersAPI.Lib;

namespace UsersAPI.Services
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;


        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
            ResponseDto responseDto = new()
            {
                Result = null,
                Status = StatusCodes.Status500InternalServerError,
                ErrorMessage = exception.Message,
            };
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(responseDto, cancellationToken);
            return true;
        }
    }
}
