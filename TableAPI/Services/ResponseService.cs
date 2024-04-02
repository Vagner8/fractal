using MatrixAPI.Models;

namespace MatrixAPI.Services
{
    public class ResponseService : IResponseService
    {
        public ResponseDto Data(object? data)
        {
            return new ResponseDto
            {
                Data = data,
                Success = true,
                Error = null,
            };
        }

        public ResponseDto Error(string? error = null)
        {
            return new ResponseDto
            {
                Data = null,
                Success = false,
                Error = error ?? "Unknown error",
            };
        }
    }

    public interface IResponseService
    {
        public ResponseDto Data(object? data);
        public ResponseDto Error(string? error = null);
    }
}
