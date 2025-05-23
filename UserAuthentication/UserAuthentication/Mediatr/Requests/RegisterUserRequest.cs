using FluentResults;
using MediatR;
using UserAuthentication.Dto;
using UserAuthentication.Entities;

namespace UserAuthentication.Mediatr.Requests
{
    public class RegisterUserRequest : IRequest<Result<User?>>
    {
        public required UserDto UserDto { get; set; }
    }
}
