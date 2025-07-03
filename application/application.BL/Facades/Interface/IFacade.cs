using System.Linq.Expressions;
using application.DAL.Entities;

namespace application.BL.Facades.Interface;

public interface IFacade<TEntity, TModel, TIdValue>
{
    public Task<ICollection<TModel>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
    public Task<TModel> SaveAsync(TModel entity);
    public Task DeleteAsync(TIdValue entity);
}