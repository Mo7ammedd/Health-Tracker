using System.Linq.Expressions;
using HealthTracker.Core.IRepositories;
using HealthTracker.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly HealthTrackerDbContext _context;

    protected GenericRepository(HealthTrackerDbContext context)
    {
        _context = context ;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
    {
        return predicate == null 
            ? await _context.Set<T>().ToListAsync() 
            : await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T> GetById(string id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }
}