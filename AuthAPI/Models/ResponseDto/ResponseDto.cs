namespace AuthAPI.Models.ResponseDto
{
    public class ResponseDto
    {
        public object? Data { get; set; } = null;
        public bool Success { get; set; } = true;
        public string? Error { get; set; } = null;
        public string? Token { get; set; } = null;
    }
}
