using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Entities;
using UserAuthentication.Entities.enums;

namespace UserAuthentication.Data
{
    public class AppDbContext : DbContext
    {
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private static readonly Guid AdminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private static readonly Guid User1Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        private static readonly Guid User2Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
        private static readonly Guid User3Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
        private static readonly Guid User4Id = Guid.Parse("55555555-5555-5555-5555-555555555555");

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
         new User
         {
             Id = AdminId,
             Username = "admin",
             PasswordHash = "AQAAAAIAAYagAAAAEH9MUVqZAd1HJosVdpILz2HzVbafsV1UNW7uBb6ZlquyAnoT7hV9y/FtkldxdkhgzQ==",
             Role = Role.Admin
         },
         new User
         {
             Id = User1Id,
             Username = "user1",
             PasswordHash = "AQAAAAIAAYagAAAAEEGKNzLpcJjG40N9LOWG+6BZg8mHNvR6XLtut5Vm8AQRx4y7NnVw05rT193He3UwGw==",
             Role = Role.User
         },
         new User
         {
             Id = User2Id,
             Username = "user2",
             PasswordHash = "AQAAAAIAAYagAAAAEFHBUMJsZL1Jj+wR8Szfr/fDa51V4XTp2xANQslpxsicfFHWvlxXB+E0oxaxDUmE3g==",
             Role = Role.User
         },
         new User
         {
             Id = User3Id,
             Username = "user3",
             PasswordHash = "AQAAAAIAAYagAAAAENfqHUq9MwyEP9BAY9x+rvumK8ZR4b07d5SXCkutS69KPo2F8NGjzrPrAOUYxd66Qw==",
             Role = Role.User
         },
         new User
         {
             Id = User4Id,
             Username = "user4",
             PasswordHash = "AQAAAAIAAYagAAAAEPGaYIJS4Uhqg0Sb31S+g932GOvjKPZrdQvtQmGeNzfsGdWgAnI/7DQ9iKtqW2Xwdw==",
             Role = Role.User
         }
     );

        }

        protected AppDbContext()
        {
        }

        public DbSet<User> Users { get; set; }
   
      
       }
}
