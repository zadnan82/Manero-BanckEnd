using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Schemas;
using Microsoft.Extensions.Configuration;

namespace Manero.Test.ManagemenTests
{
    public class GetAddressTest
    {
        

        [Fact] //FredrikSpanienTest
        public async Task GetAddress_When_AddressDoesNotExist_Returns_NotFound()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DataContext(dbContextOptions);
            var addressRepo = new AddressRepo(context);
            var addressService = new AddressService(context, addressRepo);

            // Act
            var result = await addressService.GetAddress("NonExistentStreet");

            // Assert
            Assert.Equal(ResponseStatusCode.NOTFOUND, result.Status);
            Assert.Equal("Address not found", result.Message);
            Assert.Null(result.Result);
        }

        
    }
}
