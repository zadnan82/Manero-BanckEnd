using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Manero.Test.ManagemenTests
{
    public class UpdateAddressTest
    {
        [Fact] //FredrikSpanien Test
        public async Task UpdateAddress_When_AddressNotFound_Returns_NotFound()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DataContext(dbContextOptions);
            var addressRepo = new AddressRepo(context);

            var addressService = new AddressService(context, addressRepo);

            // Skapa en användaradress och lägg till den i databasen
            
            var addressEntity = new AddressEntity
            {
                StreetName = "Krukmakargatan 68",
                City = "Kattmeow",
                Zipcode = "52115"
            };

            var addressTypeEntity = new AddressTypeEntity
            {
                UserId = "3",
                Title = "Home",
                Address = addressEntity
            };

            await context.AddressTypes.AddAsync(addressTypeEntity);
            await context.SaveChangesAsync();

            var request = new AddressUpdateRequest
            {
                StreetName = "Krukmakargatan 68",
                City = "UpdatedCity",
                Title = "Home",
                Zipcode = "12345"
            };

            // Act
            var result = await addressService.UpdateAddress("Krukmakargatan 68", "Home", request);

            // Assert
            Assert.Equal(ResponseStatusCode.OK, result.Status);
            Assert.Equal("Address updated successfully", result.Message);
        }
    }
}
