using HealthTracker.Core.Entities;
using HealthTracker.Core.IRepositories;

namespace HealthTracker.Core;

public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : class; 
    
    IUserRepository UserRepository { get; }
    
    Task<int> CompleteAsync();
}