using System.Collections;
using HealthTracker.Core;
using HealthTracker.Core.Entities;
using HealthTracker.Core.IRepositories;
using HealthTracker.Repository.Data;

namespace HealthTracker.Repository;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly HealthTrackerDbContext _dbContext;
    private readonly Hashtable _repositories;

    public UnitOfWork(HealthTrackerDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _repositories = new Hashtable();
        UserRepository = userRepository;
    }

    IGenericRepository<T> IUnitOfWork.Repository<T>()
    {
        return Repository<T>();
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(T));
            var repositoryInstance = Activator.CreateInstance(repositoryType, _dbContext);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    public IUserRepository UserRepository { get; }

    public Task<int> CompleteAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}