using MediatR;
using UserAuthentication.Dto;
using UserAuthentication.Entities;
using UserAuthentication.Mediatr.Requests;
using UserAuthentication.Services;

namespace UserAuthentication.Mediatr.Handlers
{
    public class DeleteByUsernameHandler : IRequestHandler<DeleteByUsernameRequest, User>
    {
        private readonly IUserService _userService;

        public DeleteByUsernameHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<User> Handle(DeleteByUsernameRequest request, CancellationToken cancellationToken)
        {
            return _userService.deleteByUsername(request.Username);
        }
    }
}
