using System.Linq.Expressions;

namespace application.BL.Facades;

public interface IFacade<TEntity, TModel>
{
    public Task<ICollection<TModel>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
    public Task<int> SaveAsync(TModel entity);
    public Task DeleteAsync(TModel entity);
}