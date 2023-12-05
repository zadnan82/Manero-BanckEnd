using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas.Card;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manero_BanckEnd.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Manero.Test.CardTests
{
    public class CardServiceTest
    {
        private DataContext DataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new DataContext(options);
        }
        [Fact] //Fredrik Test
        public async Task GetAllAsync_Returns_Correct_CardModels()
        {
            var context = DataContext();
            var repository = new UserRepo(context);
            var cardRepo = new CardRepo(context);
            var cardEntity = new CardEntity
            {
                Id = 1,
                CardHolderName = "Test",
                CardNumber = 212948124,
                CVV = 182,
                ExpirationDate = DateTime.Now,
                User = new UserCreateRequest { FirstName = "Test", LastName = "Testson", Email = "test@test.se", Password = "test123" }

            };
            
            context.Add(cardEntity);
            context.SaveChanges();
            //await cardRepo.CreateAsync(cardEntity);

            //Act 

            var result = await cardRepo.GetAsync(c => c.Id == cardEntity.Id);

            //Assert  
            Assert.NotNull(result);
            Assert.Equal(cardEntity.Id, result.Id);
            Assert.Equal(cardEntity.CardHolderName, result.CardHolderName);
        }


        [Fact]  //Fredrik Test
        public async Task DeleteAsync_When_CardExists_Should_DeleteCard()
        {

            //Arrange 
            var context = DataContext();
            var userRepository = new UserRepo(context);
            var cardRepo = new CardRepo(context);
            var service = new CardService(cardRepo, userRepository);

            var createCard = new CardEntity
            {
                Id = 1,
                CardHolderName = "John",
                CardNumber = 212948124,
                CVV = 182,
                ExpirationDate = DateTime.Now,
                User = new UserCreateRequest { FirstName = "Test", LastName = "Testson", Email = "test@test.se", Password = "test123" }

            };
            context.Add(createCard);
            context.SaveChanges();

            var card = new CardDeleteRequest
            {
                CardId = 1,
            };

            var userEmail = createCard.User.Email.ToString();

            //Act 
            var result = await service.DeleteAsync(card, userEmail);
            //Assert  
            Assert.NotNull(result);
            Assert.Equal(ResponseStatusCode.OK, result.Status);

        }
    }
}
