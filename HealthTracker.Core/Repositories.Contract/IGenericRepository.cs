using System.Linq.Expressions;
using HealthTracker.Core.Entities;

namespace HealthTracker.Core.IRepositories;

public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAll();

    Task<T> GetById(Guid id);

    Task AddAsync(T entity);
    
    Task Update(T entity);
    
    void Delete(T entity);
    
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);


}