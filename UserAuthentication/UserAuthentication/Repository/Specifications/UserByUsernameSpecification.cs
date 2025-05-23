using Ardalis.Specification;
using UserAuthentication.Entities;

namespace UserAuthentication.Repository.Specifications
{
    public class UserByUsernameSpecification : Specification<User>
    {
        public UserByUsernameSpecification(String username) 
        {
            Query
                .Where(u => u.Username.ToLower().Equals(username.ToLower()));
        }
    }
}
