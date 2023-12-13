using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Schemas.PromoCodes;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test.PromoCodeTests
{
    public class PromoControllerTest
    {
        private DataContext DataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new DataContext(options);
        }
        //Victor Grimschold
        [Fact]
        public async Task CreateAsync_Should_Return_entity()
        {
            // Arrange
            var context = DataContext();
            var promocodeRepro = new PromoCodeRepo(context);
            var promservice = new PromoCodeService(promocodeRepro);


            var newPromo = new PromoCodeSchema
            {
                // Id = 1,
                PromoName = "Badtoffla",
                SalePercentage = 50,
                Validity = DateTime.Now

            };

            var result = await promservice.CreateAsync(newPromo);

            Assert.NotNull(result);
            Assert.IsType<PromoCodeEntity>(result);
            Assert.Equal(newPromo.PromoName, result.PromoName);

        }
        //Victor Grimschold
        [Fact]
        public async Task DeleteAsync_ExistingPromoCode_ShouldReturnDeletedPromoCode()
        {
            // Arrange
            var context = DataContext();
            var repo = new PromoCodeRepo(context);
            var service = new PromoCodeService(repo);

            // Skapa och initialisera ett nytt PromoCodeEntity-objekt
            var newPromo = new PromoCodeEntity
            {
                PromoName = "TestPromo", // Se till att alla obligatoriska fält är ifyllda
                SalePercentage = 50,
                Validity = DateTime.Now
            };

            context.PromoCodes.Add(newPromo);
            await context.SaveChangesAsync();

            // Act
            var result = await service.DeleteAsync(newPromo.Id);

            // Kontrollera att promo-koden faktiskt har tagits bort från databasen
            var deletedPromo = await context.PromoCodes.FindAsync(newPromo.Id);
            Assert.Null(deletedPromo);
        }
        //Victor Grimschold
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPromoCodes()
        {
            // Arrange
            var context = DataContext();
            var repo = new PromoCodeRepo(context);
            var service = new PromoCodeService(repo);

            // Skapa och lägg till PromoCodeEntity-objekt i databasen
            var promoCode1 = new PromoCodeEntity { PromoName = "Promo1", SalePercentage = 10, Validity = DateTime.Now.AddDays(30) };
            var promoCode2 = new PromoCodeEntity { PromoName = "Promo2", SalePercentage = 20, Validity = DateTime.Now.AddDays(60) };
            context.PromoCodes.AddRange(promoCode1, promoCode2);
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            var promoCodesList = result.ToList();
            Assert.Equal(2, promoCodesList.Count);
            Assert.Contains(promoCode1, promoCodesList);
            Assert.Contains(promoCode2, promoCodesList);
        }
    }
}
