using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using UserAuthentication.Controllers;
using UserAuthentication.Dto;
using UserAuthentication.Entities;
using UserAuthentication.Mediatr.Requests;
using UserAuthentication.Repository.Interface;
using UserAuthentication.Services.impl;

namespace UnitTest
{
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _userController = new UserController(_mediator.Object);
        }


        [Fact]
        public async Task getAll_WorksAsync()
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



            _mediator.Setup(x => x.Send(It.IsAny<GetAllUsersRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(database);

            var UserDtos = database.Select(x => new DisplayUserDto
            {
                Username = x.Username
            }).ToList();

            //ACT
            var result = await _userController.getAll();

            var okResult = (OkObjectResult)result.Result;

            var deserializedList = JsonConvert.DeserializeObject<List<UserDto>>(JsonConvert.SerializeObject(okResult.Value));

            //ASSERT
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(UserDtos.Select(x => x.Username), deserializedList.Select(x => x.Username));
        }

        [Fact]
        public async Task Register_BadRequestAsync()
        {
            UserDto userDto = new UserDto
            {
                Username = "User",
                Password = "password"
            };
            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = userDto.Username,
                PasswordHash = "asd"
            };
            //var result = Result.Failure<User>("asd");
            var result = Result.Fail<User?>("A user with that username already exists");
            _mediator.Setup(x => x.Send(It.IsAny<RegisterUserRequest>(),It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var actual = await _userController.Register(userDto);

            //BadRequestObjectResult? newResult = (BadRequestObjectResult?)actual.Result;
            var badRequest = Assert.IsType<BadRequestObjectResult>(actual.Result);
            Assert.Equal("A user with that username already exists", badRequest.Value);

        }

        [Fact]
        public async Task Register_Ok()
        {

            //ARRANGE

            UserDto userDto = new UserDto
            {
                Username = "User",
                Password = "password"
            };
            User user = new User
            {
                Id = Guid.NewGuid(),
            };

            _mediator.Setup(x => x.Send(It.IsAny<RegisterUserRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            //ACT
            var actual = await _userController.Register(userDto);

            //ASSERT
            var okResult = Assert.IsType<OkObjectResult>(actual.Result);
            DisplayUserDto? returnValue = (DisplayUserDto?)okResult.Value;
            Assert.IsType<DisplayUserDto>(returnValue);
            Assert.Equal(user.Username, returnValue.Username);


        }


    }
}
