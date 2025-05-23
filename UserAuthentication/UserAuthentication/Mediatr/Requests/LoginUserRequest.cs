using FluentResults;
using MediatR;
using UserAuthentication.Dto;

namespace UserAuthentication.Mediatr.Requests
{
    public class LoginUserRequest : IRequest<Result<TokenResponseDto?>>
    {
        public required UserDto UserDtoRequest {  get; set; }
    }
}
