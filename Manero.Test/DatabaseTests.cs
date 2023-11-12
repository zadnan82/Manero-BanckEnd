using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test;

public class DatabaseTests
{
    [Fact]
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



    //[Fact]
    //public async Task CreateCustomerAsync_Should_CreateCustomer()
    //{
    //    // Arrange  
    //    var tempFakeUser = new UserCreateRequest
    //    {
    //        FirstName = "Lilly",
    //        LastName = "Smith",
    //        Email = "lilly@hotmail.com",
    //        Password = "Bytmig123!",
    //    };

    //    // Act
    //    await _userService.CreateUserAsync(tempFakeUser);

    //    var actualUser = await _userRepo.GetAsync(x =>x.Email == tempFakeUser.Email);

    //    // Assert
    //    Assert.NotNull(actualUser);
    //    Assert.Equal(tempFakeUser.FirstName, actualUser.FirstName);
    //    Assert.Equal(tempFakeUser.LastName, actualUser.LastName);
    //    Assert.Equal(tempFakeUser.Email, actualUser.Email);
    //}
}
