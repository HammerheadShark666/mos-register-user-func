using Microservice.Register.Function.Data.Contexts;
using Microservice.Register.Function.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Register.Function.Data.Repository;

public class UserRepository(IDbContextFactory<UserDbContext> dbContextFactory) : IUserRepository
{
    public IDbContextFactory<UserDbContext> _dbContextFactory { get; set; } = dbContextFactory; 
          
    public async Task<bool> UserExistsAsync(string email)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        return await db.Users.AnyAsync(x => x.Email.Equals(email));
    } 

    public async Task<Domain.User> AddAsync(Domain.User user)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        await db.Users.AddAsync(user);
        db.SaveChanges();

        return user;
    }
}