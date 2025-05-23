using MediatR;
using UserAuthentication.Entities;
using UserAuthentication.Mediatr.Requests;
using UserAuthentication.Services;

namespace UserAuthentication.Mediatr.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, IEnumerable<User>>
    {
        private readonly IUserService _userService;

        public GetAllUsersHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<IEnumerable<User>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            return _userService.GetAll();
        }
    }
}
