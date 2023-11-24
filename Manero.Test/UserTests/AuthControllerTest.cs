using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test;

public class AuthControllerTest
{
    private DataContext DataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new DataContext(options);
    }

    [Fact]   //Zainab Test
    public async Task Create_Should_ReturnConflic_IfUserAlreadyWithSameEmailExists()
    {
        //Arrange  
        var context = DataContext();
        var userRepository = new UserRepo(context);
        var apiKeyRepo = new ApiKeyRepo(context);
        var service = new UserService(userRepository, apiKeyRepo);
        var tokerepo = new TokenRepo(context);

        var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        var tokenService = new TokenService(config , tokerepo);
        TokenGenerator token = new TokenGenerator(config);
        AuthController controller = new AuthController(service, token, config , tokenService);
        var form = new UserCreateRequest
        {
            Email = "zainab@hotmail.com",
            FirstName = "Zainab",
            LastName = "Adnan",
            Password = "Bytmig123!"
        };
         await service.CreateUserAsync(form);


        //Act 
        var result = await controller.RegisterUser(form);

        //Assert 
        Assert.NotNull(result); 
        Assert.IsType<ConflictObjectResult>(result);

    }
     

}