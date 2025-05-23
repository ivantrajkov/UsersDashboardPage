using UserAuthentication.Entities;

namespace UserAuthentication.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetUserByUsername(String username);

    }
}
