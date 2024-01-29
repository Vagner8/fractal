using UsersAPI.Lib;

namespace UsersAPI.Middleware
{
    public class NotMatchMiddleware
    {
        private readonly RequestDelegate _next;

        public NotMatchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            if (context.Response.StatusCode == 404)
            {
                var responseDto = new ResponseDto
                {
                    Result = null,
                    Status = StatusCodes.Status404NotFound,
                    ErrorMessage = $"Wrong url: {context.Request.Path}{context.Request.QueryString}"
                };
                await context.Response.WriteAsJsonAsync(responseDto);
            }
        }
    }
}
