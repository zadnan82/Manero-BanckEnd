using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Manero_BanckEnd.Repositories;

public class TokenRepo  
{

    private readonly DataContext _dataContext; 
    public TokenRepo(DataContext context) 
    {
        _dataContext = context; 

    }

    public async Task<TokenEntity> SetRefreshTokenAsync(TokenEntity  tokenEntity)
    {
        try
        {
            var result = await _dataContext.Tokens.FirstOrDefaultAsync(x => x.UserId == tokenEntity.UserId);
            if (result != null)
            {
                result = tokenEntity;
                _dataContext.Tokens.Update(result); 
                await _dataContext.SaveChangesAsync();   
            }
            else
            {
                result = tokenEntity;
                _dataContext.Tokens.AddAsync(result);
                await _dataContext.SaveChangesAsync();
            }
            return result;

        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return null!; 
    }

    public async Task<TokenEntity> GetRefreshTokenAsync(string userId)
    {
        try
        {
            var result = await _dataContext.Tokens.FirstOrDefaultAsync(x => x.UserId == userId );
            return result ??= null!; 

        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return null!;
    }
}
