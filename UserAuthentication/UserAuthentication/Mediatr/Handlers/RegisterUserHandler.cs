using FluentResults;
using MediatR;
using UserAuthentication.Entities;
using UserAuthentication.Mediatr.Requests;
using UserAuthentication.Services;

namespace UserAuthentication.Mediatr.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, Result<User?>>
    {
        private readonly IUserService _userService;

        public RegisterUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result<User?>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            return _userService.RegisterAsync(request.UserDto);
        }
    }
}
