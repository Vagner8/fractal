namespace UsersAPI.Lib.Response
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool? Success { get; set; }
        public string? ErrorMessage { get; set; }

        public ApiResponse(T data, bool? success = true, string? errorMessage = "" )
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
        }
    }
}
