using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Test.CardTests
{
    public class CardRepoTests
    {
        [Fact]
        public async Task GetAsync_Returns_Correct_CardEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new DataContext(options))
            {
                var cardId = 1;
                var userId = "user123";
                var testData = new List<CardEntity>
                 {
                     new CardEntity { Id = cardId, UserId = userId, CardHolderName = "test" },
                     new CardEntity { Id = 2, UserId = "otherUserId", CardHolderName = "test" }
                 };

                context.Cards.AddRange(testData);
                context.SaveChanges();

                var repo = new CardRepo(context);

                // Act
                var result = await repo.GetAsync(cardId, userId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(cardId, result.Id);
                Assert.Equal(userId, result.UserId);
            }
        }



    }

}
