using System.Data;
using System.Threading.Tasks;
using Moq;
using UserAuthentication.Dto;
using UserAuthentication.Entities;
using UserAuthentication.Repository.Implementation;
using UserAuthentication.Repository.Interface;
using UserAuthentication.Services;
using UserAuthentication.Services.impl;

namespace UnitTests
{
    [TestClass]
    public sealed class UserServiceTest
    {
        private Mock<IUserRepository> _userRepository;
        private UserService _service;

        [TestInitialize]
        public void Setup()
        {
           _userRepository = new Mock<IUserRepository>();
           _service = new UserService(null,null, _userRepository.Object ,null);

        }
        [TestMethod]
        public async Task GetAll_Works()
        {
            var mockUsers = new List<User>
            {
                new User { Id = Guid.NewGuid(), Username = "user1", PasswordHash = "password1" },
                new User { Id = Guid.NewGuid(), Username = "user2", PasswordHash = "password2" },
                new User { Id = Guid.NewGuid(), Username = "ivan", PasswordHash = "password2" }
            };

            _userRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(mockUsers);

            var result = await _service.GetAll();

            Assert.AreEqual("user1", result.First().Username);
            Assert.AreEqual("ivan",result.Last().Username);
            Assert.AreEqual(3, result.Count());


        }

        public static IEnumerable<object[]> getDtoTestData()
        {
            yield return new object[] { new UserDto { Username = "Ivan", Password = "Password1" } };
            yield return new object[] { new UserDto { Username = "user", Password = "user" } };
            yield return new object[] { new UserDto { Username = "user2", Password = "user3" } };
            yield return new object[] { new UserDto { Username = "user3", Password = "Password1" } };
        }

        [TestMethod]
        [DynamicData(nameof(getDtoTestData), DynamicDataSourceType.Method)]
        public async Task Register_AlreadyExists(UserDto userDto)
        {

            var mockUsers = new List<User>
            {
                new User { Id = Guid.NewGuid(), Username = "user1", PasswordHash = "password1" },
                new User { Id = Guid.NewGuid(), Username = "user2", PasswordHash = "password2" },
                new User { Id = Guid.NewGuid(), Username = "Ivan", PasswordHash = "password2" },
                new User { Id = Guid.NewGuid(), Username = "user", PasswordHash = "password2" },
                new User { Id = Guid.NewGuid(), Username = "user3", PasswordHash = "password2" }
            };
            var filtered = mockUsers.Where(x => x.Username == userDto.Username).FirstOrDefault();

            _userRepository.Setup(x => x.GetUserByUsername(userDto.Username)).Returns(filtered);

            var result = await _service.RegisterAsync(userDto);
            if(filtered != null)
            {
                Assert.AreEqual("A user with that username already exists", result.Errors.First().Message);
            } else
            {
                Assert.Fail("The user exists.");
            }
        }



        [TestMethod]
        [DynamicData(nameof(getDtoTestData), DynamicDataSourceType.Method)]
        public async Task Register_Passes(UserDto userDto)
        {


            var result =  await _service.RegisterAsync(userDto);

            Assert.AreEqual(result.IsSuccess, true);
        }




    }
}
