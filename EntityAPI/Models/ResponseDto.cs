namespace EntityAPI.Models
{
    public class ResponseDto
    {
        public object? Data { get; set; } = null;
        public bool Success { get; set; } = true;
        public string? Error { get; set; } = null;
    }

    public class ResponseBuilder
    {
        public static ResponseDto Data(object? data)
        {
            return new ResponseDto
            {
                Data = data,
                Success = true,
                Error = null,
            };
        }

        public static ResponseDto Error(string? error = null)
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
