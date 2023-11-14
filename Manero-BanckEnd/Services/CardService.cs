using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;

namespace Manero_BanckEnd.Services;

public class CardService
{
    private readonly CardRepo _cardRepo;
    private readonly UserRepo _userRepo;

    public CardService(CardRepo cardRepo, UserRepo userRepo)
    {
        _cardRepo = cardRepo;
        _userRepo = userRepo;
    }

    public async Task<ServiceResponse> CreateCardAsync(CardEntity cardEntity, string userEmail)
    {
        try
        {
            var userId = await _cardRepo.GetUserIdByEmailAsync(userEmail);
            cardEntity.UserId = userId;

            await _cardRepo.CreateAsync(cardEntity);

            return new ServiceResponse { Status = ResponseStatusCode.CREATED, Result = cardEntity };
        }
        catch (Exception ex) { return new ServiceResponse { Status = ResponseStatusCode.ERROR }; }
    }

    public async Task<ServiceResponse> GetAllAsync(string userEmail)
    {

        var userId = await _cardRepo.GetUserIdByEmailAsync(userEmail);

        var cardDetails = await _cardRepo.GetUserByCardDetailsAsync(userId);
        var cardDetailModel = cardDetails.Select(x => (CardModel)x).ToList();
        return new ServiceResponse { Status = ResponseStatusCode.OK };
    }

    public async Task<ServiceResponse> DeleteAsync(int cardId, string userEmail)
    {
        var userId = await _cardRepo.GetUserIdByEmailAsync(userEmail);
        var card = await _cardRepo.GetAsync(cardId, userId);
        if (card == null)
        {
            throw new ArgumentException("");
        }

        await _cardRepo.DeleteAsync(card);


        return new ServiceResponse { Status = ResponseStatusCode.OK, Result = cardId, Message = "Card has been removed!" };
    }

    public async Task<ServiceResponse> PutAsync(CardEntity entity, string userEmail)
    {
        var userId = await _cardRepo.GetUserIdByEmailAsync(userEmail);
        CardEntity originalEntity = await _cardRepo.GetAsync(entity.Id, userId);
        if (originalEntity == null)
        {
            throw new ArgumentException("");

        }
        originalEntity.Id = entity.Id;
        originalEntity.CardNumber = entity.CardNumber;
        originalEntity.CardHolderName = entity.CardHolderName;
        originalEntity.CVV = entity.CVV;
        originalEntity.ExpirationDate = entity.ExpirationDate;

        await _cardRepo.UpdateAsync(originalEntity);


        return new ServiceResponse { Status = ResponseStatusCode.OK, Result = entity, Message = "Card has been changed!" };
    }
}
