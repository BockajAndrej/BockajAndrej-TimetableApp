using System.Linq.Expressions;
using application.DAL;
using application.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace application.BL.Facades;

public class Facade<TEntity> (DbContexCpFactory factory): IFacade<TEntity> where TEntity : class
{
    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
         var dbContext = factory.CreateDbContext();

        // Access to DbSet
        IQueryable<TEntity> query = dbContext.Set<TEntity>();

        if (filter != null)
            query = query.Where(filter);

        return query;
    }

    public Task<TEntity> SaveAsync()
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync()
    {
        throw new NotImplementedException();
    }
}