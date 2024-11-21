﻿using System.Linq.Expressions;
using HealthTracker.Core.Entities;
using HealthTracker.Core.IRepositories;
using HealthTracker.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly HealthTrackerDbContext _context;

    public GenericRepository(HealthTrackerDbContext context)
    {
        _context = context;
    }

    public virtual async Task<IReadOnlyList<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetById(Guid id)
    {

        return await _context.Set<T>().FindAsync(id);
        
    }

    public virtual async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public virtual Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public virtual void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }
}