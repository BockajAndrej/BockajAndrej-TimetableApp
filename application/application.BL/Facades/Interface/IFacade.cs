using System.Linq.Expressions;

namespace application.BL.Facades.Interface;

public interface IFacade<TEntity, TModel>
{
    public Task<ICollection<TModel>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
    public Task<TModel> SaveAsync(TModel entity);
    public Task DeleteAsync(TModel entity);
}