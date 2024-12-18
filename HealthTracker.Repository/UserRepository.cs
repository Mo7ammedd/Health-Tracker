﻿using System.Runtime.CompilerServices;
using HealthTracker.Core.Entities;
using HealthTracker.Core.IRepositories;
using HealthTracker.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(HealthTrackerDbContext context) : base(context)
    {

    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();

    }

    public async Task<User> GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
}