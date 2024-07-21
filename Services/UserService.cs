using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SolarpayAPI.Data;
using SolarpayAPI.Dtos;
using SolarpayAPI.Models;

namespace SolarpayAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<UserRegisterDto>> RegisterAsync(UserRegisterDto request)
        {
            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<UserRegisterDto>
            {
                Data = request,
                Success = true,
                Message = "User registered successfully"
            };
        }

        public async Task<ServiceResponse<UserLoginDto>> LoginAsync(UserLoginDto request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null)
            {
                return new ServiceResponse<UserLoginDto>
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            var userDto = new UserLoginDto
            {
                Username = user.Username,
                Password = user.Password
            };

            return new ServiceResponse<UserLoginDto>
            {
                Data = userDto,
                Success = true,
                Message = "User logged in successfully"
            };
        }
    }
}
