using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manero_BanckEnd.Repositories;

public class CardRepo : Repo<CardEntity>
{
    private readonly DataContext _context;
    public CardRepo(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CardEntity> GetAsync(int cardId, string userId)
    {
        return await _context.Cards.Where(x => x.UserId == userId && x.Id == cardId).FirstOrDefaultAsync();
    }

    public async Task<ICollection<CardEntity>> GetUserByCardDetailsAsync(string userId)
    {
        return await _context.Cards.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<string> GetUserIdByEmailAsync(string userEmail)
    {

        var user = await _context.Users.Where(u => u.Email == userEmail).FirstOrDefaultAsync();


        return user?.Id;
    }
}
