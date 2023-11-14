﻿using Manero_BanckEnd.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Manero_BanckEnd.Repositories;

public abstract class Repo<TEntity> where TEntity : class
{
    private readonly DataContext _context; 

    protected Repo(DataContext context)
    {
        _context = context;
    }   

    public virtual async Task<TEntity> CreateAsync(TEntity entity )
    {
        try
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        } catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!; 

    }
    public virtual async Task<IEnumerable<TEntity>> GetAsync()
    {
        try
        {
            return await  _context.Set<TEntity>().ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;

    }
    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool >> expression)
    {
        try
        {
            var entity =  await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            return entity ?? null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;

    }


    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual async Task<bool> DeleteAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;

    }

    public virtual async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            return await _context.Set<TEntity>().AnyAsync(expression);
       
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;

    }

  

}
