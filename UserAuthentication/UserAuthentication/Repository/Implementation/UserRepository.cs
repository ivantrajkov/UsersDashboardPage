using Ardalis.Specification;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Data;
using UserAuthentication.Entities;
using UserAuthentication.Repository.Interface;

namespace UserAuthentication.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private const string SP_GET_ALL_USERS = "GetAll";
        private const string SP_GET_BY_USERNAME = "findByUsernameProcedure @username";

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Create(User entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<User> Delete(User entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Set<User>().ToListAsync();
            //return await _dbContext.Users.FromSqlRaw("Exec GetAll").ToListAsync();
            //return await _dbContext.Users.FromSqlRaw("EXEC " + SP_GET_ALL_USERS).ToListAsync();


        }

        public async Task<User?> GetById(Guid? id)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public Task<IEnumerable<User>> GetList(ISpecification<User> specification)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetOne(ISpecification<User> specification)
        {
            throw new NotImplementedException();
        }

        public  User? GetUserByUsername(string username)
        {
            //var sqlUsername = new SqlParameter("@username", username);
            //return _dbContext.Users.FromSqlRaw("EXEC " + SP_GET_BY_USERNAME, sqlUsername).AsEnumerable().FirstOrDefault();
            return _dbContext.Users.Where(u => u.Username == username).FirstOrDefault();
        }


        public async Task<User> Update(User entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
