using Ardalis.Specification;
using UserAuthentication.Data;
using UserAuthentication.Entities;

namespace UserAuthentication.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T?> GetById(Guid? id);
        Task<T> Delete(T entity);
        Task<IEnumerable<T>> GetAll();      
    }
}
