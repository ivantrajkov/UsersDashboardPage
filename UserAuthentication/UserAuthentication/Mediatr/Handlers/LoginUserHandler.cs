using FluentResults;
using MediatR;
using UserAuthentication.Dto;
using UserAuthentication.Mediatr.Requests;
using UserAuthentication.Services;

namespace UserAuthentication.Mediatr.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, Result<TokenResponseDto?>>
    {
        private readonly IUserService _userService;

        public LoginUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result<TokenResponseDto?>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            return _userService.LoginAsync(request.UserDtoRequest);
        }
    }
}
