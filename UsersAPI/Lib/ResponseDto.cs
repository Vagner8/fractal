namespace UsersAPI.Lib
{
    public class ResponseDto
    {
        private object? _result = null;
        private string? _errorMessage = null;

        public object? Result
        {
            get => _result;
            set
            {
                _result = value;
                _errorMessage = null;
            }
        }

        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                _result = null;
            }
        }
    }
}
