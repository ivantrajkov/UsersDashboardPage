using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Data;
using UserAuthentication.Entities;
using UserAuthentication.Repository.Interface;

namespace UserAuthentication.Repository.Implementation
{
    public class SpecificationRepository<T> : ISpecificationRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;

        public SpecificationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<T>> GetList(ISpecification<T> specification)
        {
            var queryResult = SpecificationEvaluator.Default
                .GetQuery(
                query: _dbContext.Set<T>().AsQueryable(),
                specification: specification
                );
            return await queryResult.ToListAsync();
        }

        public async Task<T?> GetOne(ISpecification<T> specification)
        {
            var queryResult = SpecificationEvaluator.Default
                .GetQuery<T>(
                query: _dbContext.Set<T>().AsQueryable(),
                specification: specification
                );

            return await queryResult.FirstOrDefaultAsync();
        }
    }
}
