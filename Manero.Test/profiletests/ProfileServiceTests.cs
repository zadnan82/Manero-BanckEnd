
using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.EntityFrameworkCore;

namespace Manero.Test.profiletests;

public class ProfileServiceTests
{
    //karam-test
    [Fact]
    public async Task CreateProfile_WhenUserDoesNotExist_ReturnsUserNotFound()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new DataContext(dbContextOptions);
        var profileRepo = new ProfileRepo(context);
        var userRepo = new UserRepo(context);

        var profileService = new ProfileService(context, profileRepo, userRepo);

        var userEmail = "zoro@gmail.com";
        var request = new ProfileCreateRequest
        {
            FirstName = "zoro",
            LastName = "rorona",
            ImageUrl = "https://example.com/image.jpg",
            Location = "japan",
            PhoneNumber = "12345"
        };

        // Act
        var result = await profileService.CreateProfile(userEmail, request);

        // Assert
        Assert.Equal(ResponseStatusCode.NOTFOUND, result.Status);
        Assert.Equal("User not found", result.Message);
    }


    //Karam-Test
    [Fact]
    public async Task UpdateProfile_WhenValidInput_ReturnsOk()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "UpdateProfileTestDatabase")
            .Options;

        using var context = new DataContext(dbContextOptions);
        //using (var context = new DataContext(dbContextOptions)) ;
        
            var userRepo = new UserRepo(context);
            var apiKeyRepo = new ApiKeyRepo(context);
            var userservice = new UserService(userRepo, apiKeyRepo);
            var profileRepo = new ProfileRepo(context);
            var profileService = new ProfileService(context, profileRepo, userRepo);

            var userEmail = "Zainab@hotmail.com";
            var existingUser = new UserEntity { Email = userEmail };
            var existingProfile = new ProfileEntity { UserId = existingUser.Id };
        
            var newuser = new UserCreateRequest 
            {
                 Email = "Zainab@hotmail.com",
                FirstName = "Zainab",
                LastName = "Adnan",
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
        //context.Users.Update(existingUser);
        //context.Profiles.Update(existingProfile);
        //await context.SaveChangesAsync();
            profileService.CreateProfile(userEmail, newRequest);

        
            var updateRequest = new ProfileUpdateRequest
            {
                FirstName = "karam",
                LastName = "messi",
                ImageUrl = "https://example.com/updated-image.jpg",
                Location = "stockholm",
                PhoneNumber = "98321"
            };

            // Act
            var result = await profileService.UpdateProfile(userEmail, updateRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResponseStatusCode.OK, result.Status);
            Assert.Equal("Profile updated successfully", result.Message);
        
    }


}
