using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserAuthentication.Data;
using UserAuthentication.Dto;
using UserAuthentication.Entities;
using UserAuthentication.Repository.Interface;
using UserAuthentication.Repository.Specifications;

namespace UserAuthentication.Services.impl
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration configuration;
        private readonly IUserRepository _repository;
        private readonly ISpecificationRepository<User> _specificationRepository;

        public UserService(AppDbContext context, IConfiguration configuration, IUserRepository repository, ISpecificationRepository<User> specificationRepository)
        {
            _context = context;
            this.configuration = configuration;
            _repository = repository;
            _specificationRepository = specificationRepository;
        }


        public async Task<Result<TokenResponseDto?>> LoginAsync(UserDto request)
        {


            var user = _repository.GetUserByUsername(request.Username);
            if (user is null)
            {
                return Result.Fail("The username is incorrect!");
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return Result.Fail("The password is incorrect!");
            }
            return await CreateTokenResponse(user);
        }

        private async Task<TokenResponseDto> CreateTokenResponse(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
            };
        }

        public async Task<Result<User?>> RegisterAsync(UserDto request)
        {

            var check = _repository.GetUserByUsername(request.Username);
            if (check != null)
            {
                return Result.Fail("A user with that username already exists");
            }
            var user = new User
            {
                Id = Guid.NewGuid(),
            };

            var hashedPassword = new PasswordHasher<User>()
           .HashPassword(user, request.Password);

            user.Username = request.Username;
            user.PasswordHash = hashedPassword;

            await _repository.Create(user);

            return user;

        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            return Convert.ToBase64String(random);
        }
        private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
         
            var user = await _repository.GetById(userId);
            if(user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }
        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            //await _context.SaveChangesAsync();
            await _repository.Update(user);
            return refreshToken;
        }
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async Task<Result<TokenResponseDto?>> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
                return Result.Fail("Refresih token error");


            return await CreateTokenResponse(user);



        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<User> deleteByUsername(String username)
        {
            var user =  _repository.GetUserByUsername(username);
            return   await _repository.Delete(user);
        }
    }
}
