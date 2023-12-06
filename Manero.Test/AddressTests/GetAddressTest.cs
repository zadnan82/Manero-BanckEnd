using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manero_BanckEnd.Entities;

namespace Manero.Test.ManagemenTests
{
    public class GetAddressTest
    {
        [Fact] //FredrikSpanien Test
        public async Task GetAddress_When_AddressExists_Returns_OkWithAddress()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DataContext(dbContextOptions);

            
            var addressEntity = new AddressEntity
            {
                StreetName = "Krukmakargatan 68",
                City = "Kattmeow",
                Zipcode = "52115",
                
            };

            
            await context.Addresses.AddAsync(addressEntity);
            await context.SaveChangesAsync();

            var profileRepo = new ProfileRepo(context);
            var userRepo = new UserRepo(context);
            var addressRepo = new AddressRepo(context);
            var addressTypeRepo = new AddressTypeRepo(context);

            var addressService = new AddressService(context, addressRepo);

            var streetNameToFetch = "Krukmakargatan 68";

            // Act
            var result = await addressService.GetAddress(streetNameToFetch);

            // Assert
            Assert.Equal(ResponseStatusCode.OK, result.Status);
            Assert.NotNull(result.Result);
            Assert.Equal(addressEntity.StreetName, (result.Result as AddressEntity)?.StreetName);
            Assert.Equal(addressEntity.Zipcode, (result.Result as AddressEntity)?.Zipcode);
            Assert.Equal(addressEntity.City, (result.Result as AddressEntity)?.City);
        }
    }
}
