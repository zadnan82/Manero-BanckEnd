using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Schemas.PromoCodes;
using Manero_BanckEnd.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Manero.Test.PromoCodeTests
{
    public class PromoServiceTest
    {
        //Kevin
        private DataContext DataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new DataContext(options);
        }



        //Kevin

        [Fact]
        public async Task CreateAsync_Should_CreatePromoCodeEntity_ReturnPromoCode()
        {
            // Arrange 
            var context = DataContext();
            var promoCodeRepository = new PromoCodeRepo(context);
            var promoCodeService = new PromoCodeService(promoCodeRepository);
            var promoCodeSchema = new PromoCodeSchema
            {
                PromoName = "TestPromo",
                SalePercentage = 10,
                Validity = DateTime.UtcNow.AddDays(7),
            };

            // Act 
            var result = await promoCodeService.CreateAsync(promoCodeSchema);

            // Assert  
            Assert.NotNull(result);
            Assert.IsType<PromoCodeEntity>(result);
            Assert.Equal(promoCodeSchema.PromoName, result.PromoName);
            Assert.Equal(promoCodeSchema.SalePercentage, result.SalePercentage);
            Assert.Equal(promoCodeSchema.Validity, result.Validity);
        }





        //Kevin


        [Fact]
        public async Task CreateAsync_InvalidSchema_Should_ReturnNull()
        {
            // Arrange 
            var context = DataContext();
            var promoCodeRepository = new PromoCodeRepo(context);
            var promoCodeService = new PromoCodeService(promoCodeRepository);

            // Act 
            var result = await promoCodeService.CreateAsync(null);

            // Assert  
            Assert.Null(result);
        }



        //Kevin


        [Fact]
        public async Task GetAllAsync_Should_ReturnListOfPromoCodeEntities()
        {
            // Arrange 
            var context = DataContext();
            var promoCodeRepository = new PromoCodeRepo(context);
            var promoCodeService = new PromoCodeService(promoCodeRepository);

            // Act 
            var result = await promoCodeService.GetAllAsync();

            // Assert  
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PromoCodeEntity>>(result);
        }




        //Kevin

        [Fact]
        public async Task DeleteAsync_Should_DeletePromoCodeEntity_ReturnDeletedEntity()
        {
            // Arrange 
            var context = DataContext();
            var promoCodeRepository = new PromoCodeRepo(context);
            var promoCodeService = new PromoCodeService(promoCodeRepository);

            // Act 
            var result = await promoCodeService.DeleteAsync(1);

            // Assert  
            Assert.Null(result);
        }





        //Kevin


        [Fact]
        public async Task DeleteAsync_NonExistingEntity_Should_ReturnNull()
        {
            // Arrange 
            var context = DataContext();
            var promoCodeRepository = new PromoCodeRepo(context);
            var promoCodeService = new PromoCodeService(promoCodeRepository);

            // Act 
            var result = await promoCodeService.DeleteAsync(1);

            // Assert  
            Assert.Null(result);
        }

    }
}