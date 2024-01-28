namespace UsersAPI.Lib
{
    public class ResponseDto
    {
        public object? Result { get; set; } = null;
        public string? ErrorMessage { get; set; } = null;
        public int? Status { get; set; } = StatusCodes.Status200OK;
    }
}
