using System.Linq.Expressions;
using HealthTracker.Core.Entities;

namespace HealthTracker.Core.IRepositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(string id);

    Task AddAsync(T entity);
    
    Task Update(T entity);
    
    void Delete(T entity);
    
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);


    Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
}