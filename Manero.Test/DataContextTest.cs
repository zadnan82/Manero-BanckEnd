using Azure.Core;
using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test;

public class DataContextTest
{
    private DataContext DataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new DataContext(options);
    }

    [Fact]  //Zainab Test
    public void ConnctionStringsSqlServer_Should_Be_ConnectionStringToLocalDatabase()
    {
        //Arrange 
        var expected = "Server=localhost;Database=ManeroDB;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        //Act 
        var result = configuration.GetConnectionString("SqlServer");

        //Assert
        Assert.Equal(expected, result);
    }



    [Fact]  //Zainab Test
    public async Task AddAsync_Should_AddUserToDatabase_Return_UserEntity()
    {
        //Arrange 
        var context = DataContext();
        var user = new UserEntity
        {
            Id = Guid.NewGuid().ToString(),
            Email = "Zainab@hotmail.com",
            FirstName = "Zainab",
            LastName = "Adnan",  
        };
        user.GenerateSecurPassword("Bytmig123!");

        //Act 
        await context.AddAsync(user);
        await context.SaveChangesAsync();

        //Assert 
        var userEntity = await context.Users.FirstOrDefaultAsync();
        Assert.NotNull(userEntity);
        Assert.Equal(user.Id, userEntity!.Id);

        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [Fact]  //Zainab Test
    public async Task FirstOrDefaultAsync_Should_ReturnOneUserEntity()
    {
        //Arrange 
        var context = DataContext();
        var user = new UserEntity
        {
            Id = Guid.NewGuid().ToString(),
            Email = "Zainab@hotmail.com",
            FirstName = "Zainab",
            LastName = "Adnan",
        };
        user.GenerateSecurPassword("Bytmig123!");

        await context.AddAsync(user);
        await context.SaveChangesAsync(); 

        //Act  
        var userEntity = await context.Users.FirstOrDefaultAsync();


        //Assert
        Assert.NotNull(userEntity);

        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
