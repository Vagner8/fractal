namespace AuthAPI.Models.Dto
{
    public class ResponseDto
    {
        public object? Data { get; set; } = null;
        public bool Success { get; set; } = true;
        public string? Error { get; set; } = null;
        public string? Token { get; set; } = null;
    }

    public class ResponseDtoBuilder
    {
        private readonly ResponseDto _responseDto;

        public ResponseDtoBuilder()
        {
            _responseDto = new ResponseDto();
        }

        public ResponseDtoBuilder SetData(object value)
        {
            _responseDto.Data = value;
            return this;
        }

        public ResponseDtoBuilder SetError(string? value)
        {
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
