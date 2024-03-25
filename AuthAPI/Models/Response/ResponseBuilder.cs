namespace AuthAPI.Models.Response
{
    public class ResponseBuilder
    {
        public static ResponseDto SetData(object? data)
        {
            return new ResponseDto
            {
                Data = data,
                Success = true,
                Error = null,
            };
        }

        public static ResponseDto SetError(string? error = null)
        {
            return new ResponseDto
            {
                Data = null,
                Success = false,
                Error = error ?? "Unknown error",
            };
        }
    } 
}
