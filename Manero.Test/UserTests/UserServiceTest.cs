using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test.UserTests.UserTests;
public class UserServiceTest
{
    private DataContext DataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new DataContext(options);
    }

    [Fact]  //Zainab Test
    public async Task CreateAsync_Should_CreateUserEntity_ReturnUser()
    {
        //Arrange 
        var context = DataContext();
        var userRepository = new UserRepo(context);
        var apiKeyRepo = new ApiKeyRepo(context);
        var service = new UserService(userRepository, apiKeyRepo);
        var user = new UserCreateRequest
        {
            Email = "Zainab@hotmail.com",
            FirstName = "Zainab",
            LastName = "Adnan",
            Password = "password"

        };

        //Act 
        var result = await service.CreateUserAsync(user);

        //Assert  
        Assert.NotNull(result);
        Assert.IsType<UserCreateResult>(result.Result);
    }
}
