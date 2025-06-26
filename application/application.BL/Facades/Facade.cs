using System.Linq.Expressions;
using application.BL.Mappers;
using application.DAL;
using application.DAL.Factories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace application.BL.Facades;

public class Facade<TEntity, TModel> : IFacade<TEntity, TModel> where TEntity : class
{
    private MyDbContext _dbContext;
    private IMapper _mapper;
    public Facade(DbContexCpFactory factory, IMapper mapper)
    {
        _dbContext = factory.CreateDbContext();
        _mapper = mapper;
    }
    //Todo: There is TEntity (need to be changed to TModel) -- 26.6.2025
    public async Task<ICollection<TModel>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        // Access to DbSet
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();

        if (filter != null)
             query = query.Where(filter);
        
        IQueryable<TModel> projectedQuery = query.ProjectTo<TModel>(_mapper.ConfigurationProvider);
        List<TModel> resultList = await projectedQuery.ToListAsync();
        
        return resultList;
    }

    public async Task<int> SaveAsync(TModel model)
    {
        TEntity entity = _mapper.Map<TEntity>(model);
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

    public async Task DeleteAsync(TModel model)
    {
        var entityId = (string)model.GetType().GetProperty("Id").GetValue(model);
        
        if (entityId == null)
            throw new ArgumentException("Entity Id is not defined");
        
        TEntity? entityToDelete = await _dbContext.Set<TEntity>().FindAsync(entityId);
        if (entityToDelete != null)
        {
             _dbContext.Remove(entityToDelete);
             _dbContext.SaveChanges();
        }
    }
}