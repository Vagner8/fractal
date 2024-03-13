namespace UsersAPI.Models.Response
{
    public class ResponseDto
    {
        public object? Data { get; set; } = null;
        public bool Success { get; set; } = true;
        public string? Error { get; set; } = null;
    }
}
