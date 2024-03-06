namespace UsersAPI.Lib
{
    public class ResponseDto
    {
        public object? Data { get; set; } = null;
        public string? ErrorMessage { get; set; } = null;
        public int? Status { get; set; } = 200;
    }
}
