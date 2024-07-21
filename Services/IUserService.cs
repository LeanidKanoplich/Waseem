using System.Threading.Tasks;
using SolarpayAPI.Dtos;

namespace SolarpayAPI.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<UserRegisterDto>> RegisterAsync(UserRegisterDto request);
        Task<ServiceResponse<UserLoginDto>> LoginAsync(UserLoginDto request);
    }
}
