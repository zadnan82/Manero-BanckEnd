using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test.UserTests.UserTests;

public class UserRepositoryTest
{
    private DataContext DataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new DataContext(options);
    }

    [Fact]  //Zainab Test
    public async Task CreateAsync_Should_ReturnUserEntity()
    {
        //Arrange 
        var context = DataContext();
        var repository = new UserRepo(context);
        var userEntity = new UserEntity
        {
            Id = Guid.NewGuid().ToString(),
            Email = "Zainab@hotmail.com",
            FirstName = "Zainab",
            LastName = "Adnan",
        };
        userEntity.GenerateSecurPassword("Bytmig123!");

        //Act 
        var result = await repository.CreateAsync(userEntity);

        //Assert  
        Assert.NotNull(result);
        Assert.IsType<UserEntity>(result);
    }

    [Fact]  //Zainab Test
    public async Task GetUserAsync_Should_ReturnAUser()
    {
        //Arrange 
        var context = DataContext();
        var repository = new UserRepo(context);
        var userEntity = new UserEntity
        {
            Id = Guid.NewGuid().ToString(),
            Email = "Zainab@hotmail.com",
            FirstName = "Zainab",
            LastName = "Adnan",
        };
        userEntity.GenerateSecurPassword("Bytmig123!");
        await repository.CreateAsync(userEntity);


        //Act 

        var result = await repository.GetAsync(x => x.Id == userEntity.Id);


        //Assert  
        Assert.NotNull(result);
        Assert.IsType<UserEntity>(result);
        Assert.Equal(userEntity.Id, result.Id);
    }
}
