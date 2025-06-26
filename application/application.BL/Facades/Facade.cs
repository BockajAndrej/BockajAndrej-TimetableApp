using System.Linq.Expressions;
using application.DAL;
using application.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace application.BL.Facades;

public class Facade<TEntity> (DbContexCpFactory factory): IFacade<TEntity> where TEntity : class
{
    //Todo: containing entities with include does not implemented -- 26.6.25
    public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
         var dbContext = factory.CreateDbContext();

        // Access to DbSet
        IQueryable<TEntity> query = dbContext.Set<TEntity>();

        if (filter != null)
             query = query.Where(filter);

        return Task.FromResult<IEnumerable<TEntity>>(query);
    }

    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        var dbContext = factory.CreateDbContext();
        
        var dbSet = dbContext.Set<TEntity>();
        var firstEntity = dbSet.FirstOrDefault();
        if (firstEntity == null)
            dbContext.Add(entity);
        else
            dbContext.Update(entity);
        
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public void DeleteAsync()
    {
        throw new NotImplementedException();
    }
}