using AuthAPI.Models.Dto;

namespace AuthAPI.Services
{
    public interface IUsersAPIService
    {
        Task<ResponseDto> GetUsers();
    }
}
