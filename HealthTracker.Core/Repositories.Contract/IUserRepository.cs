using HealthTracker.Core.Entities;

namespace HealthTracker.Core.IRepositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetUserByEmail(string email);
}