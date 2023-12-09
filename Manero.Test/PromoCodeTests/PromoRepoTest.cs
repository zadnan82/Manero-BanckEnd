using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test.PromoCodeTests
{
    public class PromoRepoTest
    {
        private DataContext DataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new DataContext(options);
        }

        [Fact]  //Fredrik Test
        public async Task CreateAsync_Should_ReturnPromoEntity()
        {
            //Arrange 
            var context = DataContext();
            var repository = new PromoCodeRepo(context);
            var maxId = context.PromoCodes.Max(e => (int?)e.Id) ?? 0;
            var newId = maxId + 1;

            var promoEntity = new PromoCodeEntity
            {
                Id = newId,
                PromoName = "Test",
                SalePercentage = 75,
                Validity = DateTime.UtcNow,
            };
          
            //Act 
            var result = await repository.AddAsync(promoEntity);

            //Assert  
            Assert.NotNull(result);
            Assert.IsType<PromoCodeEntity>(result);
            Assert.Equal(promoEntity.Id, result.Id);
        }
    }
}