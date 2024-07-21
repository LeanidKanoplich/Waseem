using SolarpayAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarpayAPI.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<UserRegisterDto>> RegisterAsync(UserRegisterDto request);
        Task<ServiceResponse<UserLoginDto>> LoginAsync(UserLoginDto request);
        Task<ServiceResponse<List<UserDto>>> GetAllUsersAsync();
        Task<ServiceResponse<UserDto>> GetUserByIdAsync(int id);
        Task<ServiceResponse<UserDto>> GetUserByUsernameAsync(string username);
        Task<ServiceResponse<UserUpdateDto>> UpdateUserAsync(int id, UserUpdateDto request);
        Task<ServiceResponse<bool>> DeleteUserAsync(int id);
    }
}
