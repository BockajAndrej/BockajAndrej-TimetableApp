using System.Linq.Expressions;

namespace application.BL.Facades;

public interface IFacade<TEntity>
{
    public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
    public Task<int> SaveAsync(TEntity entity);
    public void DeleteAsync(TEntity entity);
}