namespace AuthAPI.Models.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; } = null;
        public bool Success { get; set; } = true;
        public string? Error { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    public class ResponseDtoBuilder
    {
        private readonly ResponseDto _responseDto;

        public ResponseDtoBuilder()
        {
            _responseDto = new ResponseDto();
        }

        public ResponseDtoBuilder SetResult(object value)
        {
            _responseDto.Result = value;
            _responseDto.Success = true;
            _responseDto.Error = null;
            return this;
        }

        public ResponseDtoBuilder SetError(string? value)
        {
            _responseDto.Result = null;
            _responseDto.Success = false;
            _responseDto.Error = value;
            return this;
        }

        public ResponseDtoBuilder SetToken(string token)
        {
            _responseDto.Token = token;
            return this;
        }

        public ResponseDto Get()
        {
            return _responseDto;
        }
    }
}
