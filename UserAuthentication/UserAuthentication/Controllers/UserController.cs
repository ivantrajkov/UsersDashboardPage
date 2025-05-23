using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserAuthentication.Dto;
using UserAuthentication.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using UserAuthentication.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using UserAuthentication.Mediatr.Requests;

namespace UserAuthentication.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpDelete("delete/{Username}")]
        public async Task<ActionResult<DisplayUserDto>> DeleteByUsername(string Username)
        {
            User? user = await _mediator.Send(new DeleteByUsernameRequest { Username = Username}) as User;
            if (user == null)
                return NotFound();

            var userDto = new DisplayUserDto
            {
                Username = user.Username,
                Role = user.Role,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                PasswordHash = user.PasswordHash
            };
            return Ok(userDto);
        }

        [HttpPost("register")]
        public async Task<ActionResult<DisplayUserDto>> Register(UserDto request)
        {
            //var user =  await _userService.RegisterAsync(request);
            var user = await _mediator.Send(new RegisterUserRequest { UserDto = request });
            if(user.IsFailed)
            {
                return BadRequest(user.Errors.First().Message);
            }
            if (user.Value != null)
            {
                var displayUserDto = new DisplayUserDto
                {
                    Username =  user.Value.Username,
                    Role = user.Value.Role,
                    RefreshToken = user.Value.RefreshToken,
                    RefreshTokenExpiryTime = user.Value.RefreshTokenExpiryTime,
                    PasswordHash = user.Value.PasswordHash
                };
                return Ok(displayUserDto);
            }
            return NotFound();
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            //var result = await _userService.LoginAsync(request);
            var result = await _mediator.Send(new LoginUserRequest { UserDtoRequest = request });
            if(result.IsFailed)
            {
                return BadRequest("Invalid username or password");
            }

            return Ok(result.Value);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnly()
        {
            return Ok("You are authenticated");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnly()
        {
            return Ok("You are an Admin");
        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto refreshTokenRequest)
        {
            var result = await _mediator.Send(new RefreshTokenRequest { refreshTokenRequestDto =  refreshTokenRequest });
            if (result.IsFailed) 
            {
                Unauthorized(result.Errors.First().Message);
            }

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<DisplayUserDto>>> getAll()
        {
            var users = await _mediator.Send(new GetAllUsersRequest()); await _mediator.Send(new GetAllUsersRequest());

            var userDtos = users.Select(x => new DisplayUserDto 
            {
                Username = x.Username,
                PasswordHash = x.PasswordHash,
                Role = x.Role,
                RefreshToken = x.RefreshToken,
                RefreshTokenExpiryTime = x.RefreshTokenExpiryTime
            }).ToList();
            return Ok(userDtos);
        }

    }
}
