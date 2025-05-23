using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Data;
using UserAuthentication.Entities;
using UserAuthentication.Repository.Interface;
using Ardalis.Specification.EntityFrameworkCore;

namespace UserAuthentication.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Create(T entity)
        {

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid? id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        //public async Task<IEnumerable<T>> GetList(ISpecification<T> specification)
        //{
        //    var queryResult = SpecificationEvaluator.Default
        //        .GetQuery(
        //        query: _dbContext.Set<T>().AsQueryable(),
        //        specification: specification
        //        );
        //    return await queryResult.ToListAsync();
        //}

        //public async Task<T?> GetOne(ISpecification<T> specification)
        //{
        //    var queryResult = SpecificationEvaluator.Default
        //        .GetQuery<T>(
        //        query: _dbContext.Set<T>().AsQueryable(),
        //        specification: specification
        //        );

        //    return await queryResult.FirstOrDefaultAsync();
        //}

        public async Task<T> Update(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
