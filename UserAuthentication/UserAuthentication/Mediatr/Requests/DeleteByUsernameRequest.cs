using MediatR;
using UserAuthentication.Dto;
using UserAuthentication.Entities;

namespace UserAuthentication.Mediatr.Requests
{
    public class DeleteByUsernameRequest : IRequest<User>
    {
        public required string Username { get; set; }   
    }
}
