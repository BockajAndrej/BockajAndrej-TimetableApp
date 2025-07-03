using application.BL.Facades.Interface;
using application.DAL.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace application.BL.Facades;

public class Facade
    <TEntity, TModel, TDbContext, TIdValue> : IFacade<TEntity, TModel, TIdValue>
    where TEntity : class, IEntity<TIdValue>
    where TDbContext : DbContext
{
    private IDbContextFactory<TDbContext> _factory;
    private IMapper _mapper;
    public Facade(IDbContextFactory<TDbContext> factory, IMapper mapper)
    {
        _factory = factory;
        _mapper = mapper;
    }
    //Todo: Constraints for CP state need to be entered in SQL script -- 26.6.2025
    //Todo: There is TEntity (need to be changed to TModel) -- 26.6.2025
    //Todo: Define max count of loaded entities -- 27.6.2025
    public async Task<ICollection<TModel>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        List<TModel> resultList;
        using (var dbContext = await _factory.CreateDbContextAsync())
        {
            // Access to DbSet
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            IQueryable<TModel> projectedQuery = query
                .AsSplitQuery()
                .ProjectTo<TModel>(_mapper.ConfigurationProvider);

            resultList = await projectedQuery.ToListAsync();
        }
        return resultList;
    }

    public async Task<TModel> SaveAsync(TModel model)
    {
        TEntity entity = _mapper.Map<TEntity>(model);
        using (var dbContext = _factory.CreateDbContext())
        {
            dbContext.Update(entity);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                dbContext.Add(entity);
                await dbContext.SaveChangesAsync();
            }
        }

        return _mapper.Map<TModel>(entity);
    }


    public async Task DeleteAsync(TIdValue Id)
    {
        using (var dbContext = await _factory.CreateDbContextAsync())
        {
            TEntity? entity = await dbContext.Set<TEntity>().FindAsync(Id);

            if (entity != null)
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}