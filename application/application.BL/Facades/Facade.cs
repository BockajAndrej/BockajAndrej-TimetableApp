using System.Linq.Expressions;
using System.Reflection;
using application.BL.Facades.Interface;
using application.BL.Mappers;
using application.DAL;
using application.DAL.Factories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace application.BL.Facades;

public class Facade
    <TEntity, TModel, TDbContext> : IFacade<TEntity, TModel> where TEntity : class where TDbContext : DbContext
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

            IQueryable<TModel> projectedQuery = query.ProjectTo<TModel>(_mapper.ConfigurationProvider);
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

    public async Task DeleteAsync(TModel model)
    {
        PropertyInfo? idProperty = typeof(TModel).GetProperty("Id");
        if (idProperty == null)
            throw new InvalidOperationException($"Typ {typeof(TModel).Name} nemá property s názvom 'Id'.");


        object? entityId = idProperty.GetValue(model);
        if (entityId == null)
            throw new ArgumentException("Entity Id is not defined");


        using (var dbContext = _factory.CreateDbContext())
        {
            TEntity? entityToDelete = await dbContext.Set<TEntity>().FindAsync(entityId);
            if (entityToDelete != null)
            {
                dbContext.Remove(entityToDelete);
                dbContext.SaveChanges();
            }
        }
    }
}