using MediatR;
using UserAuthentication.Entities;

namespace UserAuthentication.Mediatr.Requests
{
    public class GetAllUsersRequest : IRequest<IEnumerable<User>>
    {

    }
}
