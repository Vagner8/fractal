using Microsoft.AspNetCore.Diagnostics;

namespace UsersAPI.Lib.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private ResponseDto _responseDto;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
            _responseDto = new ResponseDto();
        }

        public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            _responseDto.ErrorMessage = exception.Message;

            await httpContext.Response.WriteAsJsonAsync(_responseDto, cancellationToken);

            return true;
        }
    }
}
