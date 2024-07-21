using System.Collections.Generic;
using System.Linq;
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

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

        public async Task<ServiceResponse<List<UserDto>>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email
            }).ToList();

            return new ServiceResponse<List<UserDto>>
            {
                Data = userDtos,
                Success = true,
                Message = "Users retrieved successfully"
            };
        }

        public async Task<ServiceResponse<UserDto>> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new ServiceResponse<UserDto>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return new ServiceResponse<UserDto>
            {
                Data = userDto,
                Success = true,
                Message = "User retrieved successfully"
            };
        }

        public async Task<ServiceResponse<UserDto>> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return new ServiceResponse<UserDto>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return new ServiceResponse<UserDto>
            {
                Data = userDto,
                Success = true,
                Message = "User retrieved successfully"
            };
        }

        public async Task<ServiceResponse<UserUpdateDto>> UpdateUserAsync(int id, UserUpdateDto request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new ServiceResponse<UserUpdateDto>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            user.Username = request.Username;
            user.Email = request.Email;

            await _context.SaveChangesAsync();

            return new ServiceResponse<UserUpdateDto>
            {
                Data = request,
                Success = true,
                Message = "User updated successfully"
            };
        }

        public async Task<ServiceResponse<bool>> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Success = true,
                Message = "User deleted successfully"
            };
        }
    }
}
