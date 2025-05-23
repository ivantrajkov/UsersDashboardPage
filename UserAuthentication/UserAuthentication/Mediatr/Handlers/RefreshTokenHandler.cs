using FluentResults;
using MediatR;
using UserAuthentication.Dto;
using UserAuthentication.Mediatr.Requests;
using UserAuthentication.Services;

namespace UserAuthentication.Mediatr.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, Result<TokenResponseDto?>>
    {
        IUserService _userService;

        public RefreshTokenHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<Result<TokenResponseDto?>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            return _userService.RefreshTokenAsync(request.refreshTokenRequestDto);
        }
    }
}
