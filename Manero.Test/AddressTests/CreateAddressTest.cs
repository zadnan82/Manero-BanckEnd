using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Manero.Test.ManagemenTests
{
    public class CreateAddressTest
    {
        [Fact] //FredrikSpanien Test
        public async Task CreateAddress_When_UserNotFound_Returns_NotFound()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DataContext(dbContextOptions);
            var profileRepo = new ProfileRepo(context);
            var userRepo = new UserRepo(context);
            var addressRepo = new AddressRepo(context);
            var addressTypeRepo = new AddressTypeRepo(context);


            var addressService = new AddressService(context, profileRepo, userRepo, addressRepo, addressTypeRepo);

            var userEmail = "Icanhasmilk@gmail.com";
            var request = new AddressCreateRequest()
            {
                FirstName = "Pelle",
                LastName = "Svanlös",
                StreetName = "Krukmakargatan 68",
                City = "Kattmeow",
                Title = "Home",
                AddressId = 3,
                Zipcode = "52115"
            };

            // Act
            var result = await addressService.CreateAddress(userEmail, request.StreetName, request);

            // Assert
            Assert.Equal(ResponseStatusCode.NOTFOUND, result.Status);
            Assert.Equal("user not found", result.Message);
        }

    }
}
