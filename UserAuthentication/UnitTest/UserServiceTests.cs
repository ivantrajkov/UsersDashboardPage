using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using UserAuthentication.Dto;
using UserAuthentication.Entities;
using UserAuthentication.Repository.Interface;
using UserAuthentication.Services;
using UserAuthentication.Services.impl;
using static CSharpFunctionalExtensions.Result;

namespace UnitTest
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public UserServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string> {
            {"AppSettings:Token", "MySuperSecureAndRandoMKeyThatLooksJusAwesomeAndNeedsToBeVeryVeryVeryLong!!!Okay?!"},
            {"AppSettings:Issuer", "AuthenticatioAppDemo"},
            {"AppSettings:Audience", "AuthenticationAppDemoAudience"},
            };



            _userRepository = new Mock<IUserRepository>();
            _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

            _userService = new UserService(null,_configuration,_userRepository.Object,null);
            _passwordHasher = new PasswordHasher<User>();

        }

        [Fact]
        public async Task GetAll_WorksAsync()
        {
            //Arrange
            var database = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username1"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username2"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username3"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username4"
                }
            };
            _userRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(database);

            //Act
            var expected = database;
            var result = await _userService.GetAll();

            //Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> UserDtoData()
        {
            for (int i = 1; i < 6; i++) 
            {
                yield return new object[]
                {
                    new UserDto{ Username = "Username" +  i, Password = "pw"}
                };
            }
        }

        [Theory]
        [MemberData(nameof(UserDtoData))]
        public async Task Register_Fails(UserDto userDto)
        {
            //Arrange
            var database = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username1"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username2"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username3"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username4"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username5"
                }
            };

            var filtered = database.Where(x => x.Username.Equals(userDto.Username)).FirstOrDefault();

            _userRepository
                .Setup(x => x.GetUserByUsername(userDto.Username))
                .Returns(filtered);

            //ACT

            var result = await _userService.RegisterAsync(userDto);


            //ASSERT
            Assert.False(result.IsSuccess);
        }

        [Theory]
        [MemberData(nameof(UserDtoData))]
        public async Task Register_Passes(UserDto userDto)
        {
            //Arrange
            var database = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username6"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username7"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username8"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username8"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "Username9"
                }
            };

            var filtered = database.Where(x => x.Username.Equals(userDto.Username)).FirstOrDefault();
            User? user = null;

            _userRepository
                .Setup(x => x.GetUserByUsername(userDto.Username))
                .Returns(user);

            //ACT

            var result = await _userService.RegisterAsync(userDto);


            //ASSERT

            Assert.True(result.IsSuccess);
        }

        private User CreateUserWithHashedPassword(Guid id, string username, string password)
        {
            var user = new User { Id = id, Username = username };
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            return user;
        }


        [Theory]
        [MemberData(nameof(UserDtoData))]
        public async Task Login_Passes(UserDto userDto) 
        {
            //Arrange
            var database = new List<User>
            {
                CreateUserWithHashedPassword(Guid.NewGuid(),"Username1","pw"),
                CreateUserWithHashedPassword(Guid.NewGuid(),"Username2","pw"),
                CreateUserWithHashedPassword(Guid.NewGuid(),"Username3","pw"),
                CreateUserWithHashedPassword(Guid.NewGuid(),"Username4","pw"),
                CreateUserWithHashedPassword(Guid.NewGuid(),"Username5","pw")
            };
            //var key = new SymmetricSecurityKey(
            //    Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));
            //_configuration.Setup(x => x.G<String>("AppSettings:Token")).Returns("MySuperSecureAndRandoMKeyThatLooksJusAwesomeAndNeedsToBeVeryVeryVeryLong!!!Okay?!");
            //_configuration.SetupGet(x => x[It.IsAny<string>()]).Returns("MySuperSecureAndRandoMKeyThatLooksJusAwesomeAndNeedsToBeVeryVeryVeryLong");\



            


            var filtered = database.Where(x => x.Username.Equals(userDto.Username)).FirstOrDefault();

            _userRepository.Setup(x => x.GetUserByUsername(userDto.Username)).Returns(filtered);

            //ACT

            var result =  await _userService.LoginAsync(userDto);


            //ASSERT
            Assert.True(result.IsSuccess);
            Assert.IsType<TokenResponseDto>(result.Value);

        }

        [Theory]
        [MemberData(nameof(UserDtoData))]
        public async Task Login_FailsAsync(UserDto userDto)
        {
            //Arrange
            var database = new List<User>
            {
                CreateUserWithHashedPassword(Guid.NewGuid(),"Username1","it"),
                CreateUserWithHashedPassword(Guid.NewGuid(),"Username2","it"),
                CreateUserWithHashedPassword(Guid.NewGuid(),"Username3","it"),
                CreateUserWithHashedPassword(Guid.NewGuid(),"Username4","it"),
                CreateUserWithHashedPassword(Guid.NewGuid(),"Username5","it")
            };
            var filtered = database.Where(x => x.Username.Equals(userDto.Username)).FirstOrDefault();

            _userRepository.Setup(x => x.GetUserByUsername(userDto.Username)).Returns(filtered);

            //ACT
            var result = await _userService.LoginAsync(userDto);

            //ASSERT
            Assert.True(result.IsFailed);

        }




    }
}
