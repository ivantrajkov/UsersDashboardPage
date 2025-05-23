using FluentResults;
using MediatR;
using UserAuthentication.Dto;

namespace UserAuthentication.Mediatr.Requests
{
    public class RefreshTokenRequest : IRequest<Result<TokenResponseDto?>>
    {
        public required RefreshTokenRequestDto refreshTokenRequestDto {  get; set; }
    }
}
