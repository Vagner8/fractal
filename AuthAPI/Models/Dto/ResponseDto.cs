namespace AuthAPI.Models.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool? Success { get; set; } = true;
        public string Error { get; set; } = string.Empty;
    }
}
