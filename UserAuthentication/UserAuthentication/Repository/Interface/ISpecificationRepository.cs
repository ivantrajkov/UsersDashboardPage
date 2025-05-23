using Ardalis.Specification;
using UserAuthentication.Entities;

namespace UserAuthentication.Repository.Interface
{
    public interface ISpecificationRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetList(ISpecification<T> specification);
        Task<T?> GetOne(ISpecification<T> specification);
    }
}
