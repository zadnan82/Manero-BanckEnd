using Manero_BanckEnd.Controllers;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Contexts;
using Microsoft.EntityFrameworkCore;
using Manero_BanckEnd.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Manero.Test.profiletests;

public class ProfileControllerTest
{

    [Fact]
    public async Task GetProfile_WhenValidUser_ReturnsOk()
    {

        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GetProfileTestDatabase")
            .Options;

        using var context = new DataContext(dbContextOptions);
        //using (var context = new DataContext(dbContextOptions)) ;

        var userRepo = new UserRepo(context);
        var apiKeyRepo = new ApiKeyRepo(context);
        var userservice = new UserService(userRepo, apiKeyRepo);
        var profileRepo = new ProfileRepo(context);
        var profileService = new ProfileService(context, profileRepo, userRepo);

        var userEmail = "zoro@gmail.com";
        var existingUser = new UserEntity { Email = userEmail };
        var existingProfile = new ProfileEntity { UserId = existingUser.Id };

        var newuser = new UserCreateRequest
        {
            Email = "zoro@gmail.com",
            FirstName = "Zoro",
            LastName = "rononoa",
            Password = "password"
        };

        userservice.CreateUserAsync(newuser);

        var newRequest = new ProfileCreateRequest
        {
            FirstName = "zoro",
            LastName = "rononoa",
            ImageUrl = "https://example.com/updated-image.jpg",
            Location = "spain",
            PhoneNumber = "987654321"
        };
       
        profileService.CreateProfile(userEmail, newRequest);

        // Act
        var result = await profileService.GetProfile(userEmail);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(ResponseStatusCode.OK, result.Status);

    }

}

   