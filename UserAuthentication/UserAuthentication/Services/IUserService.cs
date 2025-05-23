using FluentResults;
using UserAuthentication.Dto;
using UserAuthentication.Entities;

namespace UserAuthentication.Services
{
    public interface IUserService
    {
        Task<Result<User?>> RegisterAsync(UserDto request);
        Task<Result<TokenResponseDto?>> LoginAsync(UserDto request);
        Task<Result<TokenResponseDto?>> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task<IEnumerable<User>> GetAll();
        Task<User> deleteByUsername(String username);
    }
}
