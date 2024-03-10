namespace AuthAPI.Models.ResponseDto
{
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

        public ResponseDto Build()
        {
            return _responseDto;
        }
    }
}
