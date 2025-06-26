using System.Linq.Expressions;
using application.DAL;
using application.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace application.BL.Facades;

public class Facade<TEntity> : IFacade<TEntity> where TEntity : class
{
    private MyDbContext _dbContext;
    public Facade(DbContexCpFactory factory)
    {
        _dbContext = factory.CreateDbContext();
    }
    //Todo: containing entities with include does not implemented -- 26.6.25
    public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        // Access to DbSet
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();

        if (filter != null)
             query = query.Where(filter);

        return Task.FromResult<IEnumerable<TEntity>>(query);
    }

    public async Task<int> SaveAsync(TEntity entity)
    {
        int result;
        try
        {
            _dbContext.Update(entity);
            result = await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _dbContext.Add(entity);
            result = await _dbContext.SaveChangesAsync();
        }
        
        return result;
    }

    public void DeleteAsync(TEntity entity)
    {
        try
        {
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
        }    
        catch (DbUpdateConcurrencyException ex)
        {
        }
    }
}