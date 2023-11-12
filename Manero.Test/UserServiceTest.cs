using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Manero.Test
{
    public class UserServiceTests
    {
        private readonly UserRepo _realUserRepo;
        private readonly ApiKeyRepo _realApiKeyRepo; // Use the real ApiKeyRepo
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _realUserRepo = new UserRepo(new DataContext());
            _realApiKeyRepo = new ApiKeyRepo(new DataContext()); // Create a real instance of ApiKeyRepo

            _userService = new UserService(_realUserRepo, _realApiKeyRepo);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedUser()
        {
            // Arrange
            var schema = new UserCreateRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@gmail.com",
                Password = "password",
            };

            // Act
            var result = await _userService.CreateUserAsync(schema);

            Console.Write("");


            // Assert 
            UserCreateResult userResult = (UserCreateResult)result.Result;
            Assert.NotNull(result); 
            Assert.Equal(schema.FirstName, userResult.User.FirstName);
            Assert.Equal(schema.LastName, userResult.User.LastName);
            Assert.Equal(schema.Email, userResult.User.Email);
        }
    }
}
