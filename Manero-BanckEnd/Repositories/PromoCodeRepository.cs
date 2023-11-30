using Manero_BanckEnd.Contexts;

namespace Manero_BanckEnd.Repositories;

public abstract class PromoCodeRepository<TEntity> where TEntity : class
{
    private readonly DataContext _dataContext;

    protected PromoCodeRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity != null)
        {
            _dataContext.Set<TEntity>().Add(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }
        return null!;
    }


    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dataContext.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> DeleteAsync(int id)
    {
        var entity = await _dataContext.Set<TEntity>().FindAsync(id);

        if (entity != null)
        {
            _dataContext.Set<TEntity>().Remove(entity);
            await _dataContext.SaveChangesAsync();
            return null!;
        }

        return null!;
    }
}
