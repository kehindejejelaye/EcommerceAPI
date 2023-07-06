using Ecommerce.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.API.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected EcommerceContext ecomContext;

    public BaseRepository(EcommerceContext _ecomContext)
    {
        ecomContext = _ecomContext;
    }

    public IQueryable<T> FindAll(bool trackChanges) =>
       !trackChanges ?
        ecomContext.Set<T>().AsNoTracking() :
        ecomContext.Set<T>();
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ?
        ecomContext.Set<T>().Where(expression).AsNoTracking() :
        ecomContext.Set<T>().Where(expression);
    public void Create(T entity) => ecomContext.Set<T>().Add(entity);
    public void Update(T entity) => ecomContext.Set<T>().Update(entity);
    public void Delete(T entity) => ecomContext.Set<T>().Remove(entity);
}
